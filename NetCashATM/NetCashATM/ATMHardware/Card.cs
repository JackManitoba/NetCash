using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.ATMHardware
{    
    public class Card
    {
        /// <summary>
        /// WHY ARE THESE SET DEFAULT VALUES?
        /// </summary>
        private string _cardNumber = "";
        private string _expiry = "";
        private bool _cancelled = false;

        public Card (string CNo, string E, bool cancelled)
        {
            _cardNumber = CNo;
            _expiry = E;
            _cancelled = cancelled;

            Debug.WriteLine("CARDNUMBER IN CARD CLASS: " + _cardNumber);
            Debug.WriteLine("EXPIRY IN CARD CLASS: " + _expiry);
            Debug.WriteLine("CARD canceled: " + _cancelled);
        }
        public string GetCardNumber()
        {
            return _cardNumber;
        }
        public string GetExpiryDate()
        {
            return _expiry;
        }
        public bool IsCardCanceled()
        {
            return _cancelled;
        }
    }
}
