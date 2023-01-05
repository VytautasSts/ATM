using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public struct Card
    {
        private Guid ClientID { get; }
        private string Number { get; }
        private DateOnly ValidUntil { get; }
        private bool Blocked { get; set; }
        public Card(Guid id,string number, DateOnly validity, bool blocked)
        {
            ClientID = id;
            Number = number;
            ValidUntil = validity;
            Blocked = blocked;
        }
        public string getInfo()
        {
            return $"Card_number:{Number}; Valid_until:{ValidUntil}; Blocked:{Blocked};";
        }
    }
}
