using Assignment5.Persistence.Context;
using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Persistence.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryContext _context;

        public BorrowRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Borrow> GetBorrowById(int borrowId)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Borrowid == borrowId);
        }

        public async Task<IEnumerable<Borrow>> GetAllBorrows()
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task AddBorrow(Borrow borrow)
        {
            await _context.Borrows.AddAsync(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBorrow(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBorrow(int borrowId)
        {
            var existingBorrow = await _context.Borrows.FindAsync(borrowId);
            if (existingBorrow != null)
            {
                _context.Borrows.Remove(existingBorrow);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ReturnBook(int borrowId)
        {
            var existingBorrow = await _context.Borrows.FindAsync(borrowId);
            if (existingBorrow == null)
            {
                return false;
            }

            existingBorrow.Dateofreturn = DateOnly.FromDateTime(DateTime.UtcNow);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
