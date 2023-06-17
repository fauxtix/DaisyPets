using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Repositories
{
    public interface IDocumentsRepository
    {
        Task<int> InsertDocument(Documento newDocument);
        Task<bool> UpdateDocument(Documento updateDocument);
        Task DeleteDocument(int id);
        Task<IEnumerable<Documento>> GetAll();
        Task<Documento> GetDocument_ById(int id);
        Task<IEnumerable<DocumentoVM>> GetAllVM();
    }
}
