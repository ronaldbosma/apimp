using System.ComponentModel;

namespace APIM.Policies.CLI.Commands;

internal class ListPolicyFilesCommandSettings : CommandSettings
{
    [Description("Path to the folder containing the policy files")]
    [CommandArgument(0, "[FOLDER]")]
    public string? Folder { get; init; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Folder))
        {
            return ValidationResult.Error("Specify a folder");
        }
        if (!Directory.Exists(Folder))
        {
            return ValidationResult.Error($"Unable to find folder {Folder}");
        }

        return ValidationResult.Success();
    }
}
