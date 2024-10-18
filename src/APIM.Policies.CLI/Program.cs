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
            });
        });

        return app.Run(args);
    }
}