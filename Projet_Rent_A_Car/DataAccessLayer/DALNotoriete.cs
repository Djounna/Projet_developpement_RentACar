using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALNotoriete
    {
        private DalCommun dal = new();
        public List<Notoriete> SelectAllNotorieteInactif() // OK Corentin, à valider
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
        public List<Notoriete> SelectAllDNotorieteActif() // Corentin, à valider
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
    }

}
