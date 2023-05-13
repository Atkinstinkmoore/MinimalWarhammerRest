using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Miniatures;

namespace MinimalWarhammerRest.Endpoints;

public static class MiniatureEndpoint
{
    public static void MapMiniatureEndpoints(this WebApplication app)
    {
        app.MapGet("api/mini/{id:int}", GetMiniature).Produces(200, typeof(ResponseDTO<MiniatureDTO>)).Produces(404, typeof(ResponseDTO<string>));
        app.MapPost("api/mini", CreateMiniature).Produces(201).Produces(400, typeof(ResponseDTO<string>));
        app.MapGet("api/all-minis", GetAllMiniatures).Produces(200, typeof(ResponseDTO<IEnumerable<MiniatureDTO>>));
    }

    public static async Task<IResult> GetMiniature(IMiniatureService service, IResponseFactory factory, int id)
    {
        var result = await service.Get(id);

        return result.Match(r =>
        {
            return Results.Ok(factory.Create(r));
        }, ex =>
        {
            if (ex is MiniatureNotFoundException)
                return Results.NotFound(factory.Create(ex.Message));

            return Results.BadRequest(factory.Create());
        });
    }

    public static async Task<IResult> GetAllMiniatures(IMiniatureService service, IResponseFactory factory)
    {
        var result = await service.GetAll();

        return Results.Ok(factory.Create(result));
    }

    public static async Task<IResult> CreateMiniature(IMiniatureService service, IResponseFactory factory, CreateMiniatureRequest req)
    {
        var result = await service.Create(req);

        return result.Match(r =>
        {
            return Results.Created("api/mini/" + r.Id, factory.Create(r));
        }, ex =>
        {
            if (ex is CouldNotCreateException)
                return Results.BadRequest(factory.Create(ex.Message));

            return Results.BadRequest();
        });
    }
}