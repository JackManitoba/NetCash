using ATMVERSION2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.HelperClasses
{
    class RegistrationHelper
    {
        public RegistrationHelper()
        { }
        public void registerObserverToSubject(Observer e, Subject i)
        {
            i.registerObserver(e);
        }
        public void unregisterObserverToSubject(Observer e, Subject i)
        {
            i.unregisterObserver(e);
        }
    }
}
