namespace DaisyPets.Web.Blazor.BaseApiWrapperServices
{
    public interface IApiClientWrapperService<T> : IDisposable
    {
        Task<List<T>> GetAll();
        Task<T> Get(int? id);
        Task Create(T item);
        Task Update(int? id, T item);
        Task Delete(int id);
    }
}
