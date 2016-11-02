using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.UserInterface.Buttons
{
    class FunctionButton : ATMButton
    {

        public FunctionButton() { }
        

        public void setColour(string colour)
        {
            Color x = Color.FromName(colour);
            this.BackColor = x;
            this.UseVisualStyleBackColor = false;
        }
    }
}
