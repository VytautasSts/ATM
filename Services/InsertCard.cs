using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Services
{
    public class InsertCard
    {
        public readonly List<Guid> Guidselector = new List<Guid>();

        public InsertCard()
        {
            Guidselector = GUIDS.GetGUIDList();

        }
        public Guid Random()
        {
            var rnd = new Random();
            var selected = rnd.Next(0,Guidselector.Count());
            return Guidselector[selected];
        }
    }
}
