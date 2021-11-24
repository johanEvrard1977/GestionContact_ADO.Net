using System;

namespace GestionContact.DAL
{
    public class Contact
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime Date_De_Naissance { get; set; }
        public int UserId { get; set; }
    }
}
