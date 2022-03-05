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
        private ProjetSGDBContext dbcontext = new();
        public List<Ville> SelectAllVille() //Ok Antoine
        {
            List<Ville> queryResult = new();
            try
            {                              
                queryResult.AddRange(dbcontext.Ville);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryResult;
        }  

        public async void CreateVille(Ville ville)  //Ok Antoine
        {
            try
            {              
                dbcontext.Update(ville);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Ville SelectByID(int id) //Ok Antoine
        {            
            try
            {                 
                return dbcontext.Ville.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void UptadeVille(Ville ville) //Ok Antoine
        {
            try
            {               
                dbcontext.Update(ville);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void DeleteVille(int id) //Ok Antoine
        {
            try
            {              
                dbcontext.Remove(dbcontext.Ville.Find(id));
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
