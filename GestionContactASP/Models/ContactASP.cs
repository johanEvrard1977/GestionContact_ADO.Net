using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactASP.Models
{
    public class ContactASP
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_De_Naissance { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
