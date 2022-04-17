using Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DALReservation
    {
        private DalCommun dal = new();


        // Méthodes ADO
        public bool Insert(Reservation reservation)
        {
            string sql = sql = "InsertReservationWithoutForfait"; ;
            if(reservation.Idforfait != null)
            sql = "InsertReservationWithForfait";

            using (SqlConnection oCon = new SqlConnection(DALConnexion.Connexion))
            {
                using (SqlCommand oCmd = new SqlCommand(sql, oCon))
                {
                    try
                    {
                        oCon.Open();
                        oCmd.Connection = oCon;
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@IdClient", reservation.Idclient));
                        oCmd.Parameters.Add(new SqlParameter("@IdVoiture", reservation.Idvoiture));
                        oCmd.Parameters.Add(new SqlParameter("@IdDepotDepart", reservation.IddepotDepart));
                        if (reservation.Idforfait != null)
                        {
                            oCmd.Parameters.Add(new SqlParameter("@IdDepotRetour", reservation.IddepotRetour));
                            oCmd.Parameters.Add(new SqlParameter("@IdForfait", reservation.Idforfait));                           
                        }
                        
                        oCmd.Parameters.Add(new SqlParameter("@DateReservation", reservation.DateReservation));
                        oCmd.Parameters.Add(new SqlParameter("@DateDepart", reservation.DateDepart));
                     
                        oCmd.Parameters.Add(new SqlParameter("@DateRetour", reservation.DateRetour));
                        
                        oCmd.Parameters.Add(new SqlParameter("@Coefficient_Multiplicateur", reservation.CoefficientMultiplicateur));

                        int result = oCmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
        }

        public bool Update(Reservation reservation) // Nope
        {
            string sql = "UpdateReservation";
            using (SqlConnection oCon = new SqlConnection(DALConnexion.Connexion))
            {
                using (SqlCommand oCmd = new SqlCommand(sql, oCon))
                {
                    try
                    {
                        oCon.Open();
                        oCmd.Connection = oCon;
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@IdReservation", reservation.Idreservation));    
                        oCmd.Parameters.Add(new SqlParameter("@IdDepotRetour", reservation.IddepotRetour));                       
                        oCmd.Parameters.Add(new SqlParameter("@DateRetour", reservation.DateRetour));
                        oCmd.Parameters.Add(new SqlParameter("@KilometrageDepart", reservation.KilometrageDepart));
                        oCmd.Parameters.Add(new SqlParameter("@KilometrageRetour", reservation.KilometrageRetour));                       
                        int result = oCmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public bool StartReservation(Reservation reservation) // ADO
        {
            string sql = "StartReservation";
            using (SqlConnection oCon = new SqlConnection(DALConnexion.Connexion))
            {
                using (SqlCommand oCmd = new SqlCommand(sql, oCon))
                {
                    try
                    {
                        oCon.Open();
                        oCmd.Connection = oCon;
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@IdReservation", reservation.Idreservation));
                        
                        oCmd.Parameters.Add(new SqlParameter("@DateDepart", reservation.DateDepart));
                        
                        oCmd.Parameters.Add(new SqlParameter("@KilometrageDepart", reservation.KilometrageDepart));
                        
                        int result = oCmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
        }

        public bool CloseReservation(Reservation reservation)
        {
            string sql = "CloseReservationWithoutForfait";
            if (reservation.Idforfait != null)
                sql = "CloseReservationWithForfait";
            using (SqlConnection oCon = new SqlConnection(DALConnexion.Connexion))
            {
                using (SqlCommand oCmd = new SqlCommand(sql, oCon))
                {
                    try
                    {
                        oCon.Open();
                        oCmd.Connection = oCon;
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@IdReservation", reservation.Idreservation));
                        //oCmd.Parameters.Add(new SqlParameter("@IdClient", reservation.Idclient));
                        //oCmd.Parameters.Add(new SqlParameter("@IdVoiture", reservation.Idvoiture));
                        //oCmd.Parameters.Add(new SqlParameter("@IdDepotDepart", reservation.IddepotDepart));
                        oCmd.Parameters.Add(new SqlParameter("@IdDepotRetour", reservation.IddepotRetour));
                        //oCmd.Parameters.Add(new SqlParameter("@DateReservation", reservation.DateReservation));
                        //oCmd.Parameters.Add(new SqlParameter("@DateDepart", reservation.DateDepart));
                        oCmd.Parameters.Add(new SqlParameter("@DateRetour", reservation.DateRetour));
                        //oCmd.Parameters.Add(new SqlParameter("@KilometrageDepart", reservation.KilometrageDepart));
                        oCmd.Parameters.Add(new SqlParameter("@KilometrageRetour", reservation.KilometrageRetour));
                        //oCmd.Parameters.Add(new SqlParameter("@Coefficient_Multiplicateur", reservation.CoefficientMultiplicateur));
                        if (reservation.Idforfait != null)
                            oCmd.Parameters.Add(new SqlParameter("@IdForfait", reservation.Idforfait));
                        
                        oCmd.Parameters.Add(new SqlParameter("@Penalite", reservation.Penalite));
                        int result = oCmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
        }


        public List<Reservation> SelectAllReservationNotYetStarted()
        {
            try
            {
                List<Reservation> lst = dal.dbcontext.Reservation.Where(res => res.KilometrageDepart == null).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Reservation> SelectAllReservationCloturees()
        {
            try
            {
                List<Reservation> lst = dal.dbcontext.Reservation.Where(res => res.KilometrageRetour != null).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Reservation> SelectAllLocationEnCours()
        {
            try
            {
                List<Reservation> lst = dal.dbcontext.Reservation.Where(res => res.KilometrageDepart != null && res.KilometrageRetour == null).ToList();
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
