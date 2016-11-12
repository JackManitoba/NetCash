using NetCashATM.Interfaces;
using NetCashATM.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
    public interface ATMPanel : Observer, Subject, Reciever
    {
        void CreateChildControls();

        TextBox GetInput();
    }
}
