using Assignment5.Domain.Models;
using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Persistence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Services
{
    public class BookRequestService:IBookRequestService
    {
        private readonly IBookRequestRepository _bookRequestRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public BookRequestService(IBookRequestRepository bookRequestRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _bookRequestRepository = bookRequestRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<Bookrequest> AddBookRequestAsync(Bookrequest bookRequest)
        {
            // Check if bookRequest is null
            if (bookRequest == null)
            {
                throw new ArgumentNullException(nameof(bookRequest), "Book request cannot be null.");
            }

            // Check if Process is null
            if (bookRequest.Process == null)
            {
                throw new ArgumentNullException(nameof(bookRequest.Process), "Process cannot be null.");
            }

            // Get current user ID from HttpContext
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var userName = httpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var userId = user.Id;

            bookRequest.Process.Requesterid = userId;

            return await _bookRequestRepository.AddBookRequestAsync(bookRequest);
        }


        public async Task<IEnumerable<Bookrequest>> GetAllBookRequestAsync()
        {
            return await _bookRequestRepository.GetAllBookRequestAsync();
        }
    }
}
