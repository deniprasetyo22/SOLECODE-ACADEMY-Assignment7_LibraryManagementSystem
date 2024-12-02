using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Interfaces.IService
{
    public interface IBorrowService
    {
        Task AddBorrow(Borrow borrow);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int borrowId);
        Task ReturnBook(int borrowId);
        Task UpdateBorrow(int borrowId, Borrow updatedBorrow);
        Task DeleteBorrow(int borrowId);
    }

}
