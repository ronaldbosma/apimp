using APIM.Policies.CLI.Models;
using System.Text.RegularExpressions;

namespace APIM.Policies.CLI.Analyzers;

internal class PolicyFilesAnalyzer
{
    public static async Task<IList<PolicyFile>> GetPolicyFilesWithExpressionReferences(string folder, string include, string? exclude)
    {
        var policyFiles = Directory.GetFiles(folder, include, SearchOption.AllDirectories).ToList();
        if (!string.IsNullOrWhiteSpace(exclude))
        {
            policyFiles = policyFiles.Where(file => !file.EndsWith(exclude, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var result = new List<PolicyFile>();
        foreach (var filePath in policyFiles)
        {
            const string referencePattern = @"@expression:([a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*)";
            var content = await File.ReadAllTextAsync(filePath);

            var matches = Regex.Matches(content, referencePattern);

            if (matches.Count > 0)
            {
                var references = new HashSet<string>();

                foreach (Match match in matches)
                {
                    references.Add(match.Groups[1].Value);
                }

                result.Add(new PolicyFile
                {
                    Name = Path.GetFileName(filePath),
                    FullName = filePath,
                    Content = content,
                    PolicyExpressionReferences = references
                });
            }
        }

        return result;
    }
}
