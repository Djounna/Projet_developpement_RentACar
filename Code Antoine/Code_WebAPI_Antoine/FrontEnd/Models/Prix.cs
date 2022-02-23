using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Prix
    {
        public int ReferencePrix { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public decimal PrixKm { get; set; }
    }
}
