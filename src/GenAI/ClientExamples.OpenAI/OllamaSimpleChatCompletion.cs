using System.ClientModel;
using OpenAI;
using OpenAI.Chat;

namespace ClientExamples.OpenAI;

public class OllamaSimpleChatCompletion
{
    public static void Run()
    {
        var apiKey = new ApiKeyCredential("ollama");
        var options = new OpenAIClientOptions()
        {
            Endpoint = new("http://localhost:11434/v1")
        };

        ChatClient client = new(model: "qwen2.5:latest", apiKey, options);
        
        CollectionResult<StreamingChatCompletionUpdate> ups = client.CompleteChatStreaming("Say 'this is a test.' 20 times");
        Console.Write($"[ASSISTANT]: ");
        foreach (StreamingChatCompletionUpdate completionUpdate in ups)
        {
            if (completionUpdate.ContentUpdate.Count > 0)
            {
                Console.Write(completionUpdate.ContentUpdate[0].Text);
            }
        }
    }
}