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


    }
}