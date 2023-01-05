using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Services
{
    public class CardNumberGenerator
    {
        public static string GenerateNumber()
        {
            string number = "";
            Random digit= new Random();
            for(int i = 0; i < 16;i++)
            {
                number += digit.Next(0,9).ToString();//16 digit card number generated as string to keep the zero digit
            }
            return number;
        }
    }
}
