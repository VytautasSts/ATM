using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Services
{
    public class DateGenerator
    {
        public static DateOnly GenerateDate()
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            Random digits= new Random();
            int validityspan = 4 * 364 + 1; //card is valid for 4 years. One of the years is a leap year so +1 day
            int days = digits.Next(0,validityspan);
            var newDate = date.AddDays(days);
            return newDate;
        }
    }
}
