using Models;

namespace DataAccessLayer
{
    public class DALClient
    {
        private DalCommun dal = new();
        public Client SelectClientByMail(string mail)
        {
            try
            {
                return dal.dbcontext.Client.Where(x => x.Mail == mail).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Reservation> SelectAllReservationByClient(int IdClient)
        {
            try
            {
                return dal.dbcontext.Reservation.Where(r => r.Idclient == IdClient).ToList();
            }
            catch (Exception ex) { throw ex; }
        }
        public bool AlreadyExist(string nom, int id)
        {
            var client = dal.dbcontext.Client.SingleOrDefault(p => p.Mail == nom && p.Idclient != id);
            return (client != null);
        }

    }
}
