using WebAPI.DataAccessLayer;
using WebAPI.Models;

namespace WebAPI.BusinessLayer
{
    public class BLPays
    {

        public List<Pays> GetAllPays()
        {
            List<Pays> lstPays;
            DALPays pays = new DALPays();
            lstPays=pays.SelectAllPays();   
            return lstPays;
        }

    }
}
