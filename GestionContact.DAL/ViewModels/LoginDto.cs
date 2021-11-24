using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.DAL
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
