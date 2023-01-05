using System;
using System.Collections.Generic;
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
            List<string> names = new List<string> { "Danutė", "Vytautas","Svetlana","Juozas","Miglė","Robertas","Steponas","Gintarė","Modesta","Žygimantas"};
            List<string> lastnames = new List<string> { "Ambrazevičienė", "Skikas", "Ivanova", "Grušas", "Bružaitė", "Ozolas", "Barkauskas", "Narijauskė", "Girdžiūtė", "Mauricas" };
            for (int i = 0; i < 10; i++)
            {
                clientRepository.AddClient(names[i], lastnames[i], "LT0" + i + CardNumberGenerator.GenerateNumber());
                cardsRepository.AddCard(clientRepository.GetClients().Last().getGUID(),CardNumberGenerator.GenerateNumber(), DateGenerator.GenerateDate(), false);
            }
        }
    }
}
