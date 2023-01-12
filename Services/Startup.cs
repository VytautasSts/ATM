using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Services
{
    public class Startup
    {
        internal static void Initiate(SQLite sqlite)
        {
            Console.Write("Loading");
            List<string> names = new() { "Danutė", "Vytautas","Svetlana","Juozas","Miglė","Robertas","Steponas","Gintarė","Modesta","Žygimantas"};
            List<string> lastnames = new() { "Ambrazevičienė", "Skikas", "Ivanova", "Grušas", "Bružaitė", "Ozolas", "Barkauskas", "Narijauskė", "Girdžiūtė", "Mauricas" };
            string bank_table = "BankData";
            //sqlite.DeleteData(sqlite.Conn, bank_table);//If something goes horibly wrong
            //sqlite.DeleteData(sqlite.Conn, transaction_table);//If something goes horibly wrong
            if (sqlite.CheckIfTableEmpty(sqlite.Conn, bank_table))
            {
                for (int i = 0; i < 10; i++)
                {
                    var newGUID = Guid.NewGuid();
                    var name = names[i];
                    var lastname = lastnames[i];
                    var cardnumber = CardNumberGenerator.GenerateNumber();
                    var accountnumber = "LT0" + i + cardnumber;
                    var date = DateGenerator.GenerateDate();
                    var balance = Int32.Parse(accountnumber.Remove(0, 14)) / (100 + i * 15);
                    var blocked = false;
                    var pin = "111"+i.ToString();
                    SQLentry entry = new SQLentry(newGUID, name, lastname, accountnumber, balance, cardnumber, date, blocked, pin);
                    sqlite.InsertBankData(sqlite.Conn, entry);
                    var plusminus = -1;
                    for (int j = 0; j < 5; j++)
                    {
                        double amount = Int32.Parse(accountnumber.Remove(0,14))/(100+j*20)*(-1*plusminus);
                        Transaction trans = new Transaction(name, lastname, accountnumber, amount, Timestamp.GenerateDate());
                        sqlite.InsertTransaction(sqlite.Conn, trans);
                        plusminus*=(-1);
                    }
                    GUIDS.AddGUID(newGUID);
                    Console.Write(".");
                }
            }
            Console.WriteLine("Done!");
        }
    }
}
