using System;


namespace BankingFramework.InterceptorPackage.ContextObjects
{
   public class DataBaseReadRequest : ContextObject
    {
        private string _source, _description;

        public DataBaseReadRequest(string source, string description)
        {
            _source = source;
            _description = description;
        }

        public string GetObj()
        {
            return "DataBaseReadRequest Object";
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
