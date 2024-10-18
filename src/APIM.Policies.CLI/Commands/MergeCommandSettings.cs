using System.ComponentModel;

namespace APIM.Policies.CLI.Commands;

internal class MergeCommandSettings : CommandSettings
{
    [Description("Path to the project containing the policy expressions")]
    [CommandArgument(0, "[PROJECT]")]
    public string? Project { get; init; }

    [Description("Path to the folder containing the policy files. If not specified the directory of the project is used.")]
    [CommandOption("-t|--target")]
    public string? Target { get; init; }

    [Description("Specifies the filter to select the policy files. Default value: *.cshtml")]
    [CommandOption("-i|--include")]
    [DefaultValue("*.cshtml")]
    public string? Include { get; init; }

    [Description("Specifies the extension of policy files to ignore. Default value: .generated.cshtml")]
    [CommandOption("-e|--exclude")]
    [DefaultValue(".generated.cshtml")]
    public string? Exclude { get; init; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Project))
        {
            return ValidationResult.Error("Specify a project");
        }
        if (!File.Exists(Project))
        {
            return ValidationResult.Error($"Unable to find project {Project}");
        }

        if (string.IsNullOrWhiteSpace(Include))
        {
            return ValidationResult.Error("Include should have a value");
        }

        return ValidationResult.Success();
    }
}
