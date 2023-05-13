using LanguageExt.Common;
using MinimalWarhammerRest.Domain;

namespace MinimalWarhammerRest.Miniatures
{
    public interface IMiniatureService
    {
        Task<Result<MiniatureDTO>> Get(int id);
        Task<IEnumerable<MiniatureDTO>> GetAll();
        Task<Result<MiniatureDTO>> Create(CreateMiniatureRequest req);
    }
}