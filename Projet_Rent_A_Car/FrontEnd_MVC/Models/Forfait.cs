using System;
using System.Collections.Generic;

namespace FrontEnd_MVC.Models
{
    public partial class Forfait
    {
        public int Idforfait { get; set; }
        public int Iddepot1 { get; set; }
        public int Iddepot2 { get; set; }
        public decimal Prix { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        public virtual Depot? Iddepot1Navigation { get; set; }
        public virtual Depot? Iddepot2Navigation { get; set; }
    }
}
