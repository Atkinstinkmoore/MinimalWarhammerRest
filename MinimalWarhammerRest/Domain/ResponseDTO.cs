using System.Diagnostics;

namespace MinimalWarhammerRest.Domain;

public struct ResponseDTO<T>
{
    public T Data { get; init; }
    public DateTime TimeStamp { get; } = DateTime.UtcNow;
    public string RequestId { get; set; }

    public ResponseDTO(T data)
    {
        Data = data;
        RequestId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
    }
}

public struct EmptyResponseDTO
{
    public DateTime TimeStamp { get; } = DateTime.UtcNow;
    public string RequestId { get; init; }
    public EmptyResponseDTO()
    {
        RequestId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
    }

}
