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
        private DALVille dalville = new();
        public List<Ville> SelectAllVille()//OK Antoine
        {           
            return dalville.SelectAllVille();
        }
        public void CreateVille(Ville ville)//OK Antoine
        {
            dalville.CreateVille(ville);
        }
        public Ville SelectVilleByID(int id)//OK Antoine
        {            
            return dalville.SelectByID(id);
        }
        public void UptadeVille(Ville ville)//OK Antoine
        {
            dalville.UptadeVille(ville);
        }
        public void DeleteVille(int id)//OK Antoine
        {
            dalville.DeleteVille(id);
        }
    }
}
