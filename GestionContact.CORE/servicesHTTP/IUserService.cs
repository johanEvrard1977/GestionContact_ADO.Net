using GestionContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.CORE.servicesHTTP
{
    public interface IUserService<T>
    {
        IEnumerable<T> GetAll();
        T GetOne(int id);
        bool Create(T entity);
        bool Update(int id, T entity);
        bool Delete(int id);
        LoginDto Login(string Email, string password);
    }
}
