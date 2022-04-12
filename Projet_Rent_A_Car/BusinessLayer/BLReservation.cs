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
        // Passage par les méthodes DAL ADO


        public void Insert(Reservation reservation)
        {
          
            dalReservation.Insert(reservation);
        }



        public void Update(Reservation reservation)
        {
            dalReservation.Update(reservation);
        }
        public void StartReservation(Reservation reservation)
        {
            dalReservation.StartReservation(reservation);
        }
        public void CloseReservation(Reservation reservation)
        {
            dalReservation.CloseReservation(reservation);
        }



        public List<Reservation> SelectAllReservationNotYetStarted()// Ok
        {
            try
            {
                return dalReservation.SelectAllReservationNotYetStarted();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Reservation> SelectAllReservationCloturees() // Ok
        {
            try
            {
                return dalReservation.SelectAllReservationCloturees();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Reservation> SelectAllLocationEnCours() // Ok
        {
            try
            {
               return dalReservation.SelectAllLocationEnCours();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteReservation(int id) // En cours Corentin
        {
            dal.Delete(SelectReservationByID(id));
        }


     

    }
}
