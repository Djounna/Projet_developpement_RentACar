using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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


        // Test Corentin

        public List<Voiture> SelectAllVoitureInLocation()
        {
           List<Reservation> listReservation = dalReservation.SelectAllLocationEnCours();
            List<Voiture> listVoiture = new();
            foreach(Reservation res in listReservation)
            {
                Voiture voit = dal.dbcontext.Voiture.Where(voiture => voiture.Idvoiture == res.Idvoiture).SingleOrDefault();
                if (!listVoiture.Contains(voit))
                {
                    listVoiture.Add(voit);
                }
            }
            return listVoiture;
        }

        // Corentin en cours

       

        public IEnumerable<SelectListItem> SelectAllVoitureDisponibleInList(int IdDepot, DateTime DateLocation)
        {
            var lstVoit = dal.dbcontext.Voiture.Where(v => v.Inactif != true && v.Iddepot == IdDepot).ToList();
            

            var lstVoitDispo = from reservation in dal.dbcontext.Reservation join voiture in lstVoit 
                         on reservation.Idvoiture equals voiture.Idvoiture where reservation.DateRetour < DateLocation select voiture;

            List<SelectListItem> lstVoiture = lstVoitDispo.Select(v => new SelectListItem
            {
                Value = v.Idvoiture.ToString(),
                Text = v.Marque
            }).ToList();

            return lstVoiture;


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
