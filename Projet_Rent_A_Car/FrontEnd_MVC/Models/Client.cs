using System;
using System.Collections.Generic;

namespace FrontEnd_MVC.Models
{
    public partial class Client
    {
        public Client()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Idclient { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
