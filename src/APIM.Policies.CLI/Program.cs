using APIM.Policies.CLI.Commands;

namespace APIM.Policies.CLI;

public static class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.SetApplicationName("apimp");

            config.AddBranch("list", list =>
            {
                list.AddCommand<ListPolicyExpressionsCommand>("policy-expressions");
                list.AddCommand<ListPolicyFilesCommand>("policy-files");
            });
            config.AddCommand<MergeCommand>("merge");
        });

        return app.Run(args);
    }
}