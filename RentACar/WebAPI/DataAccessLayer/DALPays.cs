using WebAPI.Models;

namespace WebAPI.DataAccessLayer
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

            } catch (Exception ex) { 


            throw ex;
            }

            return queryResult;
        }

    }
}
