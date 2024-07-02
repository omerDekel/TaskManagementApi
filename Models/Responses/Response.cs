
namespace DTOs.Responses
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public object? Entity { get; set; }
        public ErrorMessage? Error { get; set; }
    }
}
