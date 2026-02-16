using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace MyFirstAiProject;

public static class MyOllama
{
    public static async Task StartOllama()
    {
        var builder = Kernel.CreateBuilder();

        builder.AddOllamaChatCompletion("llama3.2:1b", new Uri("http://localhost:11434"));
        var kernel = builder.Build();

        var chat = kernel.GetRequiredService<IChatCompletionService>();

        var history = new ChatHistory();
        history.AddSystemMessage("""
                                     You are chat assistant. Reply in friendly manner and in short. 
                                     Don't explain long
                                 """);

        Console.WriteLine("Type exit key to exit...");
        Console.WriteLine("===========================================");

        foreach (var kv in chat.Attributes)
        {
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }

        Console.WriteLine("===========================================");

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "exit")
            {
                break;
            }

            history.AddUserMessage(input!);

            var reply = await chat.GetChatMessageContentAsync(history);
            var assistedMessage = reply.Content;

            history.AddAssistantMessage(assistedMessage!);

            Console.WriteLine(assistedMessage);
            Console.WriteLine("===========================================");
        }
    }
}