using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Interfaces.IRepositories
{
    public interface IBorrowRepository
    {
        Task<Borrow> GetBorrowById(int borrowId);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task AddBorrow(Borrow borrow);
        Task UpdateBorrow(Borrow borrow);
        Task DeleteBorrow(int borrowId);
        Task<bool> ReturnBook(int borrowId);

    }
}
