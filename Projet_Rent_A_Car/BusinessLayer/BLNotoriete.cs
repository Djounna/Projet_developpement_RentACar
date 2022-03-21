using DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<Notoriete> SelectAllNotorieteInactif() // OK 
        {
            return dalNotoriete.SelectAllNotorieteInactif();
        }
        public List<Notoriete> SelectAllNotorieteActif() // OK 
        {
            return dalNotoriete.SelectAllDNotorieteActif();
        }
        
        public IEnumerable<SelectListItem> SelectAllNotorieteInList() // Corentin EN cours
        {
            return dalNotoriete.SelectAllNotorieteInList();
        }
        
               
    }
}
