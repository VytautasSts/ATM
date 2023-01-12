using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Services
{
    public class Timestamp
    {
        public static DateTime GenerateDate()
        {
            DateTime date = DateTime.Now;
            Random digits= new Random();
            var newDate = date.AddDays(digits.Next(-14, 0)).AddHours(digits.Next(-12, 0)).AddMinutes(digits.Next(-60, 0));
            return newDate;
        }
    }
}
