using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccessLayer
{
    public class DALVoiture
    {
        private DalCommun dal = new();
        public List<Voiture> SelectAllVoitureInactif() // OK 
        {
            try
            {
                return dal.dbcontext.Voiture.Where(Voiture => Voiture.Inactif == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Voiture> SelectAllVoitureActif() // Ok
        {
            try
            {
                return dal.dbcontext.Voiture.Where(Voiture => Voiture.Inactif != true).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Corentin en cours
        /*
        public IEnumerable<SelectListItem> SelectAllVoitureDisponibleInList(Depot depot)
        {

        using (dal.dbcontext)
            {

                List<SelectListItem> lstVoiture= dal.dbcontext.Voiture
                    .Where(n=> ((n.Inactif != true)&&(n....)).Select
                    (n =>
                       new SelectListItem
                       {
                           Value = n.IdVoiture.ToString(),
                           Text = n.Marque,
                       }).OrderBy(x => x.Text)
                    .ToList();

                var voitureIntro = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Voiture ---"
                };
                lstVoiture.Insert(0, voitureIntro);

                return new SelectList(lstVoiture, "Value", "Text");

            }

        }
        */


    }
}
