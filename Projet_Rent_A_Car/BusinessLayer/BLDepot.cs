using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace BusinessLayer
{
    public class BLDepot
    {
        private DALDepot dalDepot = new();
        private DalCommun dal = new();
        public List<Depot> SelectAllDepot()
        {
            return dal.SelectAll<Depot>();
        }
        public Depot SelectDepotByID(int id)
        {
            return dal.SelectById<Depot>(id);
        }
        public bool InsertOrUpdateDepot(Depot depot)
        {
            return dal.InsertOrUpdate(depot);
        }
        public bool DeleteDepot(int id)
        {
            return dal.Delete(SelectDepotByID(id));
        }
        public List<Depot> SelectAllDepotInactif()
        {
            return dalDepot.SelectAllDepotInactif();
        }
        public List<Depot> SelectAllDepotActif()
        {
            return dalDepot.SelectAllDepotActif();
        }
        public Depot SelectDepotByIDVille(int id)
        {
            return dalDepot.SelectByIdVille(id);
        }

        public IEnumerable<SelectListItem> SelectAllDepotInList()
        {
            return dalDepot.SelectAllDepotInList();
        }

        public IEnumerable<SelectListItem> SelectAllDepotByPaysInList(int idPays) 
        {
            return dalDepot.SelectAllDepotByPaysInList(idPays);
        }



        public IEnumerable<SelectListItem> SelectAllDepotRetourByDepotDepartInList(int idDepotDepart)
        {
            return dalDepot.SelectAllDepotRetourByDepotDepartInList(idDepotDepart);
        }


    }
}
