using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.Interceptor_Package
{
   public interface ContextObject
    {
        string GetObj();
        string GetVerboseDescription();
        string GetShortDescription();
    }
}
