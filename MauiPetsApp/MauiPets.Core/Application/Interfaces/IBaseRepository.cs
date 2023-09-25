using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPetsApp.Core.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
       Task< long> Insert(T entity);
       Task Update(T entity);
       Task Delete(T entity);
       Task UpdateById(int Id);
       Task DeleteById(int Id);
       Task< bool> EntradaExiste_BD(string campo, string str);
       Task< int >GetFirstId();
       Task< int >GetLastId();
       Task< T> Query_ById(int Id);
       Task< bool> RecInUse(int Id);
       Task< bool> TableHasData();
       Task< IEnumerable<T>> Query(string where = "");
    }
}
