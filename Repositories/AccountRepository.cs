using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Repositories
{
    public class CardsRepository
    {
        private readonly List<Card> _cards = new List<Card>();

        public void AddCard(Guid id, string number, DateOnly validity, bool blocked)
        {
            _cards.Add(new Card(id, number, validity, blocked));
        }
        public List<Card> GetCards()
        {
            return _cards;
        }
    }
}
