using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace BusinessLayer
{
    public class BLPays
    {     
        private DalCommun dal = new();
        private DALPays dalpays = new();
        private DALVille dalVille = new();
        public List<Pays> SelectAllPays()
        {
            return dal.SelectAll<Pays>();
        }
        public IEnumerable<SelectListItem> SelectAllPaysInList()
        {          
            return dalpays.SelectAllPaysInList();
        }       
        public Pays SelectPaysByID(int id)
        {
            return dal.SelectById<Pays>(id);
        }
        public bool AlreadyExist(Pays p)
        {
            return dalpays.AlreadyExist(p.Nom, p.Idpays);
        }
        public bool InsertOrUpdatePays(Pays pays)
        {
            return dal.InsertOrUpdate(pays);
        }
        public bool DeletePays(int id)
        {
           return dal.Delete(SelectPaysByID(id));
        }
        public List<Ville> SelectVilleByPays(int idpays)
        {
            return dalVille.SelectVilleByPays(idpays);
        }
    }
}