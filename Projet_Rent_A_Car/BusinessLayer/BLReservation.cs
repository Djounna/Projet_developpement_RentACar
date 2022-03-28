using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLReservation
    {
        DalCommun dal = new();
        DALReservation dalReservation = new();

        public List<Reservation> SelectAllReservation()
        {
            return dal.SelectAll<Reservation>();
        }
        public Reservation SelectReservationByID(int id)
        {
            return dal.SelectById<Reservation>(id);
        }

        // A supprimer, pas utilisée.
        public void InsertOrUpdateReservation(Reservation Reservation)
        {
            dal.InsertOrUpdate(Reservation);
        }


        public void DeleteReservation(int id) // Pas validée
        {
            dal.Delete(SelectReservationByID(id));
        }

        // Passage par les méthodes DAL ADO
        public void Insert(Reservation reservation)
        {

            // reservation.DateReservation = DateTime.Now;
            

            dalReservation.Insert(reservation);
        }
        public void Update(Reservation reservation)
        {
            dalReservation.Update(reservation);
        }




        /*
            public List<Reservation> SelectAllReservationEnCours() 
            {
                return dalReservation.SelectAllReservationEnCours();
            }
            public List<Reservation> SelectAllReservationCloturees() 
            {
                return dalReservation.SelectAllDReservationCloturees();
            }


        */

        

    }
}
