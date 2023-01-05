using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Repositories
{
    public class ClientRepository
    {
        private readonly List<Client> _clients = new List<Client>();

        public void AddClient(string fname, string lname, string account)
        {
            _clients.Add(new Client(fname, lname, account));
        }
        public List<Client> GetClients()
        {
            return _clients;
        }
    }
}
