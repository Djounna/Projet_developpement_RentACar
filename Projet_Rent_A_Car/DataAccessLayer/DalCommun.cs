using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
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

        public List<T> SelectAll<T>() where T : class // OK Corentin, à valider par Antoine
        {
            try
            {
                return dbcontext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public T SelectById<T>(int id) where T : class   // OK Corentin, à valider par Antoine
        {        
             try
            {
                return dbcontext.Set<T>().Find(id);
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }  

        public async void InsertOrUpdate(object o)   // Ok Corentin à valider par Antoine
        {
            try
            {
                dbcontext.Update(o);
                await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async void Delete(object o) // Ok Corentin, à valider par Antoine
        {
            try
            {
                dbcontext.Remove(o);
                await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
