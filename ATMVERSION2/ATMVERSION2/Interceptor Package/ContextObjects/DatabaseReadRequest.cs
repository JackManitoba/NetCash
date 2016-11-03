using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Interceptor_Package
{
   public class DataBaseReadRequest : ContextObject
    {
        public string getObj()
        {
            return "Framework Attempt to READ database, this context object has been created as a result"; 
        }
    }
}
