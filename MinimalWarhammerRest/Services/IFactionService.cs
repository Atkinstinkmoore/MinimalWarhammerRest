using MinimalWarhammerRest.Domain.DTOs;

namespace MinimalWarhammerRest.Services
{
    public interface IFactionService
    {
        Task<LanguageExt.Common.Result<bool>> Create(string name);
        Task<LanguageExt.Common.Result<FactionDTO>> Get(int id);
        Task<IEnumerable<FactionDTO>> GetAll();
        Task<LanguageExt.Common.Result<bool>> Delete(int id);
    }
}