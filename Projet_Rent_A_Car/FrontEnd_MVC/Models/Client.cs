using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Client
    {
        public Client()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Idclient { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        [MaxLength(50, ErrorMessage = "this field shouldn'thave more than 50 character")]
        public string Nom { get; set; } = null!;
        [Required(ErrorMessage = "This field is mandatory")]
        [MaxLength(50, ErrorMessage = "this field shouldn'thave more than 50 character")]
        public string Prenom { get; set; } = null!;
        [Required(ErrorMessage = "This field is mandatory")]
        [MaxLength(50, ErrorMessage = "this field shouldn'thave more than 50 character")]
        [EmailAddress(ErrorMessage ="This field should contain an e-mail address")]
        [Display(Name = "E-mail Address")]
        public string Mail { get; set; } = null!;

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
