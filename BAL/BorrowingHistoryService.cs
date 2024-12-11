using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BorrowingHistoryService : IBorrowingHistoryService
    {
        private readonly IBorrowingHistoryRepository _repository;

        public BorrowingHistoryService(IBorrowingHistoryRepository repository)
        {
            _repository = repository;
        }

        public double CalculateLateFee(int borrowingHistoryId)
        {
            var history = _repository.GetBorrowingHistoryById(borrowingHistoryId);
            if (history == null || history.BorrowedDate == null)
            {
                throw new ArgumentException("Invalid borrowing history ID");
            }

            var dueDate = history.BorrowedDate.AddDays(14); // Assuming 14 days borrowing period
            var today = DateTime.UtcNow;

            if (today <= dueDate)
            {
                return 0; // No late fee
            }

            var daysLate = (today - dueDate).Days;
            const double lateFeePerDay = 2.0; // Example late fee per day
            return daysLate * lateFeePerDay;
        }

        public void ReturnBook(int borrowingHistoryId)
        {
            var history = _repository.GetBorrowingHistoryById(borrowingHistoryId);
            if (history == null)
            {
                throw new ArgumentException("Invalid borrowing history ID");
            }

            history.ReturnedDate = DateTime.UtcNow;
            history.LateFee = CalculateLateFee(borrowingHistoryId);

            _repository.UpdateBorrowingHistory(history);
        }
    }
}
