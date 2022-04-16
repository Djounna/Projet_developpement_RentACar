using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class BLClient
    {
        DALClient dalclient = new();
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

        public bool AlreadyExist(Client c)
        {
            return dalclient.AlreadyExist(c.Mail, c.Idclient);
        }
        public bool CreateClient(Client client)
        {
            return dal.InsertOrUpdate(client);
        }

        public List<Reservation> SelectAllReservationByClient(int IdClient)
        {
            return dalclient.SelectAllReservationByClient(IdClient);
        }

    }
}
