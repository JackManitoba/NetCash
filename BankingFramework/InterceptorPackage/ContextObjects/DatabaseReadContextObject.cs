using BankingFramework.Logging;
using System;


namespace BankingFramework.InterceptorPackage.ContextObjects
{
   public class DataBaseReadContextObject : ContextObject
    {
        private string _source, _description;

        public DataBaseReadContextObject(string source, string description)
        {
            _source = source;
            _description = description;
        }

        public string GetObj()
        {
            return "DataBaseReadRequest Object";
        }

        public string GetShortDescription()
        {
            return _description;
        }

        public string GetVerboseDescription()
        {
            return GetObj() + " " + _source + " " + _description + DateTime.Now;
        }

        public void Service()
        {
            Logger l = new Logger();
            l.LogDatabaseInteractions(this.GetVerboseDescription());
        }  
    }
}
