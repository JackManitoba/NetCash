using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.ATMHardware
{    
    class Card
    {
        private string cardNumber = "";
        private string expiry = "";

        public Card (string CN, string E)
        {
            cardNumber = CN;
            expiry = E;
        }
        public string getCardNumber()
        {
            return this.cardNumber;
        }
        public string getExpiryDate()
        {
            return this.expiry;
        }
    }
}
