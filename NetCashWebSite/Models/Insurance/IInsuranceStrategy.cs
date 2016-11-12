using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashWebSite.Models.Insurance
{
    interface IInsuranceStrategy
    {
        double calculatequote(int ageBracket , int Location);
    }
}
