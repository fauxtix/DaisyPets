using DaisyPets.Core.Application.ViewModels;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface IDocumentsService
    {
        Task<int> InsertDocument(DocumentoDto newDocument);
        Task<bool> UpdateDocument(int Id, DocumentoDto updateDocument);
        Task DeleteDocument(int id);
        Task<IEnumerable<DocumentoDto>> GetAll();
        Task<DocumentoDto> GetDocument_ById(int id);
        Task<IEnumerable<DocumentoVM>> GetAllVM(int Id);

    }
}
