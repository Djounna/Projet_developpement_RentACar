using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace BusinessLayer
{
    public class BLVille
    {
        private DALVille dalville = new();
        private DalCommun dal = new();
        public List<Ville> SelectAllVille()
        {
            return dal.SelectAll<Ville>();
        }

        public IEnumerable<SelectListItem> SelectAllVilleInList()
        {
            return dalville.SelectAllVilleInList();
        }
        public Ville SelectVilleByID(int id)
        {
            return dal.SelectById<Ville>(id);
        }
        public bool AlreadyExist(Ville v)
        {
            return dalville.AlreadyExist(v.Nom, v.Idpays);
        }
        public bool InsertOrUpdateVille(Ville ville)
        {
            return dal.InsertOrUpdate(ville);
        }
        public bool DeleteVille(int id)
        {
            return dal.Delete(SelectVilleByID(id));
        }

    }
}
