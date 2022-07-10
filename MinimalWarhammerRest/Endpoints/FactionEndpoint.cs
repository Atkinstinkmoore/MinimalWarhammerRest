using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.DTOs;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Domain.Requests;
using MinimalWarhammerRest.Services;
using System.Diagnostics;

namespace MinimalWarhammerRest.Endpoints
{
    public static class FactionEndpoint
    {
        public static void MapFactionEndpoints(this WebApplication app)
        {
            app.MapGet("getfaction/{id}", GetFactionById).Produces(200, typeof(ResponseDTO<FactionDTO>)).Produces(404);
            app.MapGet("getallfactions", GetAllFactions).Produces(200, typeof(ResponseDTO<IEnumerable<FactionDTO>>));
            app.MapPost("newfaction", CreateFaction).Produces(204).Produces(400, typeof(EmptyResponseDTO));
            app.MapDelete("removefaction/{id}", DeleteFaction).Produces(200, typeof(EmptyResponseDTO)).Produces(400, typeof(EmptyResponseDTO));
        }


        internal static async Task<IResult> GetFactionById(IFactionService service, int id)
        {
            var result = await service.Get(id).ConfigureAwait(false);

            return result.Match(r =>
            {
                return Results.Ok(new ResponseDTO<FactionDTO>(r));
            }, ex =>
            {
                if (ex is FactionNotFoundException)
                    return Results.NotFound(new ResponseDTO<string>(ex.Message));

                return Results.StatusCode(500);
            });
        }

        internal static async Task<IResult> CreateFaction(IFactionService service, CreateFactionRequest req)
        {
            var result = await service.Create(req.Name).ConfigureAwait(false);

            if (result.IsSuccess)
                return Results.Ok(new EmptyResponseDTO());

            return Results.BadRequest(new EmptyResponseDTO());
        }       

        internal static async Task<IResult> GetAllFactions(IFactionService service)
        {
            var result = await service.GetAll();

            return Results.Ok(new ResponseDTO<IEnumerable<FactionDTO>>(result));
        }
        internal static async Task<IResult> DeleteFaction(IFactionService service, int id)
        {
            var result = await service.Delete(id).ConfigureAwait(false);
            return result.IsSuccess ? Results.Ok(new EmptyResponseDTO()) : Results.BadRequest(new EmptyResponseDTO());
        }
    }
}
