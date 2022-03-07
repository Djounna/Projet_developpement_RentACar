
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
        // private ProjetSGDBContext dbcontext = new(); // A remplacer
        private DalCommun dal = new DalCommun();
        public List<Pays> SelectAllPays() //Ok Antoine 
        {
            List<Pays> queryResult = new List<Pays>();
            try
            {                            
                queryResult.AddRange(dal.dbcontext.Pays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryResult;
        }

        
        public Pays SelectByID(int id) //Ok Antoine
        {                     
            try
            {
                return dal.dbcontext.Pays.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        public IEnumerable<SelectListItem> SelectAllPaysInList()//Ok Antoine
        {
            using (dal.dbcontext)
            {
                List<SelectListItem> lstpays = dal.dbcontext.Pays
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

        public async void InsertOrUpdatePays(Pays pays) //Ok Corentin
        {
            try
            {
                dal.InsertOrUpdate(pays);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* // A remplacer
        public async void CreatePays(Pays pays)//Ok Antoine
        {
            try
            {
                dal.dbcontext.Update(pays);
                var oResponse = await dal.dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async void UptadePays(Pays pays) //Ok Antoine
        {
            try
            {
                dal.dbcontext.Update(pays);
                var oResponse = await dal.dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */



        public async void DeletePays(int id) //Ok Antoine
        {
            try
            {             
                dal.dbcontext.Remove(dal.dbcontext.Pays.Find(id));
                var oResponse = await dal.dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

