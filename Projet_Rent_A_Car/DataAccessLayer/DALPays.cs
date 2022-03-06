
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private ProjetSGDBContext dbcontext = new();
        public List<Pays> SelectAllPays() //Ok Antoine 
        {
            List<Pays> queryResult = new List<Pays>();
            try
            {                            
                queryResult.AddRange(dbcontext.Pays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryResult;
        }
        public async void CreatePays(Pays pays)//Ok Antoine
        {           
            try
            {               
                dbcontext.Update(pays);
                var oResponse = await dbcontext.SaveChangesAsync();            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Pays SelectByID(int id) //Ok Antoine
        {                     
            try
            {
                return dbcontext.Pays.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        public IEnumerable<SelectListItem> SelectAllPaysInList()//Ok Antoine
        {
            using (dbcontext)
            {
                List<SelectListItem> lstpays = dbcontext.Pays
                   .OrderBy(n => n.Nom)
                   .Select(n =>
                       new SelectListItem
                       {
                           Value = n.Idpays.ToString(),
                           Text = n.Nom,
                       }).ToList();
                var paysIntro= new SelectListItem()
                        {
                            Value = null,
                            Text = "--- select pays ---"
                        };
                lstpays.Insert(0, paysIntro);
                return new SelectList(lstpays, "Value", "Text");
            }
        }
        public async void UptadePays(Pays pays) //Ok Antoine
        {
            try
            {
                dbcontext.Update(pays);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async void DeletePays(int id) //Ok Antoine
        {
            try
            {             
                dbcontext.Remove(dbcontext.Pays.Find(id));
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

