using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Interceptor_Package
{
   public class DataBaseReadRequest : ContextObject
    {
        string source, description;
        DataBaseReadRequest(string source, string description)
        {
            this.source = source;this.description = description;
        }
        public string getObj()
        {
            return "DataBaseReadRequest Object"; 
        }
        public string getSource()
        { return this.source; }
        public string getShortDescription()
        { return this.description; }
        public string getVerboseDescription()
        { return getObj()+" "+ source + " " + description+ DateTime.Now; }
    }
}
