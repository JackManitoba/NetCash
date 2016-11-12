using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.BankTransactions
{
   public interface Transaction
    {
        string GetType();
        int GetAmount();

        void PerformTransaction();
        bool AreFundsAvailable();
    }
}
