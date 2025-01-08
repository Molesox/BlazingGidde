using System;

namespace BlazingGidde.Shared.DTOs;


/// <summary>
/// The default aspnetcore mvc error validation response. It has the same as mvc ValidationProblemDetails class.
/// </summary>
public class ValidationErrorResponse
{
    public Dictionary<string, List<string>> Errors { get; set; } = new();
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
}