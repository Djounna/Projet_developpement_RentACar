using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace BusinessLayer
{
    public class BLPays
    {
        private DALPays dalpays = new();
        public List<Pays> SelectAllPays() //Ok Antoine
        {
            return dalpays.SelectAllPays();
        }
        public IEnumerable<SelectListItem> SelectAllPaysInList()//Ok Antoine
        {          
            return dalpays.SelectAllPaysInList();
        }
       
        public Pays SelectPaysByID(int id)//Ok Antoine
        {          
            return dalpays.SelectByID(id);
        }

        public void InsertOrUpdatePays(Pays pays)//Ok Antoine
        {
            dalpays.InsertOrUpdatePays(pays);
        }

        /* // Doublon
        public void UptadePays(Pays pays)//Ok Antoine
        {
            dalpays.InsertOrUpdatePays(pays);
        }
        */

        public void DeletePays(int id)//Ok Antoine
        {
            dalpays.DeletePays(id);
        }
    }
}