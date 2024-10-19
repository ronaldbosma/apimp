using APIM.Policies.CLI.Models;

namespace APIM.Policies.CLI.Generators;

internal class PolicyFileGenerator
{
    internal static async Task MergePolicyExpressionsInPolicyFiles(
        IDictionary<string, PolicyExpression> policyExpressions, 
        IList<PolicyFile> policyFiles)
    {
        foreach (var policyFile in policyFiles)
        {
            var targetFilePath = GetGeneratedPolicyFileTargetPath(policyFile);
            var generatedContent = policyFile.MergePolicyExpressions(policyExpressions);

            await File.WriteAllTextAsync(targetFilePath, generatedContent);
        }
    }

    private static string GetGeneratedPolicyFileTargetPath(PolicyFile policyFile)
    {
        // Add .generated to the end of the input file name for the target file
        var targetFilePath = $"{Path.GetFileNameWithoutExtension(policyFile.Name)}.generated{Path.GetExtension(policyFile.Name)}";

        // Add the directory of the input file to the target file path
        var policyFileDirectory = Path.GetDirectoryName(policyFile.FullName);
        if (policyFileDirectory != null)
        {
            targetFilePath = Path.Combine(policyFileDirectory, targetFilePath);
        }

        return targetFilePath;
    }
}
