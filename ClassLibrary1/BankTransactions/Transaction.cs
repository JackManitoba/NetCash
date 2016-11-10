using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.BankTransactions
{
   public interface Transaction
    {
        string type();
        int amount();
     }
}
