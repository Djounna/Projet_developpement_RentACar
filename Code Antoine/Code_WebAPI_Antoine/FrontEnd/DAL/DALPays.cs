using FrontEnd.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FrontEnd.DAL
{
    public class DALPays
    {

        public List<Pays> SelectAll()
        {
            List<Pays> queryResults = new List<Pays>();

            try
            {
              

                    ProjetSGDBContext dbContext = new ProjetSGDBContext();
                    var listPays = from Pays in dbContext.Pays select Pays;
                   
                    queryResults.AddRange(listPays);


                

            }          
            catch (Exception e)
            {
                throw e;
            }

            return queryResults;
        }

    }
}
