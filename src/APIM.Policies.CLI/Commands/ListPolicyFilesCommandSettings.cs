﻿using APIM.Policies.CLI.Analyzers;
using System.ComponentModel;

namespace APIM.Policies.CLI.Commands;

internal class ListPolicyFilesCommandSettings : CommandSettings
{
    [Description("Path to the folder containing the policy files")]
    [CommandArgument(0, "[FOLDER]")]
    public string? Folder { get; init; }

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
        if (string.IsNullOrWhiteSpace(Folder))
        {
            return ValidationResult.Error("Specify a folder");
        }
        if (!Directory.Exists(Folder))
        {
            return ValidationResult.Error($"Unable to find folder {Folder}");
        }

        if (string.IsNullOrWhiteSpace(Include))
        {
            return ValidationResult.Error("Include should have a value");
        }

        return ValidationResult.Success();
    }
}
