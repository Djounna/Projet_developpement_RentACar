using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DalCommun
    {
        public ProjetSGDBContext dbcontext; // mettre en private et faire un get ? Antoine ?
        public DalCommun()
        {
            dbcontext = new ProjetSGDBContext();
        }
        public async void InsertOrUpdate(object o)   // En cours Corentin, ok actuellement, à valider par Antoine
        {
            try
            {
                dbcontext.Update(o);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async void Delete(object o) // En cours Corentin,o ok actuellement, à valider par ANtoine
        {
            try
            {
                dbcontext.Remove(o);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        


    }
}
