using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLCalculPrix
    {

        public decimal PrixTotal(Reservation reservation)
        {
            if (reservation.Idforfait == null)
            {
                return PrixTotal(reservation);
            }
            else
            {
                return PrixTotalForfait(reservation);
            }

        }


        private decimal PrixTotalAuKm(Reservation reservation)
        {
            return 10;
        }

        private decimal PrixTotalForfait(Reservation reservation)
        {
            return 10;
        }

        private int KilometreParcourus(int KilometrageDepart, int KilometrageRetour)
        {
            return KilometrageRetour - KilometrageDepart;
        }






    }

}
