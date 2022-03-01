using Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLVille
    {
        public List<Ville> GetAllVille()
        {
            List<Ville> lstVille;
            DALVille dalville = new DALVille();
            lstVille = dalville.SelectAllVille();
            return lstVille;
        }


        public void CreateVille(Ville ville)
        {

            DALVille dalville = new DALVille();
            dalville.CreateVille(ville);

        }

        public Ville GetVilleByID(int id)
        {
            
            DALVille dalville = new DALVille();
            Ville ville = dalville.SelectByID(id);
            return ville;
        }

        public void UptadeVille(Ville ville)
        {

            DALVille dalville = new DALVille();
            dalville.UptadeVille(ville);

        }

        public void DeleteVille(int id)
        {

            DALVille dalville = new DALVille();
            dalville.DeleteVille(id);

        }
    }
}
