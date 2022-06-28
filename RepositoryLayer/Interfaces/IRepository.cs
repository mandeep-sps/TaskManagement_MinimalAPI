using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> InvokeQuery<T>(string query, object param, bool isStoreProcedure = false) where T : class;
        Task<IEnumerable<T>> InvokeQuery<T>(string query, bool isStoreProcedure = false) where T : class;
         T InvokeSingleQuery<T>(string query, object param, bool isStoreProcedure = false) where T : class;

        Task<bool> InvokeExecute(string query, object param, bool isStoreProcedure = false);
        Task<int> InvokeExecuteQuery(string query, object param, bool isStoreProcedure = false);


    }
}
