using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;


namespace BusinessLayer
{
    public class BLVoiture // OK Corentin, à valider
    {
        DalCommun dal = new();
        DALVoiture dalVoiture = new();
        public List<Voiture> SelectAllVoiture()
        {
            return dal.SelectAll<Voiture>();
        }
        public Voiture SelectVoitureByID(int id)
        {
            return dal.SelectById<Voiture>(id);
        }

        public bool AlreadyExist(Voiture v)
        {
            return dalVoiture.AlreadyExist(v.Immatriculation, v.Idvoiture);
        }
        public bool InsertOrUpdateVoiture(Voiture Voiture)
        {
            return dal.InsertOrUpdate(Voiture);
        }
        public bool DeleteVoiture(int id)
        {
            return dal.Delete(SelectVoitureByID(id));
        }
        public List<Voiture> SelectAllVoitureInactif()
        {
            return dalVoiture.SelectAllVoitureInactif();
        }
        public List<Voiture> SelectAllVoitureActif()
        {
            return dalVoiture.SelectAllVoitureActif();
        }

        public IEnumerable<SelectListItem> SelectAllVoitureDisponibleInList(int IdDepot, DateTime DateLocation)
        {
                
            if(DateLocation.Year < DateTime.Now.Year)
            {
                List<SelectListItem> lstVoit = new List<SelectListItem>();

                lstVoit.Add(new SelectListItem
                {
                    Value = "99999",
                    Text = "Erreur: date de départ invalide"
                });

                return lstVoit;
            }

            return dalVoiture.SelectAllVoitureDisponibleInList(IdDepot, DateLocation);
        }

    }
}
