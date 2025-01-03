using System.Text;
using Microsoft.Extensions.AI;

namespace IAChat;

public static class Chat
{
    public static async Task ChatWithIA(string url, string model)
    {
        var chatClient =
            new OllamaChatClient(new Uri(url), model);

        List<ChatMessage> chatHistory = [];

        while (true)
        {
        
            Console.WriteLine("\nAsk your question:");
            var userPrompt = Console.ReadLine();
            chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

            Console.WriteLine("Response:");
            StringBuilder response = new();
            await foreach (var item in
                           chatClient.CompleteStreamingAsync(chatHistory))
            {
                Console.Write(item.Text);
                response.Append(item.Text);
            }

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response.ToString()));
            Console.WriteLine();
        }
    }
}
