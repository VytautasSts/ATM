using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ATM.Models
{
    public struct SQLentry
    {
        public Guid ClientID { get; }
        public string Name { get; }
        public string LastName { get; }
        public string AccountNumber { get; }
        public double Balance { get; }
        public string CardNumber { get; }
        public DateOnly ValidityDate { get; }
        public bool Blocked { get; set; }
        public string PIN { get; }
        public SQLentry(Guid id,string name, string lastname, string accountnumber, double balance,string cardnumber, DateOnly validitydate,bool blocked, string pin)
        {
            ClientID = id;
            Name = name;
            LastName = lastname;
            AccountNumber = accountnumber;
            Balance = balance;
            CardNumber= cardnumber;
            ValidityDate = validitydate;
            Blocked= blocked;
            PIN = pin;
        }
    }
}
