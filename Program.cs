using System;
using System.Data.SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using ATM.Services;
using ATM.Models;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLite sqlite = new();
            Startup.Initiate(sqlite);
            
            HTMLGenerator.GenerateHTML();
            while (true)
            {
                Console.WriteLine("Insert Card");
                Guid cardInserted = new Guid(Console.ReadLine());
                Console.WriteLine("Card inserted. Please wait...");
                bool cardHolderInternal = false;
                var attempt = 3;
                var authenticated = false;
                var currentUser = sqlite.GetBankInfo(sqlite.Conn, cardInserted);
                if (cardInserted.ToString().Contains("-a")) cardHolderInternal = true;//string "-a" was chosen randomly to have majority of DB as "internal clients"
                if (currentUser.Blocked)
                {
                    Console.Clear();
                    Console.WriteLine("Card blocked. Please contact your bank!");
                }
                else
                {
                    if (cardHolderInternal)
                    {
                        Console.Clear();
                        Console.WriteLine("Card found in db");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Card belongs to other vendor!\nAdditional charges may be applied!");
                    }
                    while (true)
                    {
                        Console.WriteLine("Enter card PIN. " + attempt + " attempts remaining");
                        string pincode = Console.ReadLine();
                        if (currentUser.PIN == pincode)
                        {
                            authenticated = true; break;
                        }
                        else
                        {
                            attempt--;
                            if (attempt == 0)
                            {
                                Console.WriteLine("Card blocked. Please contact your bank!");
                                currentUser.Blocked = true;
                                sqlite.UpdateBankData(sqlite.Conn, currentUser);
                                break;
                            }
                        }
                    }
                    while (true)
                    {
                        var q = 0;
                        if (authenticated)
                        {
                            if (currentUser.Blocked) Console.WriteLine("Card blocked. Please contact your bank!");
                            else
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.Write("Please select next operation.\n");
                                    Console.Write("[1] - Account balance;\n[2] - Latest transactions;\n[3] - Withdraw cash;\n[4] - Retrieve card;\n");
                                    var menuselect = Console.ReadLine()[0];
                                    switch (menuselect)
                                    {
                                        case '1':
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Account ballance :" + currentUser.Balance.ToString()); //show me the money!
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey();
                                                break;
                                            }
                                        case '2':
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Latest transactions :"); //Latest transactions
                                                sqlite.GetTransactionInfo(sqlite.Conn, currentUser.AccountNumber);
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey();
                                                break;
                                            }
                                        case '3':
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Withdraw cash :"); //Withdraw cash
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey();
                                                break;
                                            }
                                        case '4': q = 1; break; //quit
                                        default: break;
                                    }
                                    if (q == 1) break;
                                }
                            }
                        }
                        if (q == 1) break;
                    }
                }
            }
        }   
    }
}