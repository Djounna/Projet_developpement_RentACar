namespace DataAccessLayer
{
    public class DalCommun
    {
        public ProjetSGDBContext dbcontext; 
        public DalCommun()
        {
            dbcontext = new ProjetSGDBContext();
        }

        public List<T> SelectAll<T>() where T : class 
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

        public T SelectById<T>(int id) where T : class    
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

        public bool InsertOrUpdate(object o)   
        {
            try
            {
                dbcontext.Update(o);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(object o) 
        {
            try
            {
                dbcontext.Remove(o);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
