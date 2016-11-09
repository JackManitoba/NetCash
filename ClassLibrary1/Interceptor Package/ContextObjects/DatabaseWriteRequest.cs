﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Interceptor_Package
{
    public class DatabaseWriteRequest : ContextObject
    {
            string source;
            string description;

        public DatabaseWriteRequest(string source, string description)
        {
            this.source = source; this.description = description;
        }

        public string getObj()
        {
            return "DataBaseWriteRequest Object";
        }

        public string getSource()
        { return this.source; }

        public string getShortDescription()
        { return this.description; }

        public string getVerboseDescription()
        { return getObj() + " " + source + " " + description + DateTime.Now; }
    }


}