using Models;
using DataAccessLayer;

namespace BusinessLayer
{
    public class BLPays
    {

        public List<Pays> GetAllPays()
        {
            List<Pays> lstPays;
            DALPays pays = new DALPays();
            lstPays = pays.SelectAllPays();
            return lstPays;
        }


        public void CreatePays(Pays pays)
        {
            
            DALPays dalPays = new DALPays();
            dalPays.CreatePays(pays);
            
        }

        public Pays GetPaysByID(int id)
        {
            Pays pays;
            DALPays dalPays = new DALPays();
            pays = dalPays.SelectByID(id);
            return pays;
        }

        public void UptadePays(Pays pays)
        {

            DALPays dalPays = new DALPays();
            dalPays.UptadePays(pays);

        }

        public void DeletePays(int id)
        {

            DALPays dalPays = new DALPays();
            dalPays.DeletePays(id);

        }

    }
}