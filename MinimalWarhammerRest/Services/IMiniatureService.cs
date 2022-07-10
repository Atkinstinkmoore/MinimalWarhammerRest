using LanguageExt.Common;
using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.DTOs;
using MinimalWarhammerRest.Domain.Requests;

namespace MinimalWarhammerRest.Services
{
    public interface IMiniatureService
    {
        Task<Result<MiniatureDTO>> Get(int id);
        Task<IEnumerable<MiniatureDTO>> GetAll();
        Task<Result<bool>> Create(CreateMiniatureRequest req);
    }
}