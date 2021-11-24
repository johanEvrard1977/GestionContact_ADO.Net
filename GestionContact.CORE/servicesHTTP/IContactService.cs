using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.CORE.servicesHTTP
{
    public interface IContactService<T>
    {
        IEnumerable<T> GetAll(int userId);
        T GetOne(int id, int userId);
        bool Create(T entity);
        bool Update(int id, T entity);
        bool Delete(int id);
        IEnumerable<T> GetAllByName(int userId, string Name);
    }
}
