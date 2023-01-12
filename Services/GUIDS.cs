using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Services
{
    public class GUIDS
    {
        private static readonly List<Guid> GUIDlist = new List<Guid>();

        public static void AddGUID(Guid id)
        {
            GUIDlist.Add(id);
        }
        public static bool ConfirmGUID(Guid id)
        {
            bool result = false;
            if(GUIDlist.Contains(id)) { result = true; }
            return result;
        }
        public static List<Guid> GetGUIDList()
        {
            return GUIDlist;
        }
    }
}
