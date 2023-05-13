using MinimalWarhammerRest.Services.TimeService;
using System.Diagnostics;

namespace MinimalWarhammerRest.Domain;

public sealed record ResponseDTO<T>
{
    public T Data { get; init; }
    public DateTimeOffset TimeStamp { get; init; }
    public string RequestId { get; init; }

    internal ResponseDTO(T data, DateTimeOffset timeStamp, string requestId)
    {
        Data = data;
        RequestId = requestId;
        TimeStamp = timeStamp;
    }
}

public sealed record EmptyResponseDTO
{
    public DateTimeOffset TimeStamp { get; init; }
    public string RequestId { get; init; }
    internal EmptyResponseDTO(DateTimeOffset timeStamp, string requestId)
    {
        RequestId = requestId;
        TimeStamp = timeStamp;
    }
}

public class ReponseFactory : IResponseFactory
{
    private readonly ITimeService _timeService;

    public ReponseFactory(ITimeService timeService)
    {
        _timeService = timeService;
    }
    public EmptyResponseDTO Create()
    {
        return new EmptyResponseDTO(DateTimeOffset.UtcNow, GetRequestId());
    }

    public ResponseDTO<object> Create(object data)
    {
        return new ResponseDTO<object>(data, _timeService.GetCurrentTime(), GetRequestId());
    }

    private static string GetRequestId()
    {
        return Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
    }
}

public interface IResponseFactory
{
    EmptyResponseDTO Create();
    ResponseDTO<object> Create(object data);
}