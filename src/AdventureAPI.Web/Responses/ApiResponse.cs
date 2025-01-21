namespace AdventureAPI.Web.Responses;

public class ApiResponse<T>(T data, int statusCode, IEnumerable<string>? messages = null)
{
    public int StatusCode { get; set; } = statusCode;
    public IEnumerable<string>? Messages { get; set; } = messages;
    public T Data { get; set; } = data;
}
