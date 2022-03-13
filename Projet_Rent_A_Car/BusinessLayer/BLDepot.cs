using DataAccessLayer;
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
            List<Depot> lstInactif = dal.dbcontext.Depot.Where(Depot => Depot.Inactif == true).ToList();
            return lstInactif;
        }

        public List<Depot> SelectAllDepotActif() 
        {
            List<Depot> lstActif = dal.dbcontext.Depot.Where(Depot => Depot.Inactif !=true).ToList(); 
           
            return lstActif;
        }

        public Depot SelectDepotByIDVille(int id)
        {
            return dalDepot.SelectByIdVille(id);
        }
    }
}
