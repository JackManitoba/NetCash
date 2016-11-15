using BankingFramework.Logging;
using System;


namespace BankingFramework.InterceptorPackage.ContextObjects
{
    public class DatabaseWriteContextObject : ContextObject
    {
        string _source;
        string _description;

        public DatabaseWriteContextObject(string source, string description)
        {
            _source = source;
            _description = description;
        }

        public string GetObj()
        {
            return "DataBaseWriteRequest Object";
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
