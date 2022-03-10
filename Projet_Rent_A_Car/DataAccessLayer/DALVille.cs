using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALVille
    {

        private DalCommun dal = new();
        public List<Ville> SelectVilleByPays(int idPays)
        {
            using (dal.dbcontext)
            {
                List<Ville> lstville = dal.dbcontext.Ville.Where(ville => ville.Idpays == idPays).ToList();
                return lstville;                      
            }
        }
    }
}
