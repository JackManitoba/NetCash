using NetCashATM.Presenters;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
    public class PrintInfo : ATMPanel
    {
        private PrintInfoPresenter _printInfoPresenter;
        private Label _messageLabel;

        public PrintInfo()
        {
            CreateChildControls();
            _printInfoPresenter = new PrintInfoPresenter(this);

        }

        public override void CreateChildControls()
        {
            BackColor = System.Drawing.Color.White;
            Location = new System.Drawing.Point(109, 57);

            Size = new System.Drawing.Size(351, 194);
            Name = "PrintInfo";

            _messageLabel = new Label();
            _messageLabel.Text = "Printing";
            _messageLabel.SetBounds(((Width / 2) - 50), (Height / 2 + 10), 150, 40);
            Controls.Add(_messageLabel);
        }

        public override void Enter()
        {
            _printInfoPresenter.GoToMainMenu();
        }

        public void SetMessage(string message)
        {
            _messageLabel.Text = message;
            _messageLabel.Update();
        }

        public void SetFileName()
        {
            _printInfoPresenter.PrintStatement();
        }

    }
}

