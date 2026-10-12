namespace EmployeeDirectoryApi.Options;

public sealed class AuthSettings
{
    public const string SectionName = "Auth";

    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Scope { get; set; } = "";
}