using ATMVERSION2.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATMVERSION2.Interfaces
{
    public interface Controller
    {
        void setPanel(ATMPanel panel);
       
        void resetAccountPin(string newPin);
    }
}
