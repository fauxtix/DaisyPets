﻿using DaisyPets.Core.Application.ViewModels;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface IVacinasService
    {
        Task DeleteAsync(int Id);
        Task<VacinaVM> FindByIdAsync(int Id);
        Task<IEnumerable<VacinaDto>> GetAllAsync();
        Task<IEnumerable<VacinaVM>> GetAllVacinaVMAsync();
        Task<VacinaVM> GetVacinaVMAsync(int Id);
        Task<int> InsertAsync(VacinaDto Vacina);
        string RegistoComErros(VacinaVM Vacinao);
        Task UpdateAsync(int Id, VacinaDto Vacina);
        Task<IEnumerable<VacinaVM>> GetPetVaccinesVMAsync(int petId);
    }
}