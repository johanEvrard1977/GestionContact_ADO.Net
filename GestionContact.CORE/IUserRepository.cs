using GestionContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.CORE
{
    public interface IUserRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(int id);
        Task<T> Create(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
        Task<LoginSuccessDto> Login(LoginDto user);
        Task<User> Check(LoginSuccessDto user);
    }
}
