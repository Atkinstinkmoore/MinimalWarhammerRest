using LanguageExt.Common;
using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.DTOs;

namespace MinimalWarhammerRest.Services
{
    public interface IFactionService
    {
        Task<Result<bool>> Create(string name);
        Task<Result<FactionDTO>> Get(int id);
        Task<IEnumerable<FactionDTO>> GetAll();
        Task<Result<bool>> Delete(int id);
    }
}