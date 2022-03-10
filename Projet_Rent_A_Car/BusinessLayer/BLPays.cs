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
        public void InsertOrUpdatePays(Pays pays)
        {
            dal.InsertOrUpdate(pays);
        }
        public void DeletePays(int id)
        {
            dal.Delete(SelectPaysByID(id));
        }
        public List<Ville> SelectVilleByPays(int idpays)
        {
            return dalVille.SelectVilleByPays(idpays);
        }
    }
}