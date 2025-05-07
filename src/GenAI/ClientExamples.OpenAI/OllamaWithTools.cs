using System.ClientModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using OpenAI;
using OpenAI.Chat;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

namespace ClientExamples.OpenAI;

public class OllamaWithTools
{
    public static async Task Run()
    {
        var apiKey = new ApiKeyCredential("ollama");
        var options = new OpenAIClientOptions()
        {
            Endpoint = new("http://localhost:11434/v1")
        };
        ChatClient openAIClient = new OpenAIClient(apiKey, options).GetChatClient("qwen2.5:latest");
        
        // get available tools
        var mcpTransportOptions = new SseClientTransport(new()
        {
            Endpoint = new("http://localhost:5251/sse")
        });
        var mcpClient = await McpClientFactory.CreateAsync(mcpTransportOptions);

        Console.WriteLine("Tools available:");
        var tools = await mcpClient.ListToolsAsync();
        foreach (var tool in tools)
        {
            Console.WriteLine($"  {tool}");
        }

        var chatClient = openAIClient.AsIChatClient()
            .AsBuilder()
            .UseFunctionInvocation()
            .Build();

        List<ChatMessage> messages = [];
        ChatOptions chatOptions = new() { Tools = [.. tools] };
        messages.Add(new(ChatRole.User, "I need a new name for my cat"));
        await foreach (var update in chatClient.GetStreamingResponseAsync(messages, chatOptions))
        {
            Console.Write(update);
        }
    }
}