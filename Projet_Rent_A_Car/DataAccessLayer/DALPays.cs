﻿
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    public class DALPays
    {
        public List<Pays> SelectAllPays()
        {
            List<Pays> queryResult = new List<Pays>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var listPays = from Pays in dbcontext.Pays select Pays;
                queryResult.AddRange(listPays);

            }
            catch (Exception ex)
            {


                throw ex;
            }

            return queryResult;
        }

        public async void CreatePays(Pays pays)
        {
            
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                dbcontext.Update(pays);
                var oResponse = await dbcontext.SaveChangesAsync();
                

            }
            catch (Exception ex)
            {


                throw ex;
            }

          
        }
        public Pays SelectByID(int id)
        {
            
            List<Pays> queryResult = new List<Pays>();
            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                var result = from Pays in dbcontext.Pays where Pays.IDPays==id select Pays;
                queryResult.AddRange(result);
                return queryResult[0];
            }
            catch (Exception ex)
            {


                throw ex;
            }

           
        }

        public async void UptadePays(Pays pays)
        {

            try
            {
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();
                dbcontext.Update(pays);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }

        public async void DeletePays(int id)
        {

            try
            {
                
                ProjetSGDBContext dbcontext = new ProjetSGDBContext();              
                Pays pays = dbcontext.Pays.Find(id);
                dbcontext.Remove(pays);
                var oResponse = await dbcontext.SaveChangesAsync();


            }
            catch (Exception ex)
            {


                throw ex;
            }


        }
    }
}

