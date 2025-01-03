using IAChat;
using CommandLine;

try
{
    var result = Parser.Default.ParseArguments<Options>(args);

    result.WithParsed(options =>
        {
            if (!string.IsNullOrEmpty(options.ChatUrl) && !string.IsNullOrEmpty(options.ChatModel))
            {
                Config.UpdateEnvFile(options.ChatUrl, options.ChatModel);
            }
            else
            {
                if (!string.IsNullOrEmpty(options.ChatUrl))
                {
                    Config.UpdateEnvFile(options.ChatUrl, null);
                }

                if (!string.IsNullOrEmpty(options.ChatModel))
                {
                    Config.UpdateEnvFile(null, options.ChatModel);
                }
            }
        })
        .WithNotParsed(errors =>
        {
            Console.WriteLine($"Error parsing arguments: {string.Join(',', errors)}");
        });
    if (!Config.HasEnvFile())
    {
        Console.WriteLine("No environment file found, please provide chat URL and model.");
        return;
    }
    
    var settings = Config.GetEnv();
    
    await Chat.ChatWithIA(settings.ChatUrl, settings.ChatModel);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

public class Options
{
    [Option('c', "chat-url", Required = false, HelpText = "Chat URL")]
    public string ChatUrl { get; set; }

    [Option('m', "chat-model", Required = false, HelpText = "Chat Model")]
    public string ChatModel { get; set; }
}