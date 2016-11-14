using System.Collections.Generic;

namespace NetCashATM.Observers
{
    public interface Observer
    {
        List<Subject> SubjectList { get; }

        void Update(Subject e);
    }
}
