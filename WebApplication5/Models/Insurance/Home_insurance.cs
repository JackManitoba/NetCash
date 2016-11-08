using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.Insurance
{
    public class Home_insurance : IInsuranceStrategy
    {

        public double calculatequote(int ageBracket, int Location)
        {
            return Location * 400 + ageBracket * 400;
        }
    
    }
}