using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLogin.Models.Repository
{
    interface IRepository<T>  : IDisposable
    {
        void Create(T item);
        void Edit(T item);
        void Remove(T item);
        Task<T> GetOne(int? id);
        Task<List<T>> GetAll();
    }
}
