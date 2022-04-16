namespace DataAccessLayer
{
    public class DalCommun
    {
        public ProjetSGDBContext dbcontext; // mettre en private et faire un get ? 
        public DalCommun()
        {
            dbcontext = new ProjetSGDBContext();
        }

        public List<T> SelectAll<T>() where T : class // OK // Mettre des using ?
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

        //private static bool isActif(EntityObject o)  // Test Corentin, ne fonctionne pas actuellement. Problème avec la propriété qui n'est pas généralisable
        //{
        //    return (o.Inactif == false);
        //}

        //public List<T> SelectAllActif<T>() where T : Class // Test Corentin, ne fonctionne pas actuellement. voir ci-dessus
        //{
        //    List<T> lstAll = SelectAll<T>();
        //    List<T> lstActif = new List<T>();
        //    foreach (T element in lstAll)
        //    {
        //        if (element.Inactif == null || element.Inactif == false)
        //        {
        //            lstActif.Add(element);
        //        }
        //    }
        //    return lstActif;
        //}

        //public List<T> SelectAllActif<T>() where T : EntityObject // Test Corentin, ne fonctionne pas actuellement. Voir ci-dessus
        //{
        //    List<T> lstActif = dbcontext.Set<T>().Where(o => o.Inactif == true).ToList<T>();
        //    return lstActif;
        //}


        public T SelectById<T>(int id) where T : class   // OK 
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

        public bool InsertOrUpdate(object o)   // Ok 
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

        public bool Delete(object o) // Ok 
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
