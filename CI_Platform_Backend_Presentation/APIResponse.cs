using System.Net;

namespace CI_Platform_Backend_Presentation;

public class APIResponse
{
    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccess { get; set;} = false;

    public object? Result { get; set; }

    public string? Token { get; set; }

    public int Page {  get; set; }

    public int PageSize { get; set; }

    public int RowCount { get; set; }

    public string[]? ErrorMessages { get; set; }
}
