using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALDepot
    {
        private DalCommun dal = new();
        public List<Depot> SelectAllDepotInactif() //OK
        {
            try
            {
                return dal.dbcontext.Depot.Where(Depot => Depot.Inactif == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Depot> SelectAllDepotActif() //OK
        {
            try
            {
               return dal.dbcontext.Depot.Where(Depot => Depot.Inactif != true).ToList();
            
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Depot SelectByIdVille(int id)
        {
            try
            {
                return dal.dbcontext.Depot.Where(dep => dep.Idville == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SelectListItem> SelectAllDepotInList()
        {
            using (dal.dbcontext)
            {

                List<SelectListItem> lstDepot = dal.dbcontext.Depot
                    .Join(dal.dbcontext.Ville,
                    dep => dep.Idville,
                    vil => vil.Idville,                   
                    (dep, vil) =>                  
                       new SelectListItem
                       {
                           Value = dep.Iddepot.ToString(),
                           Text = vil.Nom,
                       }).OrderBy(x => x.Text)
                    .ToList();
                
                var depotIntro = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Dépot ---"
                };
                lstDepot.Insert(0, depotIntro);
              
                return new SelectList(lstDepot, "Value", "Text");

            }
        }

    }
}
