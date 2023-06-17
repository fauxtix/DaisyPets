using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using Microsoft.Extensions.Logging;

namespace DaisyPets.Infrastructure.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IDocumentsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentsService> _logger;

        public DocumentsService(IDocumentsRepository repository, IMapper mapper, ILogger<DocumentsService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task DeleteDocument(int Id)
        {
            await _repository.DeleteDocument(Id);
        }

        public async Task<IEnumerable<DocumentoDto>> GetAll()
        {
            var resp = await _repository.GetAll();
            var output = _mapper.Map<IEnumerable<DocumentoDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<DocumentoVM>> GetAllVM()
        {
            var documentsVM = await _repository.GetAllVM();
            return documentsVM;
        }

        public async Task<DocumentoDto> GetDocument_ById(int Id)
        {
            var resp = await _repository.GetDocument_ById(Id);
            var output = _mapper.Map<DocumentoDto>(resp);
            return output;
        }

        public async Task<int> InsertDocument(DocumentoDto newDocument)
        {
            var documentIdentity = _mapper.Map<Documento>(newDocument);
            var insertedId = await _repository.InsertDocument(documentIdentity);
            return insertedId;
        }

        public async Task<bool> UpdateDocument(int Id, DocumentoDto updateDocument)
        {
            try
            {
                var documentEntity = await _repository.GetDocument_ById(Id);
                if (documentEntity == null)
                    throw new KeyNotFoundException("Document not found");

                var mappedModel = _mapper.Map(updateDocument, documentEntity);

                await _repository.UpdateDocument(mappedModel);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar documento ({ex.Message})");
                return false;
            }
        }
    }
}
