using ATMVERSION2.ConcreteCommands;
using ATMVERSION2.HelperClasses;
using ATMVERSION2.UserInterface.Buttons;
using ATMVERSION2.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ATMVERSION2.Interfaces;

namespace ATMVERSION2.Views
{
    public class ATMMainView : Form , Interfaces.View , Observer, Subject
    {
        public ATMMainView() {
            observersList = new List<Observer>();
            subjectsList = new List<Subject>();
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
            this.bf = new ButtonFactory();
            this.helperClass = new RegistrationHelper();

            this.CancelButton = bf.getButton("Cancel");
            this.ClearButton = bf.getButton("Clear");
            this.EnterButton = bf.getButton("Enter");

            cancelCommand = new CancelCommand(currentPanel);
            clearCommand = new ClearCommand(currentPanel);
            enterCommand = new EnterCommand(currentPanel);

            this.keypadButtons = new List<ATMButton>();

            this.SuspendLayout();

            //CANCEL BUTTON
            this.CancelButton.Click += new System.EventHandler(this.Cancel_Button_Click);

            //CLEAR
            this.ClearButton.Click += new System.EventHandler(this.Clear_Button_Click);

            // ENTER         
            this.EnterButton.Click += new System.EventHandler(this.Enter_Button_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 481);

            this.Controls.Add(this.currentPanel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.EnterButton);

            this.Name = "Form1";
            this.Text = "NetCash ATM";
            this.ResumeLayout(false);

            for (int i = 0; i < 10; i++)
            {
                this.keypadButtons.Add(bf.getButton("" + i));
                
                this.keypadButtons[i].Click += new System.EventHandler(this.button_Click);
                this.Controls.Add(keypadButtons[i]);
            }
        }

        //USED TO NOTIFY AN OBSERVER ONCE A SUBJECT HAS BEEN ACTED UPON
        private void button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.notifyObservers();
        }
        private void Enter_Button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.executeCommand(enterCommand);
        }
        private void Clear_Button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.executeCommand(clearCommand);
        }
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            ATMButton b = (ATMButton)sender;
            b.executeCommand(cancelCommand);
        }
        public void setCurrentPanel(ATMPanel p)
        {
            unRegisterButtonsWithPanel();
            this.Controls.Remove(currentPanel);
            Debug.WriteLine("Setting new panel");
            this.currentPanel = p;
            this.Controls.Add(currentPanel);
            registerButtonsWithPanel();
            this.currentPanel.registerObserver(this);            
        }
        public void  registerButtonsWithPanel()
        {
            cancelCommand = new CancelCommand(this.currentPanel);
            clearCommand = new ClearCommand(this.currentPanel);
            enterCommand = new EnterCommand(this.currentPanel);
            for (int i = 0; i < 10; i++)
            {
               helperClass.registerObserverToSubject(this.currentPanel, this.keypadButtons[i]);                
            }
        }

        public void unRegisterButtonsWithPanel()
        {
            for (int i = 0; i < 10; i++)
            {
                helperClass.unregisterObserverToSubject(this.currentPanel, this.keypadButtons[i]);
            }
        }

        public ATMPanel getCurrentPanel() { return this.currentPanel; }

        public NavigationDataClass getNavigationClass() { return this.currentPanel.navData; }

        public void update()
        {
            throw new NotImplementedException();
        }

        public void update(Subject e)
        {
            notifyObservers();
        }

        public void notifyObservers()
        {
            foreach (Observer o in observersList)
            { o.update(this); }
        }

        public void registerObserver(Observer e)
        {
            this.observersList.Add(e);
        }

        public void unregisterObserver(Observer e)
        {
            this.observersList.Remove(e);
        }

        #endregion
        private List<ATMButton> keypadButtons;

        private ATMButton CancelButton;
        private ATMButton ClearButton;
        private ATMButton EnterButton;
        private ATMPanel currentPanel;
        private ButtonFactory bf;
        private RegistrationHelper helperClass;
        CancelCommand cancelCommand;
        ClearCommand clearCommand;
        EnterCommand enterCommand;
        List<Observer> observersList;
        List<Subject> subjectsList;

        public List<Subject> subjectList
        {
            get
            {
                return this.subjectsList;
            }
        }

        public List<Observer> observerList
        {
            get
            {
                return this.observersList;
            }
        }
    }
}
