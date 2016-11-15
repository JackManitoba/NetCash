using System.Drawing;

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
