using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Repositories
{
    public class AccountRepository
    {
        private readonly List<Account> _accounts = new List<Account>();

        public void AddAccount(Guid id, string number, double balance )
        {
            _accounts.Add(new Account(id, number, balance));
        }
        public List<Account> GetCards()
        {
            return _accounts;
        }
    }
}
