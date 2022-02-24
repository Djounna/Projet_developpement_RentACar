
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALPays
    {
        public List<Pays> SelectAllPays()
        {
            List<Pays> queryResult = new List<Pays>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var listPays = from Pays in dbcontext.Pays select Pays;
                queryResult.AddRange(listPays);

            }
            catch (Exception ex)
            {


                throw ex;
            }

            return queryResult;
        }
    }
}
