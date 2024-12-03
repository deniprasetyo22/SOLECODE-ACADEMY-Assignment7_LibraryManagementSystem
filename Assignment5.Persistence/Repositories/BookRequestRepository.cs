using Assignment5.Persistence.Context;
using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment7.Persistence.Repositories
{
    public class BookRequestRepository:IBookRequestRepository
    {
        private readonly LibraryContext _context;
        public BookRequestRepository(LibraryContext context)
        {
            _context = context; 
        }

        public async Task<Bookrequest> AddBookRequestAsync(Bookrequest bookRequest)
        {
            _context.Bookrequests.Add(bookRequest);
            await _context.SaveChangesAsync();
            return bookRequest;
        }

        public async Task<IEnumerable<Bookrequest>> GetAllBookRequestsAsync()
        {
            return await _context.Bookrequests
                .Include(b=>b.Process).ThenInclude(b=>b.Workflowactions)
                .Include(b => b.Process).ThenInclude(b => b.Requester)
                .ToListAsync();
        }

        public async Task<Bookrequest> GetBookRequestByIdAsync(int id)
        {
            return await _context.Bookrequests.FindAsync(id);
        }

    }
}
