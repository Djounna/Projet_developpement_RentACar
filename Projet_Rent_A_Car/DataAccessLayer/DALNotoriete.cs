using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALNotoriete
    {
        private DalCommun dal = new DalCommun();

        
        //public List<Notoriete> SelectAllNotoriete() //Ok Antoine
        //{
        //    List<Notoriete> queryResult = new();
        //    try
        //    {
        //        queryResult.AddRange(dal.dbcontext.Notoriete);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return queryResult;
        //}
        
        
        public List<Notoriete> SelectAllNotoriete() 
        {
            List<Notoriete> queryResult = new();
            try
            {
                queryResult = dal.SelectAll<Notoriete>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryResult;

        }
        

        //public Notoriete SelectByID(int id)//Ok Antoine
        //{
        //    try
        //    {               
        //        return dal.dbcontext.Notoriete.Find(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        public Notoriete SelectById(int id)
        {
            try
            {
                return dal.SelectById<Notoriete>(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public async void InsertOrUpdateNotoriete(Notoriete notoriete) //Ok Corentin
        {
            try
            {
                dal.InsertOrUpdate(notoriete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            /* // A remplacer
            public async void CreateNotoriete(Notoriete notoriete) //Ok Antoine
            {
                try
                {
                    dbcontext.Update(notoriete);
                    var oResponse = await dbcontext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public async void UptadeNotoriete(Notoriete notoriete)//Ok Antoine
        {
            try
            {
                dbcontext.Update(notoriete);
                var oResponse = await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            */


            /* EN attente, voir pour remplacer
        public async void DeleteNotoriete(int id)//Ok Antoine
        {
            try
            {
                dal.dbcontext.Remove(dal.dbcontext.Notoriete.Find(id));
                var oResponse = await dal.dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
        public async void DeleteNotoriete(Notoriete notoriete)//Ok Corentin, à valider par Antoine
        {
            try
            {
                dal.Delete(notoriete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
