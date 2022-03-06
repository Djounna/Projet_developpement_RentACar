using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLClient
    {
        DALClient dalclient= new();

        public Client SelectClientByMail(string mail)//OK Antoine
        {
            return dalclient.SelectClientByMail(mail);
        }

        public void CreateClient(Client client)//Ok Antoine
        {
            dalclient.CreateClient(client);
        }
    }
}
