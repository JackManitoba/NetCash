using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.ATMHardware
{    
    class Card
    {
        private string cardNumber = "";
        private string expiry = "";
        private bool canceled = false;

        public Card (string CNo, string E, bool Canceled)
        {
            cardNumber = CNo;
            expiry = E;
            canceled = Canceled;

            Debug.WriteLine("CARDNUMBER IN CARD CLASS:" + cardNumber);
            Debug.WriteLine("EXPIRY IN CARD CLASS:" +expiry);
            Debug.WriteLine("CARD canceled:" + canceled);
        }
        public string getCardNumber()
        {
            return this.cardNumber;
        }
        public string getExpiryDate()
        {
            return this.expiry;
        }
        public bool isCardCanceled()
        {
            return this.canceled;
        }
    }
}
