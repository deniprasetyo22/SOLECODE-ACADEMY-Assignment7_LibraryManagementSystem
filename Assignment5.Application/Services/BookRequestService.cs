using Assignment5.Domain.Models;
using Assignment7.Application.DTOs;
using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Domain.Models;
using Assignment7.Domain.Models.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment7.Application.Services
{
    public class BookRequestService:IBookRequestService
    {
        private readonly IBookRequestRepository _bookRequestRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProcessRepository _processRepository;
        private readonly IWorkflowActionRepository _workflowActionRepository;
        private readonly EmailService _emailService;
        private readonly IWorkflowRepository _workflowRepository;

        public BookRequestService(IBookRequestRepository bookRequestRepository, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<AppUser> userManager, 
            IProcessRepository process, 
            IWorkflowActionRepository workflowActionRepository,
            EmailService emailService,
            IWorkflowRepository workflowRepository)
        {
            _bookRequestRepository = bookRequestRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _processRepository = process;
            _workflowActionRepository = workflowActionRepository;
            _emailService = emailService;
            _workflowRepository = workflowRepository;
        }
        public async Task<Bookrequest> AddBookRequestAsync(Bookrequest bookRequest)
        {
            // Ensure the user is authenticated
            var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            // Retrieve the current user
            var currentUser = await _userManager.FindByNameAsync(userName);
            if (currentUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var userId = currentUser.Id;

            // Check if the workflow exists
            var workflowExists = await _workflowRepository.GetWorkflowByIdAsync(1); // Adjust the ID as necessary
            if (workflowExists == null)
            {
                throw new InvalidOperationException("The specified workflow does not exist.");
            }

            // Create a new Process
            var process = new Process
            {
                Workflowid = 1,
                Requestdate = DateTime.Now,
                Status = "Reviewed By Librarian",
                Currentstepid = 2,
                Requesttype = "Borrow Book Request",
                Requesterid = userId
            };

            // Add the process to the repository
            await _processRepository.AddProcessAsync(process);

            // Create a new Workflowaction
            var workflow = new Workflowaction
            {
                Requestid = process.Processid,
                Stepid = 1,
                Actorid = userId,
                Action = process.Status,
                Actiondate = DateTime.Now,
                Comments = "Reviewed By Librarian"
            };

            // Add the workflow action to the repository
            await _workflowActionRepository.AddWorkflowActionAsync(workflow);

            // Prepare the Bookrequest entity
            bookRequest.Processid = process.Processid; // Set the Process ID
            bookRequest.Startdate = DateTime.Now;
            bookRequest.Enddate = bookRequest.Startdate?.AddDays(7);

            // Add the book request to the repository
            await _bookRequestRepository.AddBookRequestAsync(bookRequest);

            // Prepare and send the email notification
            var emailBody = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/BookRequest.html");
            emailBody = string.Format(emailBody, "Borrow Book Request", currentUser.UserName);

            var mailData = new MailData
            {
                EmailToIds = new List<string> { currentUser.Email },
                EmailCCIds = new List<string> { "deni.prasetyo@solecode.id" },
                EmailToName = currentUser.UserName,
                EmailSubject = "Welcome to Our Service!",
                EmailBody = emailBody
            };

            _emailService.SendMail(mailData);

            return bookRequest;
        }

        /*public async Task<IEnumerable<BookRequestDTO>> GetAllBookRequestsAsync()
        {
            var bookRequests = await _bookRequestRepository.GetAllBookRequestsAsync();
            var processes = await _processRepository.GetAllProcessAsync();
            var workflowActions = await _workflowActionRepository.GetAllWorkflowActionsAsync();

            var userQueryable = _userManager.Users.AsQueryable();

            var bookRequestDTOs = from request in bookRequests
                                  join process in processes on request.Processid equals process.Processid // Join with Process
                                  join user in userQueryable on process.Requesterid equals user.Id into userGroup
                                  from user in userGroup.DefaultIfEmpty() // Left join
                                  join action in workflowActions on process.Processid equals action.Requestid into actionGroup
                                  from action in actionGroup.DefaultIfEmpty() // Left join
                                  select new BookRequestDTO
                                  {
                                      Id = request.Requestid, // Assuming BookRequest has an Id property
                                      RequestName = request.Requestname, // Assuming BookRequest has a Requesttype property
                                      Description = action?.Comments, // Assuming comments are part of the description
                                      Title = request.Title, // Set this based on your logic
                                      ISBN = request.Isbn, // Set this based on your logic
                                      Author = request.Author, // Set this based on your logic
                                      Publisher = request.Publisher, // Set this based on your logic
                                      ProcessId = process.Processid,
                                      Requester = user?.UserName ?? "Unknown",
                                      Status = action.Comments,
                                      RequestDate = process.Requestdate ?? DateTime.MinValue // Handle nulls appropriately
                                  };

            // Convert to list asynchronously
            return await Task.FromResult(bookRequestDTOs.ToList());
        }*/

        public async Task<IEnumerable<Bookrequest>> GetAllBookRequestsAsync()
        {
            return await _bookRequestRepository.GetAllBookRequestsAsync();

        }


        public async Task<Bookrequest> GetBookRequestByIdAsync(int id)
        {
            return await _bookRequestRepository.GetBookRequestByIdAsync(id);
        }


        public async Task ApprovalAsync(int processId, Process process)
        {
            var userRoles = _httpContextAccessor.HttpContext?.User.Claims
                  .Where(c => c.Type == ClaimTypes.Role)
                  .Select(c => c.Value)
                  .ToList();

            var user = _httpContextAccessor.HttpContext?.User.Identity!.Name;

            var currentUser = await _userManager.FindByNameAsync(user!);
            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user not found.");
            }

            var userId = currentUser?.Id;
            var userName = currentUser?.UserName;
            var userEmail = currentUser?.Email;

            // Retrieve the existing process
            var existingProcess = await _processRepository.GetProcessByIdAsync(processId);
            if (existingProcess == null)
            {
                throw new InvalidOperationException($"Process with ID {processId} not found.");
            }

            if (userRoles.Contains("Librarian"))
            {
                existingProcess.Status = process.Status;
                if (process.Status == "Approve")
                {
                    existingProcess.Currentstepid = 3;
                    existingProcess.Status = "Reviewed By Manager";

                    var workflow = new Workflowaction
                    {
                        Requestid = processId,
                        Stepid = 2,
                        Actorid = userId,
                        Action = existingProcess.Status,
                        Actiondate = DateTime.Now,
                        Comments = "Approved By Librarian"
                    };

                    await _workflowActionRepository.AddWorkflowActionAsync(workflow);

                    var emailBody = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/ApprovalBookRequest.html");
                    emailBody = string.Format(emailBody,
                        "Borrowing Book Request",           //{0}
                        existingProcess.Requester.UserName, //{1}
                        "Approved",                         //{2}
                        userName                            //{3}
                    );

                    var mailData = new MailData
                    {
                        EmailToIds = new List<string> { existingProcess.Requester.Email },
                        EmailCCIds = new List<string> { "librarian@yopmail.com" },
                        EmailToName = existingProcess.Requester.UserName,
                        EmailSubject = "Welcome to Our Service!",
                        EmailBody = emailBody
                    };

                    _emailService.SendMail(mailData);
                }
                else if (process.Status == "Reject")
                {
                    existingProcess.Currentstepid = 5;
                    existingProcess.Status = "Rejected";

                    var workflow = new Workflowaction
                    {
                        Requestid = processId,
                        Stepid = 2,
                        Actorid = userId,
                        Action = existingProcess.Status,
                        Actiondate = DateTime.Now,
                        Comments = "Rejected By Librarian"
                    };

                    await _workflowActionRepository.AddWorkflowActionAsync(workflow);

                    var emailBody = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/ApprovalBookRequest.html");
                    emailBody = string.Format(emailBody,
                        "Borrowing Book Request",               //{0}
                        existingProcess.Requester.UserName,     //{1}
                        "Rejected",        //{2}
                        userName           //{3}
                    );

                    var mailData = new MailData
                    {
                        EmailToIds = new List<string> { existingProcess.Requester.Email },
                        EmailCCIds = new List<string> { "librarian@yopmail.com" },
                        EmailToName = existingProcess.Requester.UserName,
                        EmailSubject = "Welcome to Our Service!",
                        EmailBody = emailBody
                    };

                    try
                    {
                        _emailService.SendMail(mailData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email: {ex.Message}");
                    }
                }
            }
            else if (userRoles.Contains("Library Manager"))
            {
                existingProcess.Status = process.Status;
                if (process.Status == "Approve")
                {
                    existingProcess.Currentstepid = 5;
                    existingProcess.Status = "Approved";

                    var workflow = new Workflowaction
                    {
                        Requestid = processId,
                        Stepid = 3,
                        Actorid = userId,
                        Action = existingProcess.Status,
                        Actiondate = DateTime.Now,
                        Comments = "Approved By Library Manager"
                    };

                    await _workflowActionRepository.AddWorkflowActionAsync(workflow);

                    var emailBody = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/ApprovalBookRequest.html");
                    emailBody = string.Format(emailBody,
                        "Borrowing Book Request",               //{0}
                        existingProcess.Requester.UserName,     //{1}
                        "Approved",        //{2}
                        userName           //{3}
                    );

                    var mailData = new MailData
                    {
                        EmailToIds = new List<string> { existingProcess.Requester.Email },
                        EmailCCIds = new List<string> { "librarymanager@yopmail.com" },
                        EmailToName = existingProcess.Requester.UserName,
                        EmailSubject = "Welcome to Our Service!",
                        EmailBody = emailBody
                    };

                    try
                    {
                        _emailService.SendMail(mailData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email: {ex.Message}");
                        // Optionally, you can throw an exception or log it
                    }
                }
                else if (process.Status == "Reject")
                {
                    existingProcess.Currentstepid = 5;
                    existingProcess.Status = "Rejected";

                    var workflow = new Workflowaction
                    {
                        Requestid = processId,
                        Stepid = 2,
                        Actorid = userId,
                        Action = existingProcess.Status,
                        Actiondate = DateTime.Now,
                        Comments = "Rejected"
                    };

                    await _workflowActionRepository.AddWorkflowActionAsync(workflow);

                    var emailBody = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/ApprovalBookRequest.html");
                    emailBody = string.Format(emailBody,
                        "Borrowing Book Request",               //{0}
                        existingProcess.Requester.UserName,     //{1}
                        "Rejected",        //{2}
                        userName           //{3}
                    );

                    var mailData = new MailData
                    {
                        EmailToIds = new List<string> { existingProcess.Requester.Email },
                        EmailCCIds = new List<string> { "librarymanager@yopmail.com" },
                        EmailToName = existingProcess.Requester.UserName,
                        EmailSubject = "Welcome to Our Service!",
                        EmailBody = emailBody
                    };

                    try
                    {
                        _emailService.SendMail(mailData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email: {ex.Message}");
                        // Optionally, you can throw an exception or log it
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("User does not have the required role to approve or reject the request.");
            }
        }


    }
}
