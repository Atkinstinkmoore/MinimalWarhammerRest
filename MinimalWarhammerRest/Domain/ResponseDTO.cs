using MinimalWarhammerRest.Services.TimeService;

namespace MinimalWarhammerRest.Domain;

public sealed record ResponseDTO<T>
{
    public T Data { get; init; }
    public DateTimeOffset TimeStamp { get; init; }

    internal ResponseDTO(T data, DateTimeOffset timeStamp)
    {
        Data = data;
        TimeStamp = timeStamp;
    }
}

public sealed record EmptyResponseDTO
{
    public DateTimeOffset TimeStamp { get; init; }

    internal EmptyResponseDTO(DateTimeOffset timeStamp)
    {
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
        return new EmptyResponseDTO(_timeService.GetCurrentTime());
    }

    public ResponseDTO<object> Create(object data)
    {
        return new ResponseDTO<object>(data, _timeService.GetCurrentTime());
    }
}

public interface IResponseFactory
{
    EmptyResponseDTO Create();

    ResponseDTO<object> Create(object data);
}