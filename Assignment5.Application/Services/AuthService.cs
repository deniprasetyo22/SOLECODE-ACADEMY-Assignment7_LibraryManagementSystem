using Assignment5.Domain.Models;
using Assignment7.Application.DTOs.Account;
using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Domain.Models.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _userRepository = userRepository;
        }
        //Sign Up The User
        public async Task<AuthResponse> SignUpAsync(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)

                return new AuthResponse { Status = "Error", Message = "User already exists!" };
            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return new AuthResponse
            {
                Status = "Error",
                Message = "User creation failed! Please check user details and try again."
            };

            await _userManager.UpdateAsync(user);

            User newUser = new User
            {
                firstName = model.FirstName,
                lastName = model.LastName,
                position = model.Position,
                privilage = model.Privilage,
                libraryCardNumber = model.LibraryCardNumber,
                notes = model.Notes,
                AppUserId = user.Id
            };

            var roleName = model.Role; // or any other role as per requirement
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                // Handle role assignment failure
                return new AuthResponse
                {
                    Status = "Error",
                    Message = "Role assignment failed! Please check the role and try again."
                };
            }

            await _userRepository.AddUser(newUser);

            var emailBody = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/register.html");
            emailBody = string.Format(emailBody,
                "Library Management System", //{0}
                model.Username,              //{1}
                model.Email,                 //{2}
                model.Username,              //{3}
                model.Password               //{4}
            );

            var mailData = new MailData
            {
                EmailToIds = new List<string> { model.Email },
                EmailCCIds = new List<string> { "deni22@yopmail.com" },
                EmailToName = model.Username,
                EmailSubject = "Welcome to Our Service!",
                EmailBody = emailBody,
                Password = model.Password,
                Attachments = new List<string> { @"./Templates/EmailTemplate/c#.png" }
            };

            var emailSent = _emailService.SendMail(mailData);

            if (!emailSent)
            {
                return new AuthResponse
                {
                    Status = "SuccessWithWarning",
                    Message = "User created successfully, but failed to send confirmation email."
                };
            }

            return new AuthResponse { Status = "Success", Message = "User created succesfully!" };
        }

        //Login user
        public async Task<AuthResponse> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new AuthResponse { Status = "Error", Message = "Username not found !" };
            }

            // Cek password
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthResponse { Status = "Error", Message = "Password invalid !" };
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenExpiresOn = token.ValidTo,
                Message = "User berhasil login!",
                User = user,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = token.ValidTo,
                Roles = userRoles.ToList(),
                Status = "Success"
            };

        }
        // Create Role
        public async Task<AuthResponse> CreateRoleAsync(string rolename)
        {
            if (!await _roleManager.RoleExistsAsync(rolename))
                await _roleManager.CreateAsync(new IdentityRole(rolename));
            return new AuthResponse { Status = "Success", Message = "Role Created successfully!" };
        }

        // Assign user to role that already created before
        public async Task<AuthResponse> AssignToRoleAsync(string userName, string rolename)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (await _roleManager.RoleExistsAsync($"{rolename}"))
            {
                await _userManager.AddToRoleAsync(user, rolename);
            }
            return new AuthResponse { Status = "Success", Message = "User created succesfully!" };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<AuthResponse> LogoutAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new AuthResponse { Status = "Error", Message = "User not found!" };
            }

            // Invalidate the user's refresh token
            user.RefreshToken = null;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new AuthResponse { Status = "Success", Message = "User successfully logged out!" };
            }

            return new AuthResponse { Status = "Error", Message = "Logout failed! Please try again." };
        }
    }
}
