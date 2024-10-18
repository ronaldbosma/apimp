using System.ComponentModel;

namespace APIM.Policies.CLI.Commands;

internal class ListPolicyExpressionsCommandSettings : CommandSettings
{
    [Description("Path to the project containing the policy expressions")]
    [CommandArgument(0, "[SOURCE]")]
    public string? Source { get; init; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Source))
        {
            return ValidationResult.Error("Specify a source");
        }
        if (!File.Exists(Source))
        {
            return ValidationResult.Error($"Unable to find source {Source}");
        }

        return ValidationResult.Success();
    }
}
