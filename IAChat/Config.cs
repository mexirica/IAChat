namespace IAChat;

public struct Model
{
    public string ChatUrl { get; set; }
    public string ChatModel { get; set; }
}
public static class Config
{
    private const string EnvFilePath = ".env";

    public static bool HasEnvFile()
    {
        var exists = File.Exists(EnvFilePath);
        bool isEmpty = true;
        try
        {
            isEmpty = new FileInfo(EnvFilePath).Length == 0;
        }
        catch
        {
            return false;
        }
        
        return exists && !isEmpty;
    }

    public static void UpdateEnvFile(string? url, string? model)
    {
        if (!File.Exists(EnvFilePath))
        {
            Console.WriteLine("Env file not found. Creating a new one.");
            File.WriteAllLines(EnvFilePath, new List<string>());
        }

        var envVariables = File.ReadAllLines(EnvFilePath).ToList();

        if (!string.IsNullOrWhiteSpace(url))
        {
            var urlIndex = envVariables.FindIndex(line => line.StartsWith("CHAT_URL="));
            if (urlIndex >= 0)
            {
                envVariables[urlIndex] = $"CHAT_URL={url}";
            }
            else
            {
                envVariables.Add($"CHAT_URL={url}");
            }
        }

        if (!string.IsNullOrWhiteSpace(model))
        {
            var modelIndex = envVariables.FindIndex(line => line.StartsWith("CHAT_MODEL="));
            if (modelIndex >= 0)
            {
                envVariables[modelIndex] = $"CHAT_MODEL={model}";
            }
            else
            {
                envVariables.Add($"CHAT_MODEL={model}");
            }
        }

        File.WriteAllLines(EnvFilePath, envVariables);
    }

    
    public static Model GetEnv()
    {
        var model = new Model();

        var lines = File.ReadAllLines(EnvFilePath);
        
        model.ChatUrl = lines[0].Split("=")[1];
        model.ChatModel = lines[1].Split("=")[1];
        
        return model;
    }
}