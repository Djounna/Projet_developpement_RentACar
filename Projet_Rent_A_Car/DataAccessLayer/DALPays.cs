using Microsoft.AspNetCore.Mvc.Rendering;


namespace DataAccessLayer
{
    public class DALPays
    {
        private DalCommun dal = new();

        public bool AlreadyExist(string nom, int id)
        {
            var pays = dal.dbcontext.Pays.SingleOrDefault(p => p.Nom == nom && p.Idpays != id);
            return (pays != null);
        }
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
                var paysIntro = new SelectListItem()
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

