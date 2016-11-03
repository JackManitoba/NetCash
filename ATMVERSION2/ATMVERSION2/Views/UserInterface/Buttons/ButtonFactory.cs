using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.UserInterface.Buttons
{
    class ButtonFactory
    {
        public ButtonFactory() { Console.WriteLine("Button factory created"); }

        public ATMButton getButton(string identifier)
        {
            ATMButton b = null;
            switch (identifier)
            {
                case "Enter":
                    {
                        FunctionButton b1 = new FunctionButton();

                        b1.Text = identifier;
                        b1.setColour("Lime");
                        b1.Location = new System.Drawing.Point(385, 359);
                        b1.Name = identifier;
                        b1.Size = new System.Drawing.Size(75, 45);
                        b1.TabIndex = 11;
                        b = b1;
                        break;
                    }
                case "Cancel":
                    {
                        FunctionButton b1 = new FunctionButton();

                        b1.Text = identifier;
                        b1.setColour("Red");
                        b1.Location = new System.Drawing.Point(385, 257);
                        b1.Name = "button10";
                        b1.Size = new System.Drawing.Size(75, 45);
                        b1.TabIndex = 9;
                        b = b1;
                        break;
                    }
                case "Clear":
                    {
                        FunctionButton b1 = new FunctionButton();

                        b1.Text = identifier;
                        b1.setColour("Yellow");
                        b1.Location = new System.Drawing.Point(385, 308);
                        b1.Name = identifier;
                        b1.Size = new System.Drawing.Size(75, 45);
                        b1.TabIndex = 10;
                        b = b1;
                        break;
                    }

                default:
                    {
                        b = new NumberButton(identifier); break;
                    }
            }
            return b;
        }
    }
}
