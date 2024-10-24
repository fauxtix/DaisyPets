﻿using AutoMapper;
using FluentValidation;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class RacaoService : IRacaoService
    {
        private readonly IRacaoRepository _repository;
        private readonly IValidator<RacaoDto> _validator;

        private readonly IMapper _mapper;
        public RacaoService(IRacaoRepository repository, IValidator<RacaoDto> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<RacaoDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<RacaoDto>(resp);
            return output;
        }

        public async Task<IEnumerable<RacaoDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<RacaoDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<RacaoVM>> GetAllRacoesVMAsync()
        {
            try
            {
                var racoesVM = await _repository.GetAllRacoesVMAsync();
                return racoesVM;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<RacaoVM>();

            }
        }

        public async Task<IEnumerable<RacaoVM>> GetRacaoVMAsync(int Id)
        {
            return await _repository.GetRacaoVMAsync(Id);
        }

        public async Task<int> InsertAsync(RacaoDto racao)
        {
            var racaoIdentity = _mapper.Map<Racao>(racao);
            var insertedId = await _repository.InsertAsync(racaoIdentity);
            return insertedId;
        }

        public async Task UpdateAsync(int Id, RacaoDto racao)
        {
            try
            {
                var racaoEntity = await _repository.FindByIdAsync(Id);
                if (racaoEntity == null)
                    throw new KeyNotFoundException("Ração não encontrada");

                var mappedModel = _mapper.Map(racao, racaoEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Erro no update ({ex.Message})");

            }
        }

        public string RegistoComErros(RacaoDto racao)
        {
            FluentValidation.Results.ValidationResult results = _validator.Validate(racao);

            if (!results.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in results.Errors)
                {
                    sb.AppendLine(failure.ErrorMessage);
                }
                return sb.ToString();
            }

            return "";
        }

    }
}
