using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.Interfaces
{
    public interface Observer
    {
        List<Subject> SubjectList { get; }

        void Update(Subject e);
    }
}
