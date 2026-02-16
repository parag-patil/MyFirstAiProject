using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureAIInference;
using OpenAI.Chat;

namespace MyFirstAiProject;

public class GithubInference(IConfigurationRoot config)
{
    private readonly string? _apiKey = config["GithubInference:ApiKey"];
    private readonly string? _modelId = config["GithubInference:ModelId"];
    private readonly string? _endpoint = config["GithubInference:Endpoint"];

    public async Task RunAi()
    {
        var client = new ChatCompletionsClient(
            new Uri(_endpoint),
            new AzureKeyCredential(_apiKey),
            new AzureAIInferenceClientOptions());


        while (true)
        {
            ChatTokenUsage usage = null!;

            Console.WriteLine("Enter exit to exit...");
            Console.WriteLine("Enter your prompt:");

            var input = Console.ReadLine();
            var requestOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatRequestUserMessage(input),
                },
                Temperature = 0.9f,
                NucleusSamplingFactor = 1.0f,
                MaxTokens = 1500,
                Model = _modelId
            };

            Response<ChatCompletions> response = client.Complete(requestOptions);
            System.Console.WriteLine(response.Value.Content);
            
            Console.WriteLine("=============================================");
            Console.WriteLine($"Input Tokens: {usage.InputTokenCount}");
            Console.WriteLine($"Output Tokens: {usage.OutputTokenCount}");
            Console.WriteLine($"Total Tokens: {usage.TotalTokenCount}");
        }

        // var builder = Kernel.CreateBuilder();
        //
        // builder.AddAzureAIInferenceChatCompletion(_modelId, _apiKey , new Uri(_endpoint));
        // Kernel kernel = builder.Build();
        //
        // var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        // AzureAIInferencePromptExecutionSettings settings = new()
        // {
        //     Temperature = 0.9f,
        //     MaxTokens = 1500
        // };
        //
        // var history = new ChatHistory();
        // var reducer = new ChatHistoryTruncationReducer(2);
        //
        // Console.WriteLine("=============================================");
        //
        // foreach (var kv in chatCompletionService.Attributes)
        // {
        //     Console.WriteLine($"{kv.Key}: {kv.Value}");
        // }
        //
        // Console.WriteLine("=============================================");
        //
        // while (true)
        // {
        //     Console.WriteLine("Enter exit to exit...");
        //     Console.WriteLine("Enter your prompt:");
        //     
        //     var input = Console.ReadLine();
        //     if (input == "exit")
        //     {
        //         break;
        //     }
        //     
        //     var fullResponse = "";
        //     ChatTokenUsage usage = null!;
        //     history.AddUserMessage(input!);
        //
        //     try
        //     {
        //         var response = await chatCompletionService.GetChatMessageContentsAsync(history);
        //         Console.WriteLine(response);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        //
        //     
        //     
        //     // await foreach (StreamingChatMessageContent responseChunk in chatCompletionService
        //     //                    .GetStreamingChatMessageContentsAsync(history, settings))
        //     // {
        //     //     Console.WriteLine(responseChunk.Content);
        //     //     fullResponse += responseChunk.Content;
        //     //     usage = ((OpenAI.Chat.StreamingChatCompletionUpdate)responseChunk.InnerContent).Usage;
        //     // }
        //
        //     Console.WriteLine("=============================================");
        //     Console.WriteLine($"Input Tokens: {usage.InputTokenCount}");
        //     Console.WriteLine($"Output Tokens: {usage.OutputTokenCount}");
        //     Console.WriteLine($"Total Tokens: {usage.TotalTokenCount}");
        //}
    }
}