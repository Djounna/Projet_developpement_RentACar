using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALPrix
    {
        private DalCommun dal = new();

        public List<Prix> SelectAllPrix() 
        { 
            return dal.dbcontext.Prix.Where(p=>p.DateFin==null).ToList();
        }

        public Prix SelectPrixByPays(int id)
        {
            return dal.dbcontext.Prix.Where(p=>p.Idpays==id && p.DateFin == null).FirstOrDefault();
        }

        public bool UpdatePrix(Prix prix)
        {
            var p = dal.dbcontext.Prix.Where(p=>p.Idprix==prix.Idprix && p.DateFin==null).FirstOrDefault();
            p.DateFin = DateTime.Now;
            dal.InsertOrUpdate(p);
            prix.Idprix = 0;
           return dal.InsertOrUpdate(prix);
            
        }
    }
}
