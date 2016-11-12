﻿using NetCashATM.ConcreteCommands;
using NetCashATM.HelperClasses;
using NetCashATM.UserInterface.Buttons;
using NetCashATM.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using NetCashATM.Interfaces;

namespace NetCashATM.Views
{
    public class ATMMainView : Form , Interfaces.View , Observer, Subject
    {
        public ATMMainView() {
            ObserversList = new List<Observer>();
            SubjectsList = new List<Subject>();
            InitializeComponent(); }
        private System.ComponentModel.IContainer components = null;


        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._bf = new ButtonFactory();
            this._helperClass = new RegistrationHelper();

            this._cancelButton = _bf.GetButton("Cancel");
            this._clearButton = _bf.GetButton("Clear");
            this._enterButton = _bf.GetButton("Enter");

            CancelCommand = new CancelCommand(_currentPanel);
            ClearCommand = new ClearCommand(_currentPanel);
            EnterCommand = new EnterCommand(_currentPanel);

            this._keypadButtons = new List<ATMButton>();

            this.SuspendLayout();

            //CANCEL BUTTON
            this._cancelButton.Click += new System.EventHandler(this.Cancel_Button_Click);

            //CLEAR
            this._clearButton.Click += new System.EventHandler(this.Clear_Button_Click);

            // ENTER         
            this._enterButton.Click += new System.EventHandler(this.Enter_Button_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 481);

            this.Controls.Add(this._currentPanel);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._clearButton);
            this.Controls.Add(this._enterButton);

            this.Name = "Form1";
            this.Text = "NetCash ATM";
            this.ResumeLayout(false);

            for (int i = 0; i < 10; i++)
            {
                this._keypadButtons.Add(_bf.GetButton("" + i));
                
                this._keypadButtons[i].Click += new System.EventHandler(this.button_Click);
                this.Controls.Add(_keypadButtons[i]);
            }
        }

        //USED TO NOTIFY AN OBSERVER ONCE A SUBJECT HAS BEEN ACTED UPON
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
          
               
                unRegisterButtonsWithPanel();
                this.Controls.Remove(_currentPanel);
            
            
            this._currentPanel = p;
            this.Controls.Add(_currentPanel);
            RegisterButtonsWithPanel();
            this._currentPanel.RegisterObserver(this);            
        }
        public void  RegisterButtonsWithPanel()
        {
            CancelCommand = new CancelCommand(this._currentPanel);
            ClearCommand = new ClearCommand(this._currentPanel);
            EnterCommand = new EnterCommand(this._currentPanel);
            for (int i = 0; i < 10; i++)
            {
                _helperClass.registerObserverToSubject(this._currentPanel, this._keypadButtons[i]);                
            }
        }

        public void unRegisterButtonsWithPanel()
        {
            for (int i = 0; i < 10; i++)
            {
                _helperClass.UnregisterObserverToSubject(this._currentPanel, this._keypadButtons[i]);
            }
        }

        public ATMPanel GetCurrentPanel() { return this._currentPanel; }

        public NavigationDataClass getNavigationClass() { return this._currentPanel.NavData; }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Update(Subject e)
        {
            NotifyObservers();
        }

        public void NotifyObservers()
        {
            foreach (Observer o in ObserversList)
            { o.Update(this); }
        }

        public void RegisterObserver(Observer e)
        {
            this.ObserversList.Add(e);
        }

        public void UnregisterObserver(Observer e)
        {
            this.ObserversList.Remove(e);
        }

        #endregion
        private List<ATMButton> _keypadButtons;

#pragma warning disable CS0108 // 'ATMMainView.CancelButton' hides inherited member 'Form.CancelButton'. Use the new keyword if hiding was intended.
        private ATMButton _cancelButton;
#pragma warning restore CS0108 // 'ATMMainView.CancelButton' hides inherited member 'Form.CancelButton'. Use the new keyword if hiding was intended.
        private ATMButton _clearButton;
        private ATMButton _enterButton;
        private ATMPanel _currentPanel;
        private ButtonFactory _bf;
        private RegistrationHelper _helperClass;
        CancelCommand CancelCommand;
        ClearCommand ClearCommand;
        EnterCommand EnterCommand;
        List<Observer> ObserversList;
        List<Subject> SubjectsList;

        public List<Subject> SubjectList
        {
            get
            {
                return SubjectsList;
            }
        }

        public List<Observer> ObserverList
        {
            get
            {
                return ObserversList;
            }
        }
    }
}