using Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessLayer
{
    public class BLVille
    {
        private DALVille dalville = new();
        private DalCommun dal = new();
        public List<Ville> SelectAllVille()
        {           
            return dal.SelectAll<Ville>();
        }

        public IEnumerable<SelectListItem> SelectAllVilleInList()
        {
            return dalville.SelectAllVilleInList();
        }
        public Ville SelectVilleByID(int id)
        {            
            return dal.SelectById<Ville>(id);
        }
        public void InsertOrUpdateVille(Ville ville)
        {
            dal.InsertOrUpdate(ville);
        }
        public void DeleteVille(int id)
        {
            dal.Delete(SelectVilleByID(id));
        }
        
    }
}
