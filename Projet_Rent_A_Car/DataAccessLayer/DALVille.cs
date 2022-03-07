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
        // private ProjetSGDBContext dbcontext = new();
        private DalCommun dal = new DalCommun();
        public List<Ville> SelectAllVille() //Ok Antoine
        {
            List<Ville> queryResult = new();
            try
            {                              
                queryResult.AddRange(dal.dbcontext.Ville);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryResult;
        }  

        
        public Ville SelectByID(int id) //Ok Antoine
        {            
            try
            {                 
                return dal.dbcontext.Ville.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /* // A remplacer
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
        */

        public async void InsertOrUpdateVille(Ville ville) //Ok Corentin, à valider par Antoine
        {
            try
            {
                dal.InsertOrUpdate(ville);
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
                dal.dbcontext.Remove(dal.dbcontext.Ville.Find(id));
                var oResponse = await dal.dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
