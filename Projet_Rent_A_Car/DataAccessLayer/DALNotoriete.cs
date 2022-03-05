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
        private ProjetSGDBContext dbcontext = new ProjetSGDBContext();
        public List<Notoriete> SelectAllNotoriete() //Ok Antoine
        {
            List<Notoriete> queryResult = new();
            try
            {
                queryResult.AddRange(dbcontext.Notoriete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryResult;
        }

        public async void CreateNotoriete(Notoriete notoriete) //Ok Antoine
        {
            try
            {
                dbcontext.Update(notoriete);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Notoriete SelectByID(int id)//Ok Antoine
        {
            try
            {               
                return dbcontext.Notoriete.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void UptadeNotoriete(Notoriete notoriete)//Ok Antoine
        {
            try
            {
                dbcontext.Update(notoriete);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void DeleteNotoriete(int id)//Ok Antoine
        {
            try
            {
                dbcontext.Remove(dbcontext.Notoriete.Find(id));
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
