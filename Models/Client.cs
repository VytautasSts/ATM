using ATM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public struct Client
    {
        private readonly Guid ClientID { get; } 
        private string Fname { get; }
        private string Lname { get; }
        private string AccountNumber { get; }
        public Client(string firstname, string lastname, string account)
        {
            ClientID = new Guid();
            Fname = firstname;
            Lname = lastname;
            AccountNumber = account;
            GUIDS.AddGUID(ClientID);
        }
        public string getInfo()
        {
            return $"Client_name:{Fname}; Client_surname:{Lname}; Account_number:{AccountNumber};";
        }
        public Guid getGUID()
        {
            return ClientID;
        }
    }
}
