using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Interfaces.IService
{
    public interface IBookRequestService
    {
        Task<Bookrequest> AddBookRequestAsync(Bookrequest bookRequest);
        Task ApprovalAsync(int processId, Process process);
        Task<IEnumerable<Bookrequest>> GetAllBookRequestsAsync();
        Task<Bookrequest> GetBookRequestByIdAsync(int id);
    }
}
