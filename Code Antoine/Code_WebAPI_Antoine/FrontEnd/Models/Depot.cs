using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Depot
    {
        public Depot()
        {
            ForfaitIddepot1Navigations = new HashSet<Forfait>();
            ForfaitIddepot2Navigations = new HashSet<Forfait>();
            ReservationIddepotDepartNavigations = new HashSet<Reservation>();
            ReservationIddepotRetourNavigations = new HashSet<Reservation>();
        }

        public int Iddepot { get; set; }
        public int Idville { get; set; }
        public bool? Inactif { get; set; }

        public virtual Ville IdvilleNavigation { get; set; } = null!;
        public virtual ICollection<Forfait> ForfaitIddepot1Navigations { get; set; }
        public virtual ICollection<Forfait> ForfaitIddepot2Navigations { get; set; }
        public virtual ICollection<Reservation> ReservationIddepotDepartNavigations { get; set; }
        public virtual ICollection<Reservation> ReservationIddepotRetourNavigations { get; set; }
    }
}
