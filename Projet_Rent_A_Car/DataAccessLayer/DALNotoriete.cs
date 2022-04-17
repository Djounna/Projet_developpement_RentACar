using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace DataAccessLayer
{
    public class DALNotoriete
    {
        private DalCommun dal = new();
        public List<Notoriete> SelectAllNotorieteInactif() 
        {
            try
            {
                return dal.dbcontext.Notoriete.Where(Notoriete => Notoriete.Inactif == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Notoriete> SelectAllDNotorieteActif() 
        {
            try
            {
                return dal.dbcontext.Notoriete.Where(Notoriete => Notoriete.Inactif != true).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AlreadyExist(string nom, int id)
        {
            var notoriete = dal.dbcontext.Notoriete.SingleOrDefault(p => p.Libelle == nom && p.Idnotoriete != id);
            return (notoriete != null);
        }


        public IEnumerable<SelectListItem> SelectAllNotorieteInList()  
        {
            using (dal.dbcontext)
            {

                List<SelectListItem> lstNotoriete = dal.dbcontext.Notoriete
                    .Where(n => n.Inactif != true).Select
                    (n =>
                       new SelectListItem
                       {
                           Value = n.Idnotoriete.ToString(),
                           Text = n.Libelle,
                       }).OrderBy(x => x.Text)
                    .ToList();

                var notorieteIntro = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Notoriete ---"
                };
                lstNotoriete.Insert(0, notorieteIntro);

                return new SelectList(lstNotoriete, "Value", "Text");

            }
        }

    }

}
