namespace ApiTemplate.Result;


public class ApiResult
{
    public bool State { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }

    public static ApiResult Success(object? data = null) =>
        new() { State = true, Data = data };


    public static ApiResult Fail(string message) =>
        new() { State = false, Message = message };

}
