using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.UserInterface.Buttons
{
    public class FunctionButton : ATMButton
    {
        public FunctionButton() { }
        
        public void SetColour(string colorName)
        {
            Color color = Color.FromName(colorName);
            this.BackColor = color;
            this.UseVisualStyleBackColor = false;
        }
    }
}
