using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Interceptor_Package
{
   public interface ContextObject
    {
        string getObj();
        string getVerboseDescription();
        string getShortDescription();
    }
}
