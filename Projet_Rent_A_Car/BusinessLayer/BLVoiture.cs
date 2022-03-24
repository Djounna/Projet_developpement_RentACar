using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLVoiture // OK Corentin, à valider
    {
        DalCommun dal = new();
        DALVoiture dalVoiture = new();
        public List<Voiture> SelectAllVoiture()
        {
            return dal.SelectAll<Voiture>();
        }
        public Voiture SelectVoitureByID(int id)
        {
            return dal.SelectById<Voiture>(id);
        }
        public void InsertOrUpdateVoiture(Voiture Voiture)
        {
                dal.InsertOrUpdate(Voiture);
        }
        public void DeleteVoiture(int id)
        {
            dal.Delete(SelectVoitureByID(id));
        }
        public List<Voiture> SelectAllVoitureInactif() 
        {
            return dalVoiture.SelectAllVoitureInactif();
        }
        public List<Voiture> SelectAllVoitureActif()
        {
            return dalVoiture.SelectAllVoitureActif();
        }


    }
}
