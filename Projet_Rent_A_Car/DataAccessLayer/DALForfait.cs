using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace DataAccessLayer
{

    public class DALForfait
    {
        DalCommun dal = new();

        public List<Forfait> SelectAllForfait()
        {
            return dal.dbcontext.Forfait.Where(Forfait => Forfait.DateFin == null).ToList();
        }

        public Forfait SelectById(int id)
        {
            return dal.dbcontext.Forfait.Where(forfait => forfait.Idforfait == id && forfait.DateFin == null).SingleOrDefault();
        }
        public Forfait SelectForfaitByIDDepot(int id)
        {
            return dal.dbcontext.Forfait.Where(forfait => forfait.Iddepot1 == id || forfait.Iddepot2 == id).FirstOrDefault();
        }


        public bool Insert(Forfait forfait)
        {
            string sql = "InsertForfait";
            using (SqlConnection oCon = new SqlConnection(DALConnexion.Connexion))
            {
                using (SqlCommand oCmd = new SqlCommand(sql, oCon))
                {
                    try
                    {
                        oCon.Open();
                        oCmd.Connection = oCon;
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@IdDep1", forfait.Iddepot1));
                        oCmd.Parameters.Add(new SqlParameter("@IdDep2", forfait.Iddepot2));
                        oCmd.Parameters.Add(new SqlParameter("@prix", forfait.Prix));
                        oCmd.Parameters.Add(new SqlParameter("@DateDeb", forfait.DateDebut));


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

        public bool Update(Forfait forfait)
        {
            string sql = "UpdateForfait";
            using (SqlConnection oCon = new SqlConnection(DALConnexion.Connexion))
            {
                using (SqlCommand oCmd = new SqlCommand(sql, oCon))
                {
                    try
                    {
                        oCon.Open();
                        oCmd.Connection = oCon;
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@dep1", forfait.Iddepot1));
                        oCmd.Parameters.Add(new SqlParameter("@dep2", forfait.Iddepot2));
                        oCmd.Parameters.Add(new SqlParameter("@prix", forfait.Prix));
                        oCmd.Parameters.Add(new SqlParameter("@dateDeb", forfait.DateDebut));
                        oCmd.Parameters.Add(new SqlParameter("@DateFin", forfait.DateFin));
                        oCmd.Parameters.Add(new SqlParameter("@id", forfait.Idforfait));

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

        public Forfait SelectForfaitReservation(int idDepot1, int idDepot2) // En test sutie modif
        {
            Forfait forf = dal.dbcontext.Forfait.Where(forfait => forfait.Iddepot1 == idDepot1 && forfait.Iddepot2 == idDepot2 && forfait.DateFin == null).FirstOrDefault();
            if (forf is null)
            {
                forf = dal.dbcontext.Forfait.Where(forfait => forfait.Iddepot2 == idDepot1 && forfait.Iddepot1 == idDepot2 && forfait.DateFin == null).FirstOrDefault();
            }

            return forf;
        }


        public bool AlreadyExist(Forfait forfait) // En Test
        {
            return (SelectForfaitReservation(forfait.Iddepot1, forfait.Iddepot2) != null);
        }

    }
}
