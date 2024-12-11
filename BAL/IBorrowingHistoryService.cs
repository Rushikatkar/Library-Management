using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface IBorrowingHistoryService
    {
        double CalculateLateFee(int borrowingHistoryId);
        void ReturnBook(int borrowingHistoryId);
    }

}
