using McMaster.Extensions.CommandLineUtils;

var app = new CommandLineApplication();

var path = "";

app.Command("run", runCmd =>
{
    runCmd.OnExecute(() =>
    {
        Console.WriteLine("Specify a subcommand");
        runCmd.ShowHelp();
    });
    runCmd.Command("lab3", labExecute =>
    {
        runCmd.Command("setPath", pathExecute =>
        {
            var option = pathExecute.Option("-p||--path", "Specify path to lab", CommandOptionType.SingleValue).IsRequired();
            pathExecute.OnExecute(() =>
            {
                if (option.Values.Any()) path = option.Values[0];
            });
        });
        
        labExecute.OnExecute(() =>
        {
            var input = labExecute.Option("-i|--input", "Input file name", CommandOptionType.SingleValue);
            var output = labExecute.Option("-o|--output", "Output file name", CommandOptionType.SingleValue);
            if (path is not "")
            {
                var lab = new LabExecution.LabExecution();
                lab.Run(path);
            }
            else
            {
                Console.WriteLine("There this no path to execute lab!");
            }
        });
    });
});

app.Command("version", versionCmd =>
{
    versionCmd.OnExecute(() =>
    {
        Console.WriteLine("Oleg Sabadaha");
        Console.WriteLine("Version 1.0");
    });
});


return app.Execute(args);