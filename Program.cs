using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using ATM.Services;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Initiate();
            
            HTMLGenerator.GenerateHTML();
        }
    }
}