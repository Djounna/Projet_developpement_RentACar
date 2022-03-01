using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALVille
    {
        public List<Ville> SelectAllVille()
        {
            List<Ville> queryResult = new List<Ville>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var ville = dbcontext.Ville;
                queryResult.AddRange(ville);

            }
            catch (Exception ex)
            {


                throw ex;
            }

            return queryResult;
        }

        public async void CreateVille(Ville ville)
        {

            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                dbcontext.Update(ville);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }
        public Ville SelectByID(int id)
        {

            List<Ville> queryResult = new List<Ville>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var result = from Ville in dbcontext.Ville where Ville.Idville == id select Ville;
                queryResult.AddRange(result);
                return queryResult[0];
            }
            catch (Exception ex)
            {


                throw ex;
            }


        }

        public async void UptadeVille(Ville ville)
        {

            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                dbcontext.Update(ville);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }

        public async void DeleteVille(int id)
        {

            try
            {

                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                Ville ville = dbcontext.Ville.Find(id);
                dbcontext.Remove(ville);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }
    }
}
