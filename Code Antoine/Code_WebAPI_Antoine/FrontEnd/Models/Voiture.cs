using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Voiture
    {
        public Voiture()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Idvoiture { get; set; }
        public int Idnotoriete { get; set; }
        public int? Iddepot { get; set; }
        public string Immatriculation { get; set; } = null!;
        public string? Marque { get; set; }
        public bool? Inactif { get; set; }

        public virtual Notoriete IdnotorieteNavigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
