using GestionContact.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Connections.Database;

namespace GestionContact.CORE
{
    public class ContactRepository : IContactRepository<Contact>
    {
        private readonly Connection _connection;

        public ContactRepository(Connection connection)
        {
            _connection = connection;
        }

        public async Task<Contact> Create(Contact entity)
        {
            try
            {
                Command command = new Command("INSERT INTO Contact Values (@nom, @Prenom, @mail, @Tel, @Date_De_Naissance, @UserId)", false);
                command.AddParameter("@Nom", entity.Nom);
                command.AddParameter("@Prenom", entity.Prenom);
                command.AddParameter("@mail", entity.Email);
                command.AddParameter("@Tel", entity.Telephone);
                command.AddParameter("@Date_De_Naissance", entity.Date_De_Naissance);
                command.AddParameter("@UserId", entity.UserId);
                var state = await Task.Run(() => _connection.ExecuteNonQuery(command));
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            Command command = new Command("delete from Contact where Id = @Id", false);
            command.AddParameter("Id", id);

            await Task.Run(() => _connection.ExecuteNonQuery(command)); // Execute NonQuery renvoit le nbr de lignes affectées
        }

        public async Task<IEnumerable<Contact>> GetAll(int userId)
        {
            Command command = new Command("select Id, Nom, Prenom, Email, Tel, Date_de_naissance from Contact" +
                    " where Contact.userId = @p1");
                command.AddParameter("p1", userId);
                return await Task.Run(() => _connection.ExecuteReader(command, (dr) => new Contact()
                {
                    Id = (int)dr["Id"],
                    Nom = (string)dr["Nom"],
                    Prenom = (string)dr["Prenom"],
                    Email = (string)dr["Email"],
                    Telephone = (string)dr["Tel"],
                    Date_De_Naissance = (DateTime)dr["Date_de_naissance"]
                }));
        }

        public async Task<IEnumerable<Contact>> GetName(string Name, int userId)
        {
            Command command = new Command("select Id, Nom, Prenom, Email, Tel, Date_De_Naissance from Contact" +
                " where Nom like @Name and UserId = @UserId");
            command.AddParameter("Name", Name+'%');
            command.AddParameter("UserId", userId);
            return await Task.Run(() => _connection.ExecuteReader(command, (dr) => new Contact()
            {
                Id = (int)dr["Id"],
                Nom = (string)dr["Nom"],
                Prenom = (string)dr["Prenom"],
                Email = (string)dr["Email"],
                Telephone = (string)dr["Tel"],
                Date_De_Naissance = (DateTime)dr["Date_de_naissance"]
            }));
        }

        public async Task<Contact> GetOne(int id, int userId)
        {
            Command command = new("select Id, Nom, Prenom, Email, Tel, Date_De_Naissance, UserId from Contact where id = @p1" +
                " and userId = @p2;", false);
            command.AddParameter("p1", id);
            command.AddParameter("p2", userId);

            IEnumerable<Contact> cat = await Task.Run(() => _connection.ExecuteReader(command, (dr) => new Contact()
            {
                Id = (int)dr["Id"],
                Nom = (string)dr["Nom"],
                Prenom = (string)dr["Prenom"],
                Email = (string)dr["Email"],
                Telephone = (string)dr["Tel"],
                Date_De_Naissance = (DateTime)dr["Date_de_naissance"],
                UserId = (int)dr["UserId"]
            }));
            Contact c = new Contact();
            foreach (var item in cat)
            {
                c.Id = item.Id;
                c.Nom = item.Nom;
                c.Prenom = item.Prenom;
                c.Date_De_Naissance = item.Date_De_Naissance;
                c.Email = item.Email;
                c.Telephone = item.Telephone;
                c.UserId = item.UserId;
            }
            return c;
        }

        public async Task Update(int id, Contact entity)
        {
            Command command = new Command("update Contact set Nom = @p2, Prenom = @p3," +
                    " Email = @p4, Date_De_Naissance = @p5, Tel = @p6, UserId = @p7 where Id = @p1", false);
                command.AddParameter("p1", id);
                command.AddParameter("p2", entity.Nom);
                command.AddParameter("p3", entity.Prenom);
                command.AddParameter("p4", entity.Email);
                command.AddParameter("p5", entity.Date_De_Naissance);
                command.AddParameter("p6", entity.Telephone);
                command.AddParameter("p7", entity.UserId);

                await Task.Run(() => _connection.ExecuteNonQuery(command)); // Execute NonQuery renvoit le nbr de lignes affectées
        }

        public async Task<bool> EmailExist(int id, string email)
        {
            Command command = new Command("select * from Contact where Email like @p2 and id <> @p1", false);
            command.AddParameter("p1",id);
            command.AddParameter("p2", email);
            int? result = await Task.Run(() => _connection.ExecuteScalar(command)) as int?;
            return result != null;
        }
    }
}
