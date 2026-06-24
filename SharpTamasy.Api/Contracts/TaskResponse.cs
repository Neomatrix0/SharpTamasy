namespace SharpTamasy.Api.Contracts;

public sealed record TaskResponse(
    int Id,
    string Title,
    string Description,
    string Type,
    string Status,
    string AssignedTo,
    string? Severity,
    string? Priority);
