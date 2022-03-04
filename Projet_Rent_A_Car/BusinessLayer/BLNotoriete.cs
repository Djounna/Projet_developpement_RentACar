using DataAccessLayer;
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
            lstNotoriete = notoriete.SelectAllNotoriete();           

            return lstNotoriete;
        }

        public List<Notoriete> GetAllNotorieteInactif()
        {
            List<Notoriete> lstNotoriete;
            List<Notoriete> lstInactif = new List<Notoriete>();
            DALNotoriete notoriete = new DALNotoriete();
            lstNotoriete = notoriete.SelectAllNotoriete();

            foreach (Notoriete not in lstNotoriete)
            {
                if (not.Inactif ==true)
                {
                    lstInactif.Add(not);
                }
            }

            return lstInactif;
        }
        public List<Notoriete> GetAllNotorieteActif()
        {
            List<Notoriete> lstNotoriete;
            List<Notoriete> lstActif = new List<Notoriete>();
            DALNotoriete notoriete = new DALNotoriete();
            lstNotoriete = notoriete.SelectAllNotoriete();    
            
            foreach (Notoriete not in lstNotoriete)
            {
                if (not.Inactif == null || not.Inactif == false)
                {
                    lstActif.Add(not);
                }
            }

            return lstActif;
        }


        public void CreateNotoriete(Notoriete notoriete)
        {

            DALNotoriete dalNotoriete = new DALNotoriete();
            dalNotoriete.CreateNotoriete(notoriete);

        }

        public Notoriete GetNotorieteByID(int id)
        {
            Notoriete notoriete;
            DALNotoriete dalNotoriete = new DALNotoriete();
            notoriete = dalNotoriete.SelectByID(id);
            return notoriete;
        }

        public void UptadeNotoriete(Notoriete notoriete)
        {

            DALNotoriete dalNotoriete = new DALNotoriete();
            dalNotoriete.UptadeNotoriete(notoriete);

        }

        public void DeleteNotoriete(int id)
        {

            DALNotoriete dalNotoriete = new DALNotoriete();
            dalNotoriete.DeleteNotoriete(id);

        }
    }
}
