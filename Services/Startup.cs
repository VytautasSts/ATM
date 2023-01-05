using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ATM.Repositories;

namespace ATM.Services
{
    public class Startup
    {
        internal static void Initiate()
        {
            //Create and populate the Client and Card repositories
            CardsRepository cardsRepository = new CardsRepository();
            ClientRepository clientRepository= new ClientRepository();
            AccountRepository accountRepository= new AccountRepository();
            List<string> names = new List<string> { "Danutė", "Vytautas","Svetlana","Juozas","Miglė","Robertas","Steponas","Gintarė","Modesta","Žygimantas"};
            List<string> lastnames = new List<string> { "Ambrazevičienė", "Skikas", "Ivanova", "Grušas", "Bružaitė", "Ozolas", "Barkauskas", "Narijauskė", "Girdžiūtė", "Mauricas" };
            for (int i = 0; i < 10; i++)
            {
                var newGUID = new Guid();
                var name = names[i];
                var lastname = lastnames[i];
                var cardnumber = CardNumberGenerator.GenerateNumber();
                var accountnumber = "LT0" + i + cardnumber;
                var date = DateGenerator.GenerateDate();
                var balance = Math.Round((Math.PI * (i * 1000) - Math.Pow(i, 2) * 300),2);
                var blocked = false;
                
                object entry = new {newGUID,name,lastname,accountnumber,balance,cardnumber,date,blocked};
                SQLite.InsertData(SQLite.Conn, entry);
            }
        }
        
    }
}
