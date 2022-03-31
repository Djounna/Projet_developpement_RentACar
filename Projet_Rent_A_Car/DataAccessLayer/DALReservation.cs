using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALReservation
    {
        private DalCommun dal = new();


        // Méthodes ADO
        public void Insert(Reservation reservation)
        {
            string sql = "InsertReservation";
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
                        oCmd.Parameters.Add(new SqlParameter("@IdDepotRetour", reservation.IddepotRetour));
                        oCmd.Parameters.Add(new SqlParameter("@IdForfait", reservation.Idforfait));
                        oCmd.Parameters.Add(new SqlParameter("@DateReservation", reservation.DateReservation));
                        oCmd.Parameters.Add(new SqlParameter("@DateDepart", reservation.DateDepart));
                        // oCmd.Parameters.Add(new SqlParameter("@DateRetour", reservation.DateRetour));
                        //oCmd.Parameters.Add(new SqlParameter("@KilometrageDepart", reservation.KilometrageDepart));
                        //oCmd.Parameters.Add(new SqlParameter("@KilometrageRetour", reservation.KilometrageRetour));
                        oCmd.Parameters.Add(new SqlParameter("@Coefficient_Multiplicateur", reservation.CoefficientMultiplicateur));

                        int result = oCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
        }

        public void Update(Reservation reservation)
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
                        oCmd.Parameters.Add(new SqlParameter("@IdClient", reservation.Idclient));
                        oCmd.Parameters.Add(new SqlParameter("@IdVoiture", reservation.Idvoiture));
                        oCmd.Parameters.Add(new SqlParameter("@IdDepotDepart", reservation.IddepotDepart));
                        oCmd.Parameters.Add(new SqlParameter("@IdDepotRetour", reservation.IddepotRetour));
                        oCmd.Parameters.Add(new SqlParameter("@DateReservation", reservation.DateReservation));
                        oCmd.Parameters.Add(new SqlParameter("@DateDepart", reservation.DateDepart));
                        // oCmd.Parameters.Add(new SqlParameter("@DateRetour", reservation.DateRetour));
                        // oCmd.Parameters.Add(new SqlParameter("@KilometrageDepart", reservation.KilometrageDepart));
                        // oCmd.Parameters.Add(new SqlParameter("@KilometrageRetour", reservation.KilometrageRetour));
                        oCmd.Parameters.Add(new SqlParameter("@Coefficient_Multiplicateur", reservation.CoefficientMultiplicateur));
                        int result = oCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
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
                List<Reservation> lst = dal.dbcontext.Reservation.Where(res=>res.KilometrageDepart != null && res.KilometrageRetour ==null ).ToList();        
                   return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
