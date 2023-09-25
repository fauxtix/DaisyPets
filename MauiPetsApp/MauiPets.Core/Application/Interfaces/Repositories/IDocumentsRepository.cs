using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
{
    public interface IDocumentsRepository
    {
        Task<int> InsertDocument(Documento newDocument);
        Task<bool> UpdateDocument(Documento updateDocument);
        Task DeleteDocument(int id);
        Task<IEnumerable<Documento>> GetAll();
        Task<Documento> GetDocument_ById(int id);
        Task<IEnumerable<DocumentoVM>> GetAllVM(int Id);
    }
}
