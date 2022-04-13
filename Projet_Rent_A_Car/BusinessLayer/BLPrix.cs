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
    public class BLPrix
    {
        private DALPrix dalPrix = new();
        private DalCommun dal = new();
        public List<Prix> SelectAllPrix()
        {
            return dalPrix.SelectAllPrix();
        }

        public Prix SelectPrixByPays(int id)
        {
            return dalPrix.SelectPrixByPays(id);
        }
        public Prix SelectPrixByID(int id)
        {
            return dal.SelectById<Prix>(id);
        }
        public void InsertOrUpdatePrix(Prix Prix)
        {
            Prix p = SelectPrixByPays(Prix.Idpays);

            if (p != null)
            {     
                Prix prixACloturer = SelectPrixByID(p.Idprix);
                prixACloturer.DateFin = DateTime.Now;
                dal.InsertOrUpdate(prixACloturer);
                Prix.Idprix = 0;
            }

            dal.InsertOrUpdate(Prix);
        }
        public void UpdatePrix(Prix prix)
        {
            
            dalPrix.UpdatePrix(prix);
        }
    }
}
