using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.Insurance
{
    public class Young_Calculator : IInsuranceStrategy
    {
    
        public double calculatequte(int province, int insuranceType)
        {
            return province * 50   + insuranceType * 25 + 600;
        }
    }
}