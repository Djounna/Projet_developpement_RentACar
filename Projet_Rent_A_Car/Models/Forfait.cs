namespace Models
{
    public partial class Forfait
    {
        public Forfait()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Idforfait { get; set; }
        public int Iddepot1 { get; set; }
        public int Iddepot2 { get; set; }
        public decimal Prix { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        public virtual Depot Iddepot1Navigation { get; set; } = null!;
        public virtual Depot Iddepot2Navigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
