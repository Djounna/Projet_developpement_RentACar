using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class BLForfait
    {
        private DALForfait dalForfait = new();
        private DalCommun dal = new();
        public List<Forfait> SelectAllForfait()
        {
            return dalForfait.SelectAllForfait();
        }
        public Forfait SelectForfaitByID(int id)
        {
            return dalForfait.SelectById(id);
        }

        public Forfait SelectForfaitByIDDepot(int id)
        {
            return dalForfait.SelectForfaitByIDDepot(id);
        }
        public bool Insert(Forfait forfait) 
        {
            if (!dalForfait.AlreadyExist(forfait))
            {
                return dalForfait.Insert(forfait);

            }
            else
            {
                return false;
            }
        }

        public bool AlreadyExist(Forfait forfait)
        {
            return dalForfait.AlreadyExist(forfait);
        }
        public bool Update(Forfait forfait)
        {
            return dalForfait.Update(forfait);
        }

        public Forfait SelectForfaitReservation(int idDepot1, int idDepot2) // OK Corentin, fonctionne
        {
            return dalForfait.SelectForfaitReservation(idDepot1, idDepot2);
        }
    }
}
