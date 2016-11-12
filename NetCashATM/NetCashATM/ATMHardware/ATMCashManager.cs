using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using BankingFramework.Utils;

namespace NetCashATM.ATMHardware
{
    class ATMCashManager
    {
        private double _totalATMCash = 0;
        private int[] _noteCounts = new int[6];
      
        public ATMCashManager()
        {      
            SetLocalCash();
        }

        private void SetLocalCash()
        {
            string[] notes = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            double[] noteValues = { 10, 20, 50, 100, 200, 500 };
            int i = 0;
            while (i < notes.Length)
            {
                int noteCount = SetEachDenomination(notes[i]);
                _noteCounts[i] = noteCount;
                _totalATMCash += noteCount * noteValues[i];
                i++;
            }        
        }

        private int SetEachDenomination(string current)
        {
            int returnedValue = DatabaseManager.GetInstance().RetrieveDenominationAmounts(current);
            return returnedValue;
        }

        public bool IsWithdrawable(double attemptedWithdrawal)
        {
            bool returnValue = CheckIfNoteAssortmentAvailableForAmount(attemptedWithdrawal);
            SetLocalCash();
            return returnValue;
        }

        private bool CheckIfNoteAssortmentAvailableForAmount(double totalAmount)
        {
            int totalAmountWhole = Convert.ToInt32(totalAmount);
            int[] notes = { 10, 20, 50, 100, 200, 500 };
           
            if (totalAmountWhole <= _totalATMCash)
            {
                for (int i = notes.Length - 1; i >= 0; i--)
                {                          
                    if(notes[i] <= totalAmountWhole)
                    {
                        if(_noteCounts[i] != 0)
                        {
                            totalAmountWhole -= notes[i];
                            _noteCounts[i]--;

                            if (totalAmountWhole != 0) i++;
                        }
                    }
                }
            }

            if (totalAmountWhole == 0) return true;
            else return false;
        }

        /// <summary>
        /// CAN SOMEONE ELSE CLEAN THE FOLLOWING MESS UP? PLEASE AND THANKS.
        /// </summary>
        /// <param name="updateAmount"></param>

        public void UpdateAmountWithdrawal(double updateAmount)
        {
            updatecashWithdrawal(updateAmount);
            int i = 0;
            string[] denominations = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };

            while (i < _noteCounts.Length)
            {
                updateCashAmounts(denominations[i], _noteCounts[i]);
                i++;
            }
        }

        public void UpdateAmountDeposit(double updateAmount)
        {
            updatecashDeposit(updateAmount);
            int i = 0;
            string[] denominations = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            while (i < _noteCounts.Length)
            {
                updateCashAmounts(denominations[i], _noteCounts[i]);
                i++;
            }
        }

        private void updateCashAmounts(string note, int amount)
        {
            DatabaseManager.GetInstance().UpdateATMCashAmount(note, amount);
        }

        private void updatecashWithdrawal(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] notes = { 10, 20, 50, 100, 200, 500 };
           
            for (int i = notes.Length - 1; i >= 0; i--)
            {
               
                if (notes[i] > attempted)
                {
                }                
                else if (notes[i] <= attempted)
                {
                    if (_noteCounts[i] != 0)
                    {
                        attempted -= notes[i];
                        _noteCounts[i]--;
                        if (attempted != 0) i++;
                    }
                }
            }
        }

        private void updatecashDeposit(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] notes = { 10, 20, 50, 100, 200, 500 };
           
            for (int i = notes.Length - 1; i >= 0; i--)
            {
                if (notes[i] > attempted)
                {
                }
               
                else if (notes[i] <= attempted)
                {
                   attempted -= notes[i];
                    _noteCounts[i]++;
                    if (attempted != 0)
                        i++;
                }
            }
        }
    }
}
