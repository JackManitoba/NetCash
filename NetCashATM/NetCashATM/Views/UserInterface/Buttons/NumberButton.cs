using System;

namespace NetCashATM.UserInterface.Buttons
{
    class NumberButton : ATMButton
    {
        public int[,] ButtonPositions = new int[10, 2] { { 109, 257 }, { 190, 257 }, { 271, 257 }, { 109, 308 }, { 190, 308 }, { 271, 308 }, { 109, 359 }, { 190, 359 }, { 271, 359 }, { 190, 410 } };

        public NumberButton(string number)
        {
            Name = "button" + number;
            Text = number;
            UseVisualStyleBackColor = true;
            Size = new System.Drawing.Size(75, 45);

            if (number == "0")
            {
                TabIndex = (13);
                Location = new System.Drawing.Point(ButtonPositions[9, 0], ButtonPositions[9, 1]);
            }
            else
            {
                TabIndex = (Int32.Parse(number) - 1);
                Location = new System.Drawing.Point(ButtonPositions[(Int32.Parse(number) - 1), 0], ButtonPositions[(Int32.Parse(number) - 1), 1]);
            }
        }
    }
}
