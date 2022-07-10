using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.DTOs;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Domain.Requests;
using MinimalWarhammerRest.Services;
using System.Diagnostics;

namespace MinimalWarhammerRest.Endpoints
{
    public static class MiniatureEndpoint
    {
        public static void  MapMiniatureEndpoints(this WebApplication app)
        {
            app.MapGet("getmini/{id}", GetMiniature).Produces(200, typeof(ResponseDTO<MiniatureDTO>)).Produces(404, typeof(ResponseDTO<string>));
            app.MapGet("getallminis", GetAllMiniatures).Produces(200, typeof(ResponseDTO<IEnumerable<MiniatureDTO>>));
            app.MapPost("newmini", CreateMiniature).Produces(201, typeof(EmptyResponseDTO)).Produces(400, typeof(ResponseDTO<string>));
        }

        public static async Task<IResult> GetMiniature(IMiniatureService service, int id)
        {
            var result = await service.Get(id).ConfigureAwait(false);

            return result.Match(r =>
            {
                return Results.Ok(new ResponseDTO<MiniatureDTO>(r));
            }, ex =>
            {
                if(ex is MiniatureNotFoundException)
                    return Results.NotFound(new ResponseDTO<string>(ex.Message));

                return Results.StatusCode(500);
            });
        }

        public static async Task<IResult> GetAllMiniatures(IMiniatureService service)
        {
            var result = await service.GetAll().ConfigureAwait(false);

            return Results.Ok(new ResponseDTO<IEnumerable<MiniatureDTO>>(result));
        }

        public static async Task<IResult> CreateMiniature(IMiniatureService service, CreateMiniatureRequest req)
        {
            var result = await service.Create(req).ConfigureAwait(false);

            return result.Match(r =>
            {
                return Results.Ok(new EmptyResponseDTO());
            }, ex =>
            {
                if (ex is CouldNotCreateException)
                    return Results.BadRequest(new ResponseDTO<string>(ex.Message));

                return Results.BadRequest();
            });
        }
    }
}
