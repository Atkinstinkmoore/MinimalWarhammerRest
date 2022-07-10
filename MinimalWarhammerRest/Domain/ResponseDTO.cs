using System.Diagnostics;

namespace MinimalWarhammerRest.Domain
{
    public struct ResponseDTO<T>
    {
        public T Data { get; init; }
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
        public string requestId { get; set; }

        public ResponseDTO(T data)
        {
            Data = data;
            requestId = Activity.Current.TraceId.ToString();
        }
    }

    public struct EmptyResponseDTO
    {
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
        public string requestId { get; init; }
        public EmptyResponseDTO()
        {
            requestId = Activity.Current.TraceId.ToString();
        }

    }
}
