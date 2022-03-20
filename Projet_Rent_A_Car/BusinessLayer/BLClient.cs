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
        DalCommun dal = new();

        public Client SelectClientById(int id)
        {
            {
                return dal.SelectById<Client>(id);
            }
        }

        public Client SelectClientByMail(string mail)
        {
            return dalclient.SelectClientByMail(mail);
        }
        public void CreateClient(Client client)
        {
            dal.InsertOrUpdate(client);
        }
    }
}
