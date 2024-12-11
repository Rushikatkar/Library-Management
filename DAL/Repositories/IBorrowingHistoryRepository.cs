using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IBorrowingHistoryRepository
    {
        BorrowingHistory GetBorrowingHistoryById(int id);
        void UpdateBorrowingHistory(BorrowingHistory history);
    }
}
