using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLNotoriete
    {
        public List<Notoriete> GetAllNotoriete()
        {
            List<Notoriete> lstNotoriete;
            DALNotoriete notoriete = new DALNotoriete();
            lstNotoriete = notoriete.SelectAllPays();
            return lstNotoriete;
        }


        public void CreatePays(Notoriete notoriete)
        {

            DALNotoriete dalNotoriete = new DALNotoriete();
            dalNotoriete.CreatePays(notoriete);

        }

        public Pays GetNotorieteByID(int id)
        {
            Notoriete notoriete;
            DALNotoriete dalNotoriete = new DALNotoriete();
            notoriete = dalNotoriete.SelectByID(id);
            return pays;
        }

        public void UptadeNotoriete(Notoriete notoriete)
        {

            DALNotoriete dalNotoriete = new DALNotoriete();
            dalNotoriete.UptadePays(notoriete);

        }

        public void DeleteNotoriete(int id)
        {

            DALNotoriete dalNotoriete = new DALNotoriete();
            dalNotoriete.DeleteNotoriete(id);

        }
    }
}
