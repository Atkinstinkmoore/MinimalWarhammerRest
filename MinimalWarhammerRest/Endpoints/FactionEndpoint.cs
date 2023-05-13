using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Factions;

namespace MinimalWarhammerRest.Endpoints;

public static class FactionEndpoint
{
    public static void MapFactionEndpoints(this WebApplication app)
    {
        app.MapGet("api/faction/{id:int}", GetFactionById).Produces(200, typeof(ResponseDTO<FactionDetailsDTO>)).Produces(404);
        app.MapPost("api/faction", CreateFaction).Produces(204).Produces(400, typeof(EmptyResponseDTO));
        app.MapDelete("api/faction/{id:int}", DeleteFaction).Produces(200, typeof(EmptyResponseDTO)).Produces(400, typeof(EmptyResponseDTO));
        app.MapGet("api/all-factions", GetAllFactions).Produces(200, typeof(ResponseDTO<IEnumerable<FactionDTO>>));
    }

    internal static async Task<IResult> GetFactionById(IFactionService service, IResponseFactory factory, int id)
    {
        var result = await service.Get(id);

        return result.Match(r =>
        {
            return Results.Ok(factory.Create(r));
        }, ex =>
        {
            if (ex is FactionNotFoundException)
                return Results.NotFound(factory.Create(ex.Message));

            return Results.StatusCode(500);
        });
    }

    internal static async Task<IResult> CreateFaction(IFactionService service, IResponseFactory factory, CreateFactionRequest req)
    {
        var result = await service.Create(req.Name);

        if (result.IsSuccess)
            return Results.Ok(factory.Create());

        return Results.BadRequest(factory.Create());
    }

    internal static async Task<IResult> GetAllFactions(IFactionService service, IResponseFactory factory)
    {
        var result = await service.GetAll();

        return Results.Ok(factory.Create(result));
    }

    internal static async Task<IResult> DeleteFaction(IFactionService service, IResponseFactory factory, int id)
    {
        var result = await service.Delete(id);
        return result.IsSuccess ? Results.Ok(factory.Create()) : Results.BadRequest(factory.Create());
    }
}