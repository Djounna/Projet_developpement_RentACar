using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLForfait
    {
        private DALForfait dalForfait = new();
        private DalCommun dal = new();
        public List<Forfait> SelectAllForfait()
        {
            return dalForfait.SelectAllForfait();
        }
        public Forfait SelectForfaitByID(int id)
        {
            return dalForfait.SelectById(id);
        }
        public void InsertOrUpdateForfait(Forfait forfait)
        {

            dalForfait.InsertOrUpdate(forfait);
        }

    }
}
