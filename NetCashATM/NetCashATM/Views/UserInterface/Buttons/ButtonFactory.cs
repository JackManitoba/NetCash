using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.UserInterface.Buttons
{
    class ButtonFactory
    {
        public ButtonFactory() { Console.WriteLine("Button factory created"); }

        public ATMButton GetButton(string buttonName)
        {
            ATMButton atmButton = null;

            switch (buttonName)
            {
                case "Enter":
                    {
                        FunctionButton b1 = new FunctionButton();

                        b1.Text = buttonName;
                        b1.SetColour("Lime");
                        b1.Location = new System.Drawing.Point(385, 359);
                        b1.Name = buttonName;
                        b1.Size = new System.Drawing.Size(75, 45);
                        b1.TabIndex = 11;
                        atmButton = b1;
                        break;
                    }

                case "Cancel":
                    {
                        FunctionButton b1 = new FunctionButton();

                        b1.Text = buttonName;
                        b1.SetColour("Red");
                        b1.Location = new System.Drawing.Point(385, 257);
                        b1.Name = "button10";
                        b1.Size = new System.Drawing.Size(75, 45);
                        b1.TabIndex = 9;
                        atmButton = b1;
                        break;
                    }

                case "Clear":
                    {
                        FunctionButton b1 = new FunctionButton();

                        b1.Text = buttonName;
                        b1.SetColour("Yellow");
                        b1.Location = new System.Drawing.Point(385, 308);
                        b1.Name = buttonName;
                        b1.Size = new System.Drawing.Size(75, 45);
                        b1.TabIndex = 10;
                        atmButton = b1;
                        break;
                    }

                default:
                    {
                        atmButton = new NumberButton(buttonName); break;
                    }
            }
            return atmButton;
        }
    }
}
