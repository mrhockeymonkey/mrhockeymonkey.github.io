// See https://aka.ms/new-console-template for more information

using System.ClientModel;
using ClientExamples.OpenAI;
using OpenAI;
using OpenAI.Chat;

Console.WriteLine("Hello, World!");

// curl https://api.mistral.ai/v1/chat/completions -H "Authorization: Bearer ..." -H "Content-Type: application/json" -d @req.json

//OllamaSimpleChatCompletion.Run();

await OllamaWithTools.Run();


// var f = chatClient.CompleteChatStreaming();
//
// ChatMessage[] messages =
// [
//     new UserChatMessage("What is a good name for a cat?")
// ];
// var completionUpdates = chatClient.CompleteChatStreaming(messages);
// Console.Write($"[ASSISTANT]: ");
// foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
// {
//     if (completionUpdate.ContentUpdate.Count > 0)
//     {
//         Console.Write(completionUpdate.ContentUpdate[0].Text);
//     }
// }