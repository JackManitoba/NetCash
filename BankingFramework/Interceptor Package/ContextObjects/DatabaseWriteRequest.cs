using System;


namespace BankingFramework.Interceptor_Package
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
    }
}
