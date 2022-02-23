using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Client
    {
        public Client()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Idclient { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
