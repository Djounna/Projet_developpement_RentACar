using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Voiture
    {
        public Voiture()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Idvoiture { get; set; }
        public int Idnotoriete { get; set; }
        public int? Iddepot { get; set; }
        public string Immatriculation { get; set; } = null!;
        public string? Marque { get; set; }
        public bool? Inactif { get; set; }

        public virtual Notoriete IdnotorieteNavigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
