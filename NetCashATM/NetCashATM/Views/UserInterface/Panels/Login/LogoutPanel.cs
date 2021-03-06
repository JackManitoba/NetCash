﻿using NetCashATM.Observers;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
    public class LogoutPanel : ATMPanel
    {
        private LogoutPresenter _logoutPresenter;
        private static Label _netCashLabel;
        private static Label _message;
      
        public LogoutPanel()
        {
            CreateChildControls();
            _logoutPresenter = new LogoutPresenter();
        }

        public override void CreateChildControls()
        {
            Name = "LogoutPanel";
            BackColor = System.Drawing.Color.White;
            Location = new System.Drawing.Point(109, 57);

            Size = new System.Drawing.Size(351, 194);
            TabIndex = 12;

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            Controls.Add(_netCashLabel);

            _message = new Label();
            _message.Text = "PLEASE TAKE YOUR CARD, PRESS ENTER TO CONTINUE";
            _message.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 10), 150, 40);
            Controls.Add(_message);
        }

        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;    
        }

        public override void Cancel()
        {
            _logoutPresenter.Logout();
        }

        public override void Clear()
        {
            _logoutPresenter.Logout();
        }

        public override void Enter()
        {
            _logoutPresenter.Logout();
        }
    }
}