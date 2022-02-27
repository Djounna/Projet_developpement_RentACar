using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALNotoriete
    {
        public List<Notoriete> SelectAllNotoriete()
        {
            List<Notoriete> queryResult = new List<Notoriete>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var listNotoriete = from Notoriete in dbcontext.Notoriete select Notoriete;
                queryResult.AddRange(listNotoriete);

            }
            catch (Exception ex)
            {


                throw ex;
            }

            return queryResult;
        }

        public async void CreateNotoriete(Notoriete notoriete)
        {

            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                dbcontext.Update(notoriete);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }
        public Notoriete SelectByID(int id)
        {

            List<Notoriete> queryResult = new List<Notoriete>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var result = from Notoriete in dbcontext.Notoriete where Notoriete.Idnotoriete == id select Notoriete;
                queryResult.AddRange(result);
                return queryResult[0];
            }
            catch (Exception ex)
            {


                throw ex;
            }


        }

        public async void UptadeNotoriete(Notoriete notoriete)
        {

            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                dbcontext.Update(notoriete);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }

        public async void DeleteNotoriete(int id)
        {

            try
            {

                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                Notoriete Notoriete = dbcontext.Notoriete.Find(id);
                dbcontext.Remove(Notoriete);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }
    }

}
