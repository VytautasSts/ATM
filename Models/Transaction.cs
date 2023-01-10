using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ATM.Models
{
    public struct Transaction
    {
        public string Name { get; }
        public string LastName { get; }
        public string AccountNumber { get; }
        public double Amount { get; }
        public DateTime Time { get; }
        public Transaction(string name, string lastname, string accountnumber, double amount, DateTime time)
        {
            Name = name;
            LastName = lastname;
            AccountNumber = accountnumber;
            Amount = amount;
            Time = time;
        }
    }
}
