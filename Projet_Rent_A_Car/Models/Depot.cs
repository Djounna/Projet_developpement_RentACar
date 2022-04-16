namespace Models
{
    public partial class Depot
    {
        public Depot()
        {
            ForfaitIddepot1Navigation = new HashSet<Forfait>();
            ForfaitIddepot2Navigation = new HashSet<Forfait>();
            ReservationIddepotDepartNavigation = new HashSet<Reservation>();
            ReservationIddepotRetourNavigation = new HashSet<Reservation>();
            Voiture = new HashSet<Voiture>();
        }

        public int Iddepot { get; set; }
        public int Idville { get; set; }
        public bool? Inactif { get; set; }

        public virtual Ville IdvilleNavigation { get; set; } = null!;
        public virtual ICollection<Forfait> ForfaitIddepot1Navigation { get; set; }
        public virtual ICollection<Forfait> ForfaitIddepot2Navigation { get; set; }
        public virtual ICollection<Reservation> ReservationIddepotDepartNavigation { get; set; }
        public virtual ICollection<Reservation> ReservationIddepotRetourNavigation { get; set; }
        public virtual ICollection<Voiture> Voiture { get; set; }
    }
}
