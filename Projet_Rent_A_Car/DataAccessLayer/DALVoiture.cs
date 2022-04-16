using Microsoft.AspNetCore.Mvc.Rendering;
using Models;


namespace DataAccessLayer
{
    public class DALVoiture
    {
        private DalCommun dal = new();
        private DALReservation dalReservation = new();
        public List<Voiture> SelectAllVoitureInactif() // OK 
        {
            try
            {
                return dal.dbcontext.Voiture.Where(Voiture => Voiture.Inactif == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Voiture> SelectAllVoitureActif() // Ok
        {
            try
            {
                return dal.dbcontext.Voiture.Where(Voiture => Voiture.Inactif != true).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Voiture> SelectAllVoitureInLocation()
        {
            List<Reservation> listReservation = dalReservation.SelectAllLocationEnCours();
            List<Voiture> listVoiture = new();
            foreach (Reservation res in listReservation)
            {
                Voiture voit = dal.dbcontext.Voiture.Where(voiture => voiture.Idvoiture == res.Idvoiture).SingleOrDefault();
                if (!listVoiture.Contains(voit))
                {
                    listVoiture.Add(voit);
                }
            }
            return listVoiture;
        }
        // OK
        public IEnumerable<SelectListItem> SelectAllVoitureDisponibleInList(int IdDepot, DateTime DateLocation)
        {
            
            var lstVoit = from voiture in dal.dbcontext.Voiture where voiture.Inactif != true && voiture.Iddepot == IdDepot select voiture;
            
            List<int> idLstVoit = lstVoit.Select(v => v.Idvoiture).ToList();
            List<int> idLstVoitRes = dal.dbcontext.Reservation.Where(r => r.DateRetour == null ).Select(v => v.Idvoiture).ToList();
            List<int> idVoitSsRes = (from id in idLstVoit select id).Except(idLstVoitRes).ToList();

                List<SelectListItem> lstVoiture = dal.dbcontext.Reservation.Where(r => r.DateRetour < DateLocation && r.KilometrageRetour == null).Join(
                lstVoit, r => r.Idvoiture, v => v.Idvoiture, (r, v) =>
                new SelectListItem
                {
                    Value = r.Idvoiture.ToString(),
                    Text = v.Marque
                }).ToList();

                List<SelectListItem> lstVoitureSsRes = dal.dbcontext.Voiture.Where(v => idVoitSsRes.Contains(v.Idvoiture)).Select(v =>
                           new SelectListItem
                           {
                               Value = v.Idvoiture.ToString(),
                               Text = v.Marque + " / " + v.IdnotorieteNavigation.Libelle.ToString()
                           }).ToList();

                lstVoiture.AddRange(lstVoitureSsRes);

                if (lstVoiture.Count == 0)
                {
                    lstVoiture.Add(new SelectListItem
                    {
                        Value = "99999",
                        Text = "Pas de voiture disponible dans ce dépôt à cette date"
                    });
                }
            
            return lstVoiture;


        }

        public bool AlreadyExist(string nom, int id)
        {
            var voiture = dal.dbcontext.Voiture.SingleOrDefault(p => p.Immatriculation == nom && p.Idvoiture != id);
            return (voiture != null);
        }

        /*
        public List<Voiture> SelectAllVoitureActiveByDepot(int IdDepot)
        {
            var lstVoit = dal.dbcontext.Voiture.Where(v => v.Inactif != true && v.Iddepot == IdDepot).ToList();
            return lstVoit;

        }

        public List<Reservation> SelectAllReservationByVoiture(int idVoiture, DateTime DateLocation)
        {
            var lstRes = dal.dbcontext.Reservation.Where(r => r.Idvoiture == idVoiture && r.DateRetour < DateLocation).ToList();
            return lstRes;
        }
        */

    }
}
