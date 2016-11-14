using BankingFramework.Utils;
using System;


namespace BankingFramework.InterceptorPackage.ContextObjects
{
    public class DatabaseWriteRequest : ContextObject
    {
        string _source;
        string _description;

        public DatabaseWriteRequest(string source, string description)
        {
            _source = source;
            _description = description;
        }

        public string GetObj()
        {
            return "DataBaseWriteRequest Object";
        }

        public string GetSource()
        {
            return _source;
        }

        public string GetShortDescription()
        {
            return _description;
        }

        public string GetVerboseDescription()
        {
            return GetObj() + " " + _source + " " + _description + DateTime.Now;
        }

        public void service()
        {
            Logger l = new Logger();
            l.LogDatabaseInteractions(this.GetVerboseDescription());
        }
    }
}
