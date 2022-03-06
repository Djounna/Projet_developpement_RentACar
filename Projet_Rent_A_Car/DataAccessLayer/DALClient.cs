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
        private ProjetSGDBContext dbcontext = new ProjetSGDBContext();

        public Client SelectClientByMail(string mail)//OK Antoine
        {
            List<Client> c = new();
           
            try
            {
                c.AddRange(dbcontext.Client.Where(x => x.Mail == mail));
                return c[0];                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async void CreateClient(Client client)//Ok Antoine
        {
            try
            {
                dbcontext.Update(client);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
