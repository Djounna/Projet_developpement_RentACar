using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void InsertOrUpdateDepot(Depot depot)
        {
            dal.InsertOrUpdate(depot);
        }
        public void DeleteDepot(int id)
        {
            dal.Delete(SelectDepotByID(id));
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

        public IEnumerable<SelectListItem> SelectAllDepotByPaysInList(int idPays) // Ok testé avec swagger
        {
            return dalDepot.SelectAllDepotByPaysInList(idPays);
        }


        // En cours Corentin
        /*
        public IEnumerable<SelectListItem> SelectAllDepotRetourByDepotDepartInList(int idDepotDepart)
        {
            return dalDepot.SelectAllDepotRetourByDepotDepartInList(idDepotDepart);
        }
        */

    }
}
