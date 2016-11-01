using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.UserInterface.Buttons
{
    class NumberButton : ATMButton
    {
        int[,] ButtonPositions = new int[10, 2] { { 109, 257 }, { 190, 257 }, { 271, 257 }, { 109, 308 }, { 190, 308 }, { 271, 308 }, { 109, 359 }, { 190, 359 }, { 271, 359 }, { 190, 410 } };
        public NumberButton(string number)
        {
            this.Name = "button" + number;
            this.Text = number;
            this.UseVisualStyleBackColor = true;
            this.Size = new System.Drawing.Size(75, 45);
            if (number == "0")
            {
                this.TabIndex = (13);
                this.Location = new System.Drawing.Point(ButtonPositions[9, 0], ButtonPositions[9, 1]);
            }
            else
            {
                this.TabIndex = (Int32.Parse(number) - 1);
                this.Location = new System.Drawing.Point(ButtonPositions[(Int32.Parse(number) - 1), 0], ButtonPositions[(Int32.Parse(number) - 1), 1]);
            }

        }
    }
}
