using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return dal.dbcontext.Forfait.Where(forfait => forfait.Idforfait == id && forfait.DateFin==null).SingleOrDefault();
        }
        public Forfait SelectForfaitByIDDepot(int id)
        {
            return dal.dbcontext.Forfait.Where(forfait => forfait.Iddepot1 == id || forfait.Iddepot2 == id).FirstOrDefault();
        }

        public void Insert(Forfait forfait)
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
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
        }

        public void Update(Forfait forfait)
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
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
        }

    }
}
