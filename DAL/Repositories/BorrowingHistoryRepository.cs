using DAL.Models;
using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BorrowingHistoryRepository : IBorrowingHistoryRepository
    {
        private readonly LibraryManagementDbContext _context;

        public BorrowingHistoryRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public BorrowingHistory GetBorrowingHistoryById(int id)
        {
            return _context.BorrowingHistories.Include(b => b.Book).FirstOrDefault(b => b.Id == id);
        }

        public void UpdateBorrowingHistory(BorrowingHistory history)
        {
            _context.BorrowingHistories.Update(history);
            _context.SaveChanges();
        }
    }
}
