using LanguageExt.Common;
using Microsoft.Extensions.Caching.Memory;

namespace MinimalWarhammerRest.Miniatures;

public sealed class CachedMiniatureService : IMiniatureService
{
    private readonly IMiniatureService _service;
    private readonly IMemoryCache _cache;

    public CachedMiniatureService(IMiniatureService service, IMemoryCache cache)
    {
        _service = service;
        _cache = cache;
    }

    public Task<Result<MiniatureDTO>> Create(CreateMiniatureRequest req)
    {
        return _service.Create(req);
    }

    public async Task<Result<MiniatureDTO>> Get(int id)
    {
        var isCached = _cache.TryGetValue("miniature_get_" + id.ToString(), out MiniatureDTO value);
        if (isCached)
            return new Result<MiniatureDTO>(value);
        var result = await _service.Get(id);

        return result.Match(r =>
        {
            var policy = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set("miniature_get_" + id.ToString(), r, policy);
            return result;
        }, ex =>
        {
            return result;
        });
    }

    public Task<IEnumerable<MiniatureDTO>> GetAll()
    {
        return _service.GetAll();
    }
}