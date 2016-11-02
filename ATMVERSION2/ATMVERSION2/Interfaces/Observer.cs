using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.Interfaces
{
    public interface Observer
    {
        List<Subject> subjectList { get; }

        void update();
        void update(Subject e);
    }
}
