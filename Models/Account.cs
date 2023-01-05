using ATM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public struct Account
    {
        private readonly Guid ClientID { get; } 
        private string AccountNumber { get; }
        private double Balance { get; }
        public Account(Guid id, string account, double balance)
        {
            ClientID = id;
            AccountNumber = account;
            Balance = balance;
        }
        public string getInfo()
        {
            return $"Account_number:{AccountNumber}; Balance:{Balance};";
        }
    }
}
