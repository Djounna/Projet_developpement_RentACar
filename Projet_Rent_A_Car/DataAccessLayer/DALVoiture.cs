using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALVoiture
    {
        private DalCommun dal = new();
        public List<Voiture> SelectAllVoitureInactif() // OK Corentin, à valider
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
        public List<Voiture> SelectAllDVoitureActif() // Corentin, à valider
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

    }
}
