using LanguageExt.Common;
using Microsoft.Extensions.Caching.Memory;
using MinimalWarhammerRest.Domain.DTOs;

namespace MinimalWarhammerRest.Services;

public class CachedFactionService : IFactionService
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
        return _factionService.Create(name);
    }

    public Task<Result<bool>> Delete(int id)
    {
        return _factionService.Delete(id);
    }

    public async Task<Result<FactionDTO>> Get(int id)
    {
        var isCached = _cache.TryGetValue("faction" + id.ToString(), out FactionDTO value);
        if(isCached)
            return new Result<FactionDTO>(value);

        var result = await _factionService.Get(id);
        return result.Match(r =>
        {
            _cache.Set("faction" + id.ToString(), r, TimeSpan.FromMinutes(5));
            return result;
        }, ex => {
            return result;
        });
        
    }

    public async Task<IEnumerable<FactionDTO>> GetAll()
    {

        var cachedItem = _cache.TryGetValue("all-factions", out IEnumerable<FactionDTO> value);
        if (cachedItem)
            return value;

        var result = await _factionService.GetAll();
        if (result.Any())
        {
            _cache.Set("all-factions", result.ToList(), TimeSpan.FromMinutes(5));
        }
        return result;
    }
}
