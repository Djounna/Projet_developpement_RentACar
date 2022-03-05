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
        public void CreatePays(Pays pays)//Ok Antoine
        {           
            dalpays.CreatePays(pays);           
        }
        public Pays SelectPaysByID(int id)//Ok Antoine
        {          
            return dalpays.SelectByID(id);
        }
        public void UptadePays(Pays pays)//Ok Antoine
        {
            dalpays.UptadePays(pays);
        }
        public void DeletePays(int id)//Ok Antoine
        {
            dalpays.DeletePays(id);
        }
    }
}