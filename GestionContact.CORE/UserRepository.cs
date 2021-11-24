using GestionContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connections.Database;

namespace GestionContact.CORE
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly Connection _connection;
        public UserRepository(Connection connection)
        {
            _connection = connection;
        }
        public async Task<User> Create(User entity)
        {
            try
            {
                Command command = new Command("SP_Add_User", true);
                command.AddParameter("@FirstName", entity.FirstName);
                command.AddParameter("@LastName", entity.LastName);
                command.AddParameter("@mail", entity.Email);
                command.AddParameter("@pass", entity.Password); // Execute NonQuery renvoit le nbr de lignes affectées
                var state = await Task.Run(() => _connection.ExecuteNonQuery(command));
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public async Task Delete(int id)
        {
            Command command = new Command("delete from [User] where Id = @p1", false);
            command.AddParameter("p1", id);

            await Task.Run(() => _connection.ExecuteNonQuery(command)); // Execute NonQuery renvoit le nbr de lignes affectées
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            Command command = new Command("select Id, FirstName, LastName, Email from [User]");
            return await Task.Run(() => _connection.ExecuteReader(command, (dr) => new User()
            {
                Id = (int)dr["Id"],
                FirstName = (string)dr["FirstName"],
                LastName = (string)dr["LastName"],
                Email = (string)dr["Email"]
            }));
        }

        public async Task<User> GetOne(int id)
        {
            Command command = new Command("select Id, FirstName, LastName, Email from [User] where id = @p1;", false);
            command.AddParameter("p1", id);

            IEnumerable<User> cat = await Task.Run(() => _connection.ExecuteReader(command, (dr) => new User()
            {
                Id = (int)dr["Id"],
                FirstName = (string)dr["FirstName"],
                LastName = (string)dr["LastName"],
                Email = (string)dr["Email"]
            }));
            User c = new User();
            foreach (var item in cat)
            {
                c.Id = item.Id;
                c.FirstName = item.FirstName;
                c.LastName = item.LastName;
            }
            return c;
        }

        public async Task Update(int id, User entity)
        {
            Command command = new Command("update [User] set FirstName = @p2, LastName = @p3, Email = @p4, Password = @p5 where Id = @p1", false);
            command.AddParameter("p1", id);
            command.AddParameter("p2", entity.FirstName);
            command.AddParameter("p3", entity.LastName);
            command.AddParameter("p4", entity.Email);
            command.AddParameter("p5", entity.Password);

            await Task.Run(() => _connection.ExecuteNonQuery(command)); // Execute NonQuery renvoit le nbr de lignes affectées
        }

        public async Task<LoginSuccessDto> Login(LoginDto login)
        {

            try
            {
                Command command = new Command("SP_LoginUser", true);
                command.AddParameter("@email", login.Email);
                command.AddParameter("@Passwd", login.Password);
                IEnumerable<LoginSuccessDto> user = await Task.Run(() => _connection.ExecuteReader(command, (dr) => new LoginSuccessDto()
                {
                    Id = (int)dr["Id"],
                    Email = (string)dr["Email"]
                }));

                LoginSuccessDto c = new LoginSuccessDto();
                foreach (var item in user)
                {
                    c.Id = item.Id;
                    c.Email = item.Email;
                }
                return c;
            }
            catch
            {
                throw;
            }
        }
        public async Task<User> Check(LoginSuccessDto user)
        {
            IEnumerable<User> log = new List<User>();
            log = await GetAll();
            return log.SingleOrDefault(u => u.Email == user.Email && u.Id == user.Id);
        }
    }
}
