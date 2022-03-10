
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
        private DalCommun dal = new();
        public IEnumerable<SelectListItem> SelectAllPaysInList()
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

    }
}

