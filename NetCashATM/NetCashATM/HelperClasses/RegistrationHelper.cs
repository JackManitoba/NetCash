using NetCashATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.HelperClasses
{
    class RegistrationHelper
    {
        public RegistrationHelper(){ }

        public void registerObserverToSubject(Observer e, Subject i)
        {
            i.RegisterObserver(e);
        }
        public void UnregisterObserverToSubject(Observer e, Subject i)
        {
            i.UnregisterObserver(e);
        }
    }
}
