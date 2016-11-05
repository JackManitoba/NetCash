using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.Insurance
{
    public class Travel_Insurance : IInsuranceStrategy
    {

        public double calculatequte(int Location, int ageBracket)
        {
            return Location * 50 + ageBracket * 50;
        }
    
    }
}