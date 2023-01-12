using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Services
{
    public class Withdraw
    {
        public static void cash(SQLentry e,bool localclient,SQLite sqlite) 
        {
            int transactionsToday = sqlite.GetTransactionCountToday(sqlite.Conn, e.AccountNumber);
            if (transactionsToday >= 10)
            {
                Console.WriteLine($"You have done {transactionsToday} transactions today. Transaction limit exceeded!");
            }
            else
            {

                if (localclient) Console.WriteLine("Enter amount you wish to withdraw:");
                else Console.WriteLine("Enter amount you wish to withdraw.\nNote that you will be charged aditional 1 percent of the selected amount, because you are not a client of this Bank!");
                int amount = Int32.Parse(Console.ReadLine());
                if(amount > 1000) 
                {
                    Console.Clear();
                    Console.WriteLine($"You have chosen to withdraw {amount}.\nCash out limit is 1000!");
                }
                else
                {
                    if (Math.Abs(amount) > e.Balance)
                    {
                        Console.Clear();
                        Console.WriteLine($"Insufficient funds!");
                    }
                    else
                    {
                        double newbalance = 0.0;
                        if (localclient)
                        {
                            newbalance = e.Balance - amount;
                            Transaction cashout = new(e.Name, e.LastName, e.AccountNumber, amount*(-1), DateTime.Now);
                            sqlite.InsertTransaction(sqlite.Conn, cashout);
                            e.Balance = newbalance;
                            sqlite.UpdateBankData(sqlite.Conn, e);
                            Console.WriteLine($"Cashed out {amount}");
                        }
                        else
                        {
                            newbalance = e.Balance - amount * 1.01;
                            Transaction cashout = new(e.Name, e.LastName, e.AccountNumber, amount*(-1.01), DateTime.Now);
                            sqlite.InsertTransaction(sqlite.Conn, cashout);
                            e.Balance = newbalance;
                            sqlite.UpdateBankData(sqlite.Conn, e);
                            Console.WriteLine($"Cashed out {amount}");
                        }
                    }
                }
            }
        }
    }
}
