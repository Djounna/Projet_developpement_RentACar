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

        public void InsertOrUpdate(Forfait forfait)
        {
            string sql = "InsertOrUptadeForfait";
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
    }
}
