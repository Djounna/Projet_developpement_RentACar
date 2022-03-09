﻿using DataAccessLayer;
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
        DALNotoriete dalNotoriete = new();
        //public List<Notoriete> SelectAllNotoriete()//ok Antoine
        //{            
        //    return dalNotoriete.SelectAllNotoriete();
        //}

        //public Notoriete GetNotorieteByID(int id)//ok Antoine
        //{
        //    return dalNotoriete.SelectById(id);
        //}

        //public void InsertOrUpdateNotoriete(Notoriete notoriete)//ok  Antoine
        //{
        //    dalNotoriete.InsertOrUpdateNotoriete(notoriete);
        //}

        //public void DeleteNotoriete(Notoriete notoriete)//ok  Antoine
        //{
        //    dalNotoriete.DeleteNotoriete(notoriete);
        //}

        DalCommun dal = new();
        public List<Notoriete> SelectAllNotoriete()// Ok Corentin, à valider par Antoine
        {
            return dal.SelectAll<Notoriete>();
        }
        public Notoriete GetNotorieteByID(int id)//Ok Corentin, à valider par Antoine
        {
            return dal.SelectById<Notoriete>(id);
        }
        public void InsertOrUpdateNotoriete(Notoriete notoriete)//ok Corentin, à valider par Antoine
        {
            dal.InsertOrUpdate(notoriete);
        }
        public void DeleteNotoriete(Notoriete notoriete)//ok Corentin, à valider par Antoine
        {
            dal.Delete(notoriete);
        }

        public List<Notoriete> SelectAllNotorieteInactif() //ok Antoine
        {          
            List<Notoriete> lstInactif = new List<Notoriete>();
            List<Notoriete> lstNotoriete = dalNotoriete.SelectAllNotoriete();
            foreach (Notoriete not in lstNotoriete)
            {
                if (not.Inactif ==true)
                {
                    lstInactif.Add(not);
                }
            }
            return lstInactif;
        }
        public List<Notoriete> SelectAllNotorieteActif()//ok Antoine
        {           
            List<Notoriete> lstActif = new List<Notoriete>();
            List<Notoriete> lstNotoriete = dalNotoriete.SelectAllNotoriete();              
            foreach (Notoriete not in lstNotoriete)
            {
                if (not.Inactif == null || not.Inactif == false)
                {
                    lstActif.Add(not);
                }
            }
            return lstActif;
        }

        

        

        /* // Doublon
        public void UptadeNotoriete(Notoriete notoriete)//ok Antoine
        {
            dalNotoriete.InsertOrUpdateNotoriete(notoriete);
        }
        */

        /* // En attente
        public void DeleteNotoriete(int id)//ok Antoine
        {
            dalNotoriete.DeleteNotoriete(id);
        }
        */

        
    }
}
