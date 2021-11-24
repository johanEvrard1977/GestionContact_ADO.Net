using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactApi.ViewModels
{
    public class ViewLoginApi
    {
        [Required(ErrorMessage = "le champs << {0} >> est obligatoire")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "vous devez spécifier un mot de passe compris entre 3 et 30 caractères")]
        [DisplayName(displayName: "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public int Id { get; set; }
    }
}
