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
        //public List<Depot> SelectAllDepotInactif()
        //{
        //    List<Depot> lstInactif = new List<Depot>();
        //    List<Depot> lstDep = dal.SelectAll<Depot>();
        //    foreach (Depot dep in lstDep)
        //    {
        //        if (dep.Inactif == true)
        //        {
        //            lstInactif.Add(dep);
        //        }
        //    }
        //    return lstInactif;
        //}

        public List<Depot> SelectAllDepotInactif() // Test Corentin, code plus court avec Linq, Ok, à valider par Antoine
        {
            List<Depot> lstInactif = dal.dbcontext.Depot.Where(Depot => Depot.Inactif == true).ToList<Depot>();
            return lstInactif;
        }

        //public List<Depot> SelectAllDepotActif()
        //{
        //    List<Depot> lstActif = new List<Depot>();
        //    List<Depot> lstDep = dal.SelectAll<Depot>();
        //    foreach (Depot dep in lstDep)
        //    {
        //        if (dep.Inactif == null || dep.Inactif == false)
        //        {
        //            lstActif.Add(dep);
        //        }
        //    }
        //    return lstActif;
        //}

        public List<Depot> SelectAllDepotActif() // Test Corentin, code plus court avec Linq, Ok, à valider par Antoine
        {
            List<Depot> lstActif = dal.dbcontext.Depot.Where(Depot => Depot.Inactif == false || Depot.Inactif == null).ToList<Depot>();
            return lstActif;
        }

        public Depot SelectDepotByIDVille(int id)
        {
            return dalDepot.SelectByIdVille(id);
        }
    }
}
