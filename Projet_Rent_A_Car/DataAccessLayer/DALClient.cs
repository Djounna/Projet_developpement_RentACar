using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALClient
    {
        private DalCommun dal = new();
        public Client SelectClientByMail(string mail)
        {          
            try
            {
               return dal.dbcontext.Client.Where(x => x.Mail == mail).FirstOrDefault();
                          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
