using System;
using System.Collections.Generic;

namespace FrontEnd_MVC.Models
{
    public partial class Prix
    {
        public int Idprix { get; set; }
        public int ReferencePrix { get; set; }
        public int Idpays { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public decimal PrixKm { get; set; }

        public virtual ICollection<Pays> IdpaysNavigation { get; set; }
    }
}
