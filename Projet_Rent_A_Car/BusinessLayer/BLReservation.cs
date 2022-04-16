using DataAccessLayer;
using Models;

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


        public bool Insert(Reservation reservation)
        {

            return dalReservation.Insert(reservation);
        }



        public bool Update(Reservation reservation)
        {
            return dalReservation.Update(reservation);
        }
        public bool StartReservation(Reservation reservation)
        {
            return dalReservation.StartReservation(reservation);
        }
        public bool CloseReservation(Reservation reservation)
        {
            return dalReservation.CloseReservation(reservation);
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
