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

        public bool AlreadyExist(string nom, int id)
        {
            var ville = dal.dbcontext.Ville.SingleOrDefault(p => p.Nom == nom && p.Idpays == id);
            return (ville != null);
        }
        public IEnumerable<SelectListItem> SelectAllVilleInList()
        {
            using (dal.dbcontext)
            {
                List<int> lst = (from ville in dal.dbcontext.Ville select ville.Idville).ToList();
                List<int> lst2 = (from depot in dal.dbcontext.Depot select depot.Idville).ToList();

                var lst3 = (from id in lst select id).Except(lst2).ToList();

                List<SelectListItem> lstville = dal.dbcontext.Ville
                   .Where(x=> lst3.Contains(x.Idville))
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
