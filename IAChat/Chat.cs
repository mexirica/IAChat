using System.Text;
using Microsoft.Extensions.AI;
using System.Threading;

namespace IAChat;

public static class Chat
{
    public static async Task ChatWithIA(string url, string model)
    {
        var chatClient = new OllamaChatClient(new Uri(url), model);
        List<ChatMessage> chatHistory = new List<ChatMessage>();

        while (true)
        {
            Console.WriteLine("\nAsk your question:");
            var userPrompt = Console.ReadLine();
            chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

            Console.WriteLine("Response:");
            StringBuilder response = new();

            var cancellationTokenSource = new CancellationTokenSource();

            await StreamResponseAsync(chatClient, chatHistory, response, cancellationTokenSource);

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response.ToString()));
            Console.WriteLine();
        }
    }

    private static async Task StreamResponseAsync(OllamaChatClient chatClient, List<ChatMessage> chatHistory, StringBuilder response, CancellationTokenSource cancellationTokenSource)
    {
        var token = cancellationTokenSource.Token;

        var streamingTask = Task.Run(async () =>
        {
            await foreach (var item in chatClient.CompleteStreamingAsync(chatHistory, cancellationToken: token))
            {
                if (CheckForEscapeKey(cancellationTokenSource))
                {
                    break;
                }
                Console.Write(item.Text);
                response.Append(item.Text);
            }
        });

        while (!streamingTask.IsCompleted)
        {
            if (CheckForEscapeKey(cancellationTokenSource))
            {
                break;
            }
            await Task.Delay(100);
        }

        await streamingTask;  
    }

    private static bool CheckForEscapeKey(CancellationTokenSource cancellationTokenSource)
    {
        if (Console.KeyAvailable && Console.ReadKey(intercept: true).Key == ConsoleKey.Escape)
        {
            Console.WriteLine("\nEsc key pressed. Returning to the next question.");
            cancellationTokenSource.Cancel();
            return true;
        }
        return false;
    }
}
