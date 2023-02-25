using LanguageExt.Common;
using Microsoft.Extensions.Caching.Memory;
using MinimalWarhammerRest.Domain.DTOs;

namespace MinimalWarhammerRest.Services;

public sealed class CachedFactionService : IFactionService
{
    private readonly IFactionService _factionService;
    private readonly IMemoryCache _cache;

    public CachedFactionService(IFactionService factionService, IMemoryCache cache)
    {
        _factionService = factionService;
        _cache = cache;
    }

    public Task<Result<bool>> Create(string name)
    {
        _cache.Remove("faction_getall");
        return _factionService.Create(name);
    }

    public Task<Result<bool>> Delete(int id)
    {
        _cache.Remove("faction_get_" + id.ToString());
        _cache.Remove("faction_getall");
        return _factionService.Delete(id);
    }

    public async Task<Result<FactionDetailsDTO>> Get(int id)
    {
        var isCached = _cache.TryGetValue("faction_get_" + id.ToString(), out FactionDetailsDTO value);
        if (isCached)
            return new Result<FactionDetailsDTO>(value);

        var result =  await _factionService.Get(id);
        return result.Match(r =>
        {
            var policy = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set("faction_get_" + id.ToString(), r, policy);
            return result;
        }, ex =>
        {
            return result;
        });
    }

    public async Task<IEnumerable<FactionDTO>> GetAll()
    {
        var cachedItem = _cache.TryGetValue("faction_getall", out IEnumerable<FactionDTO> value);
        if (cachedItem)
            return value;

        var result = await _factionService.GetAll();
        if (result.Any())
        {
            var policy = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set("faction_getall", result.ToList(), policy);
        }
        return result;
    }
}