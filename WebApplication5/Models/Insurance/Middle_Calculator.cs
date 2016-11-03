using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.Insurance
{
    public class Middle_Calculator : IInsuranceStrategy
    {
      
        public double calculatequte(int province, int insuranceType)
        {
            return province * 200 + insuranceType * 800;
        }
    }
}