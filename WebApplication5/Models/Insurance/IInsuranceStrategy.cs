using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Models.Insurance
{
    interface IInsuranceStrategy
    {
        double calculatequte(int province , int insuranceType);
    }
}
