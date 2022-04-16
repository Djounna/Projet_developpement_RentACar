using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class BLCalculPrix
    {
        private DalCommun dal = new();

        public decimal PrixTotal(Reservation reservation)
        {
            int reduction = Reduction(reservation.DateDepart, reservation.DateReservation);
            decimal total;
            decimal penalite = (decimal)0.1; decimal ristourne = (decimal)0.05;

            if (reservation.Idforfait == null)
            {
                total = PrixTotalAuKm(reservation) - (PrixTotalAuKm(reservation) * reduction / 100);
            }
            else
            {
                if (reservation.Penalite == true)
                {
                    total = PrixTotalForfait(reservation) - ((PrixTotalForfait(reservation) * reduction) / 100) + (PrixTotalForfait(reservation) * penalite);
                }
                else
                {
                    total = PrixTotalForfait(reservation) - ((PrixTotalForfait(reservation) * reduction) / 100) - (PrixTotalForfait(reservation) * ristourne);
                }
            }
            return total;
        }


        private decimal PrixTotalAuKm(Reservation reservation)
        {
            reservation.IddepotDepartNavigation = dal.dbcontext.Depot.Where(d => d.Iddepot == reservation.IddepotDepart).SingleOrDefault();
            reservation.IddepotDepartNavigation.IdvilleNavigation = dal.dbcontext.Ville.Where(v => v.Idville == reservation.IddepotDepartNavigation.Idville).SingleOrDefault();
            reservation.IddepotDepartNavigation.IdvilleNavigation.IdpaysNavigation = dal.dbcontext.Pays.Where(p => p.Idpays == reservation.IddepotDepartNavigation.IdvilleNavigation.Idpays).SingleOrDefault();
            Prix prix = dal.dbcontext.Prix.Where(prix => prix.Idpays == reservation.IddepotDepartNavigation.IdvilleNavigation.IdpaysNavigation.Idpays && prix.DateFin == null).SingleOrDefault();

            decimal prixAuKm = prix.PrixKm;

            decimal coefficient = reservation.CoefficientMultiplicateur;

            int kilometres = (int)KilometreParcourus(reservation.KilometrageDepart, reservation.KilometrageRetour);

            decimal total = (kilometres * prixAuKm) * coefficient;

            return total;
        }


        private decimal PrixTotalForfait(Reservation reservation)
        {

            reservation.IdforfaitNavigation = dal.dbcontext.Forfait.Where(f => f.Idforfait == reservation.Idforfait).FirstOrDefault();

            decimal prixForfait = reservation.IdforfaitNavigation.Prix;

            decimal coefficient = reservation.CoefficientMultiplicateur;

            decimal total = prixForfait * coefficient;

            return total;
        }

        private int? KilometreParcourus(int? KilometrageDepart, int? KilometrageRetour)
        {
            return KilometrageRetour - KilometrageDepart;
        }

        private int Reduction(DateTime DateDepart, DateTime DateReservation)
        {
            var difindays = (DateDepart - DateReservation).Days; // une différence entre dates retourne un TimeSpan. la meth .Days transforme le timespan en (int)jours.

            var diftotal = difindays - 7;

            if (diftotal > 7 && diftotal <= 14)
            {
                return 5;
            }
            else
            {
                if (diftotal > 14 && diftotal <= 21)
                {
                    return 10;
                }
                else
                {
                    if (diftotal > 21 && diftotal <= 28)
                    {
                        return 15;
                    }
                    else
                    {
                        if (diftotal > 28)
                            return 20;
                        else
                        {
                            return 0;
                        }
                    }
                }
            }

        }
    }
}
