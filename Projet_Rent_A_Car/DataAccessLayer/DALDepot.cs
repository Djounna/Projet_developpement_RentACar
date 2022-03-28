using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALDepot
    {
        private DalCommun dal = new();
        public List<Depot> SelectAllDepotInactif() //OK
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
        public List<Depot> SelectAllDepotActif() //OK
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
       
        public IEnumerable<SelectListItem> SelectAllDepotByPaysInList(int idPays) // Ok, testé avec Swagger
        {
            using (dal.dbcontext)
            {

                var lstville = from ville in dal.dbcontext.Ville where ville.Idpays == idPays select ville;

                //  dal.dbcontext.Ville.Where(ville => ville.Idpays == idPays).ToList();

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

                var depotIntro = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Dépot Départ ---"
                };
                lstDepot.Insert(0, depotIntro);

                return new SelectList(lstDepot, "Value", "Text");
            }
        }



        /* // Test Corentin en cours
        public IEnumerable<SelectListItem> SelectAllDepotRetourByDepotDepartInList(int idDepotDepart)
        {
            using (dal.dbcontext)
            {
                var lstForfait1 = from forfait in dal.dbcontext.Forfait where forfait.Iddepot1 == idDepotDepart select forfait;
                var lstForfait2 = from forfait in dal.dbcontext.Forfait where forfait.Iddepot2 == idDepotDepart select forfait;
                var lstForfaits = lstForfait1.Union(lstForfait2); // Union method removes duplicates

                var listIdDepotsPossible = new List<int>();
                foreach(var forfait in lstForfaits)
                {
                    if(forfait.Iddepot1 != idDepotDepart && !listIdDepotsPossible.Contains(forfait.Iddepot1)){
                        listIdDepotsPossible.Add(forfait.Iddepot1);
                    }
                    if (forfait.Iddepot2 != idDepotDepart && !listIdDepotsPossible.Contains(forfait.Iddepot2))
                    {
                        listIdDepotsPossible.Add(forfait.Iddepot2);
                    }
                }
                var listDepotsRetourPossible = new List<Depot>();   
                foreach (var IdDepot in listIdDepotsPossible)
                {
                    var depotRetourPossible = from depot in dal.dbcontext.Depot   // Erreur Swagger à cet endroit. Cast impossible
                                                        where depot.Iddepot == IdDepot select depot;
                    listDepotsRetourPossible.Add(depotRetourPossible);
                }             
                List<SelectListItem> lstDepot = listDepotsRetourPossible
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
                    Text = "--- select Dépot Retour ---"
                };
                lstDepot.Insert(0, depotIntro);

                var choixNoForfait = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Prix au Km/Pas de forfait ---"
                };
                lstDepot.Insert(1, choixNoForfait);
                return new SelectList(lstDepot, "Value", "Text");
            }
        }
        */

    }
}
