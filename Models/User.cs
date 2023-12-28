using System.Text.Json.Serialization;

namespace DotNetCRUDWebAPIs.Models;

public record User
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = null!;

    [JsonPropertyName("full_name")]
    public string FullName { get; set; } = null!;

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public long? Phone { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
