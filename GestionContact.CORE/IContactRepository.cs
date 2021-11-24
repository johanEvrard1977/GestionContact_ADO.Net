using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.CORE
{
    public interface IContactRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int userId);
        Task<T> GetOne(int id, int userId);
        Task<T> Create(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> GetName(string Name, int userId);
        Task<bool> EmailExist(int id, string email);
    }
}
