using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class BLPrix
    {
        private DALPrix dalPrix = new();
        private DalCommun dal = new();
        public List<Prix> SelectAllPrix()
        {
            return dalPrix.SelectAllPrix();
        }

        public Prix SelectPrixByPays(int id)
        {
            return dalPrix.SelectPrixByPays(id);
        }
        public Prix SelectPrixByID(int id)
        {
            return dal.SelectById<Prix>(id);
        }
        public bool InsertOrUpdatePrix(Prix Prix)
        {
            Prix p = SelectPrixByPays(Prix.Idpays);

            if (p != null)
            {
                Prix prixACloturer = SelectPrixByID(p.Idprix);
                prixACloturer.DateFin = DateTime.Now;
                dal.InsertOrUpdate(prixACloturer);
                Prix.Idprix = 0;
            }

            return dal.InsertOrUpdate(Prix);
        }
        public bool UpdatePrix(Prix prix)
        {

            return dalPrix.UpdatePrix(prix);
        }
    }
}
