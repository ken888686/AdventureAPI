namespace AdventureAPI.Web.Responses;

public class ApiResponse<T>(T data, string message = "", bool success = true)
{
    public string Message { get; set; } = message;
    public bool Success { get; set; } = success;
    public T Data { get; set; } = data;
}
