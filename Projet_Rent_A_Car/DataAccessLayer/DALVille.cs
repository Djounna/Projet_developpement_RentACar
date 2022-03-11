using Microsoft.AspNetCore.Mvc.Rendering;
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

        private DalCommun dal = new();
        public List<Ville> SelectVilleByPays(int idPays)
        {
            using (dal.dbcontext)
            {
                List<Ville> lstville = dal.dbcontext.Ville.Where(ville => ville.Idpays == idPays).ToList();
                return lstville;                      
            }
        }
        public IEnumerable<SelectListItem> SelectAllVilleInList()
        {
            using (dal.dbcontext)
            {
                List<SelectListItem> lstville = dal.dbcontext.Ville
                   .OrderBy(n => n.Nom)
                   .Select(n =>
                       new SelectListItem
                       {
                           Value = n.Idville.ToString(),
                           Text = n.Nom,
                       }).ToList();
                var paysIntro = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select ville ---"
                };
                lstville.Insert(0, paysIntro);
                return new SelectList(lstville, "Value", "Text");
            }
        }
    }
}
