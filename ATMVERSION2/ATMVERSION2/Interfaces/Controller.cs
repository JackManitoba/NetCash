﻿using ATMVERSION2.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMVERSION2.Models;

namespace ATMVERSION2.Interfaces
{
    public interface Controller
    {
        void setPanel(ATMPanel panel);
        void performWithdrawal(Withdrawal withdrawal);
        void performDeposit(Deposit deposit);
        void resetAccountPin(string newPin);
    }
}