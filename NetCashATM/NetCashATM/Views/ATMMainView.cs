using NetCashATM.Commands;
using NetCashATM.UserInterface.Buttons;
using NetCashATM.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NetCashATM.Observers;

namespace NetCashATM.Views
{
    public class ATMMainView : Form 
    {
        public ATMMainView()
        {
            InitializeComponent();
        }

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
            _bf = new ButtonFactory();
            _helperClass = new RegistrationHelper();

            _cancelButton = _bf.GetButton("Cancel");
            _clearButton = _bf.GetButton("Clear");
            _enterButton = _bf.GetButton("Enter");

            CancelCommand = new CancelCommand(_currentPanel);
            ClearCommand = new ClearCommand(_currentPanel);
            EnterCommand = new EnterCommand(_currentPanel);

            _keypadButtons = new List<ATMButton>();

            SuspendLayout();

            _cancelButton.Click += new EventHandler(this.Cancel_Button_Click);
            _clearButton.Click += new EventHandler(this.Clear_Button_Click);      
            _enterButton.Click += new EventHandler(this.Enter_Button_Click);

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(560, 481);

            Controls.Add(_currentPanel);
            Controls.Add(_cancelButton);
            Controls.Add(_clearButton);
            Controls.Add(_enterButton);

            Name = "Form1";
            Text = "NetCash ATM";
            ResumeLayout(false);

            for (int i = 0; i < 10; i++)
            {
                _keypadButtons.Add(_bf.GetButton(i.ToString()));            
                _keypadButtons[i].Click += new EventHandler(button_Click);
                Controls.Add(_keypadButtons[i]);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.NotifyObservers();
        }

        private void Enter_Button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.ExecuteCommand(EnterCommand);
        }
        private void Clear_Button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.ExecuteCommand(ClearCommand);
        }
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.ExecuteCommand(CancelCommand);
        }

        public void SetCurrentPanel(ATMPanel p)
        {        
            UnRegisterButtonsWithPanel();
            Controls.Remove(_currentPanel);
            _currentPanel = p;
            Controls.Add(_currentPanel);
            RegisterButtonsWithPanel();                    
        }

        private void RegisterButtonsWithPanel()
        {
            CancelCommand = new CancelCommand(this._currentPanel);
            ClearCommand = new ClearCommand(this._currentPanel);
            EnterCommand = new EnterCommand(this._currentPanel);
            for (int i = 0; i < 10; i++)
            {
                _helperClass.registerObserverToSubject(this._currentPanel, this._keypadButtons[i]);                
            }
        }

        public void UnRegisterButtonsWithPanel()
        {
            for (int i = 0; i < 10; i++)
            {
                if (_currentPanel != null)
                {
                    _helperClass.UnregisterObserverToSubject(_currentPanel, this._keypadButtons[i]);
                }
            }
        }

        private List<ATMButton> _keypadButtons;
        private ATMButton _cancelButton;
        private ATMButton _clearButton;
        private ATMButton _enterButton;
        private ATMPanel _currentPanel;
        private ButtonFactory _bf;
        private RegistrationHelper _helperClass;
        CancelCommand CancelCommand;
        ClearCommand ClearCommand;
        EnterCommand EnterCommand;
    }
}
