using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Interceptor_Package
{
    public class DatabaseWriteRequest : ContextObject
    {

        
       public string getObj()
        {
            return "Framework Database WRITE has been attempted, this object has been created as a result";
        }
    }


}
