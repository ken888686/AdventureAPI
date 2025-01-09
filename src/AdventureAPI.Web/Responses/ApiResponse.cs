namespace AdventureAPI.Web.Responses;

public class ApiResponse<T>(T data, int statusCode, string message = "")
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public T Data { get; set; } = data;
}
