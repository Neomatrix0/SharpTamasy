using System.ComponentModel.DataAnnotations;

namespace SharpTamasy.Api.Contracts;

public sealed class CreateTaskRequest
{
    [Required]
    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string AssignedTo { get; init; } = string.Empty;
}
