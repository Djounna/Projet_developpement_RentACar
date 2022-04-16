using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace BusinessLayer
{
    public class BLNotoriete
    {
        DalCommun dal = new();
        DALNotoriete dalNotoriete = new();

        public List<Notoriete> SelectAllNotoriete()
        {
            return dal.SelectAll<Notoriete>();
        }
        public Notoriete SelectNotorieteByID(int id)
        {
            return dal.SelectById<Notoriete>(id);
        }

        public bool AlreadyExist(Notoriete not)
        {
            return dalNotoriete.AlreadyExist(not.Libelle, not.Idnotoriete);
        }
        public bool InsertOrUpdateNotoriete(Notoriete notoriete) // OK
        {

            return dal.InsertOrUpdate(notoriete);

        }
        public bool DeleteNotoriete(int id)
        {
            return dal.Delete(SelectNotorieteByID(id));
        }
        public List<Notoriete> SelectAllNotorieteInactif() // OK 
        {
            return dalNotoriete.SelectAllNotorieteInactif();
        }
        public List<Notoriete> SelectAllNotorieteActif() // OK 
        {
            return dalNotoriete.SelectAllDNotorieteActif();
        }

        public IEnumerable<SelectListItem> SelectAllNotorieteInList() // OK
        {
            return dalNotoriete.SelectAllNotorieteInList();
        }


    }
}
