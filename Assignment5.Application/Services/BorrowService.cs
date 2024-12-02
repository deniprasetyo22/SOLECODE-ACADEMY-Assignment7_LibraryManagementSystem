using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        private const int MaxBookBorrowed = 3; // Adjust as needed
        private const int DurationBookLoans = 14; // Adjust as needed

        public BorrowService(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public async Task AddBorrow(Borrow borrow)
        {
            // Check if the borrow object is null
            if (borrow == null)
            {
                throw new ArgumentException("Invalid borrow data.");
            }

            // Check if the book is already borrowed and not returned
            var existingBorrows = await _borrowRepository.GetAllBorrows();
            if (existingBorrows.Any(b => b.Bookid == borrow.Bookid && b.Dateofreturn == null))
            {
                throw new InvalidOperationException("The book is already borrowed and not returned yet.");
            }

            // Validate the number of books currently borrowed by the user
            var borrowedBooksCount = existingBorrows.Count(b => b.Userid == borrow.Userid && b.Dateofreturn == null);
            if (borrowedBooksCount >= MaxBookBorrowed)
            {
                throw new InvalidOperationException($"User has already borrowed the maximum number of books ({MaxBookBorrowed}).");
            }

            // Ensure the DateOfBorrow is provided
            if (!borrow.Dateofborrow.HasValue)
            {
                throw new ArgumentException("The borrow date must be provided.");
            }

            // Calculate and set the deadline for returning the book
            borrow.Deadlinereturn = borrow.Dateofborrow.Value.AddDays(DurationBookLoans);

            // Add the borrow record to the database
            await _borrowRepository.AddBorrow(borrow);
        }

        public async Task<IEnumerable<Borrow>> GetAllBorrows()
        {
            return await _borrowRepository.GetAllBorrows();
        }

        public async Task<Borrow> GetBorrowById(int borrowId)
        {
            var borrow = await _borrowRepository.GetBorrowById(borrowId);
            if (borrow == null)
            {
                throw new KeyNotFoundException("Borrow record not found.");
            }
            return borrow;
        }

        public async Task ReturnBook(int borrowId)
        {
            var success = await _borrowRepository.ReturnBook(borrowId);
            if (!success)
            {
                throw new KeyNotFoundException("Borrow record not found for return.");
            }
        }

        public async Task UpdateBorrow(int borrowId, Borrow updatedBorrow)
        {
            var existingBorrow = await _borrowRepository.GetBorrowById(borrowId);
            if (existingBorrow == null)
            {
                throw new KeyNotFoundException("Borrow record not found.");
            }

            // Validate that the updated borrow record meets the requirements
            var borrowedBooksCount = await _borrowRepository.GetAllBorrows();
            if (borrowedBooksCount.Count(b => b.Userid == updatedBorrow.Userid && b.Dateofreturn == null && b.Borrowid != borrowId) >= MaxBookBorrowed)
            {
                throw new InvalidOperationException("User has already borrowed the maximum number of books.");
            }

            // Update borrow details
            existingBorrow.Userid = updatedBorrow.Userid;
            existingBorrow.Bookid = updatedBorrow.Bookid;
            existingBorrow.Dateofborrow = updatedBorrow.Dateofborrow;
            existingBorrow.Deadlinereturn = updatedBorrow.Dateofborrow?.AddDays(DurationBookLoans);
            existingBorrow.Dateofreturn = updatedBorrow.Dateofreturn;

            await _borrowRepository.UpdateBorrow(existingBorrow);
        }

        public async Task DeleteBorrow(int borrowId)
        {
            var existingBorrow = await _borrowRepository.GetBorrowById(borrowId);
            if (existingBorrow == null)
            {
                throw new KeyNotFoundException("Borrow record not found.");
            }

            await _borrowRepository.DeleteBorrow(borrowId);
        }
    }

}
