using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactASP.ViewModels
{
    public class Login
    {
        private string _mail { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
            }
        }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        private string _password { get; set; }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public int Id { get; set; }
        public string LastName { get; set; }
    }
}