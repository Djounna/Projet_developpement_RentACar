using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace DataAccessLayer
{
    public class DALDepot
    {
        private DalCommun dal = new();
        public List<Depot> SelectAllDepotInactif() 
        {
            try
            {
                return dal.dbcontext.Depot.Where(Depot => Depot.Inactif == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Depot> SelectAllDepotActif()
        {
            try
            {
                return dal.dbcontext.Depot.Where(Depot => Depot.Inactif != true).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Depot SelectByIdVille(int id)
        {
            try
            {
                return dal.dbcontext.Depot.Where(dep => dep.Idville == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SelectListItem> SelectAllDepotInList()
        {
            using (dal.dbcontext)
            {

                List<SelectListItem> lstDepot = dal.dbcontext.Depot
                    .Join(dal.dbcontext.Ville,
                    dep => dep.Idville,
                    vil => vil.Idville,
                    (dep, vil) =>
                       new SelectListItem
                       {
                           Value = dep.Iddepot.ToString(),
                           Text = vil.Nom,
                       }).OrderBy(x => x.Text)
                    .ToList();

                var depotIntro = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Dépot ---"
                };
                lstDepot.Insert(0, depotIntro);

                return new SelectList(lstDepot, "Value", "Text");

            }
        }

        public IEnumerable<SelectListItem> SelectAllDepotByPaysInList(int idPays) 
        {
            using (dal.dbcontext)
            {

                var lstville = from ville in dal.dbcontext.Ville where ville.Idpays == idPays select ville;                

                List<SelectListItem> lstDepot = dal.dbcontext.Depot
                    .Join(lstville,
                    dep => dep.Idville,
                    vil => vil.Idville,
                    (dep, vil) =>
                       new SelectListItem
                       {
                           Value = dep.Iddepot.ToString(),
                           Text = vil.Nom,
                       }).OrderBy(x => x.Text)
                    .ToList();



                return new SelectList(lstDepot, "Value", "Text");
            }
        }



        public IEnumerable<SelectListItem> SelectAllDepotRetourByDepotDepartInList(int idDepotDepart)
        {

            var lstForfait1 = from forfait in dal.dbcontext.Forfait where forfait.Iddepot1 == idDepotDepart select forfait;
            var lstForfait2 = from forfait in dal.dbcontext.Forfait where forfait.Iddepot2 == idDepotDepart select forfait;
            List<Forfait> lstForfaits = new List<Forfait>();
            lstForfaits.AddRange(lstForfait1.Union(lstForfait2).ToList()); // Union method removes duplicates
            SortedSet<int> lstDepots = new();
            foreach (Forfait forfait in lstForfaits)
            {
                if (forfait.Iddepot1 != idDepotDepart && forfait.DateFin == null)
                    lstDepots.Add(forfait.Iddepot1);
                if (forfait.Iddepot1 == idDepotDepart && forfait.DateFin == null)
                    lstDepots.Add(forfait.Iddepot2);
            }

            var listIdVilleForfaitDepot = new List<int>();
            var listIdVilleDepotsPossible = dal.dbcontext.Depot.Where(depot => lstDepots.Contains(depot.Iddepot));

            List<SelectListItem> lstville = listIdVilleDepotsPossible
            .Join(dal.dbcontext.Ville,
                dep => dep.Idville,
                vil => vil.Idville,
                (dep, vil) =>
                   new SelectListItem
                   {
                       Value = dep.Iddepot.ToString(),
                       Text = vil.Nom,
                   })
                   .OrderBy(x => x.Text)
                   .ToList();

            var choixNoForfait = new SelectListItem()
            {
                Value = "999",
                Text = "--- Prix au Km/Pas de forfait ---"
            };

            lstville.Insert(0, choixNoForfait);
            return new SelectList(lstville, "Value", "Text");

        }

    }
}
