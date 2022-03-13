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
        DalCommun dal = new();
        DALNotoriete dalNotoriete = new();

        //private bool AlreadyExist(string nom,int id)     // Mis dans la DAL, OK COrentin, à valider par Antoine
        //{
        //    var notoriete = dal.dbcontext.Notoriete.SingleOrDefault(p => p.Libelle==nom && p.Idnotoriete != id);
        //    return (notoriete != null);
        //}

        public List<Notoriete> SelectAllNotoriete()
        {
            return dal.SelectAll<Notoriete>();
        }
        public Notoriete SelectNotorieteByID(int id)
        {
            return dal.SelectById<Notoriete>(id);
        }
        public bool InsertOrUpdateNotoriete(Notoriete notoriete) // modifié voir ci-dessus, OK Corentin, à valider par Antoine
        {
            if (!dalNotoriete.AlreadyExist(notoriete.Libelle,notoriete.Idnotoriete))
            {
                dal.InsertOrUpdate(notoriete);
                return true;
            }
            return false;          
        }
        public void DeleteNotoriete(int id)
        {
            dal.Delete(SelectNotorieteByID(id));
        }
        public List<Notoriete> SelectAllNotorieteInactif() // OK Corentin, à valider
        {
            return dalNotoriete.SelectAllNotorieteInactif();
        }
        public List<Notoriete> SelectAllNotorieteActif() // OK COrentin, à valider
        {
            return dalNotoriete.SelectAllDNotorieteActif();
        }

        //public List<Notoriete> SelectAllNotorieteInactif()
        //{          
        //    List<Notoriete> lstInactif = new List<Notoriete>();
        //    List<Notoriete> lstNotoriete = dal.SelectAll<Notoriete>();
        //    foreach (Notoriete not in lstNotoriete)
        //    {
        //        if (not.Inactif ==true)
        //        {
        //            lstInactif.Add(not);
        //        }
        //    }
        //    return lstInactif;
        //}
        //public List<Notoriete> SelectAllNotorieteActif()
        //{           
        //    List<Notoriete> lstActif = new List<Notoriete>();
        //    List<Notoriete> lstNotoriete = dal.SelectAll<Notoriete>();
        //    foreach (Notoriete not in lstNotoriete)
        //    {
        //        if (not.Inactif == null || not.Inactif == false)
        //        {
        //            lstActif.Add(not);
        //        }
        //    }
        //    return lstActif;
        //}        
    }
}
