using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using static System.Environment;
using System.Collections.Generic; // Added to use List<T>

string? key = GetEnvironmentVariable("AZURE_OPENAI_KEY");
string? endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
string? model = "gpt-4o";

AzureOpenAIClient azureClient = new(
    new Uri(endpoint),
    new AzureKeyCredential(key));

Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Azure Open AI SDK 2.0 - Chat Completions - Pablito Piova");

ChatClient chatClient = azureClient.GetChatClient(model);

ChatCompletionOptions options = new ChatCompletionOptions()
{
    Temperature = 0.7f,
    MaxTokens = 50,
    FrequencyPenalty = 0,
    PresencePenalty = 0
    
};

List<ChatMessage> messages = new List<ChatMessage>
{
    new SystemChatMessage("You are a helpful assistant."),
};

string userInput;
do
{
    Console.Write("Enter your message (type 'exit' to stop): ");
    userInput = Console.ReadLine();
    if (String.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase)) break;

    messages.Add(new UserChatMessage(userInput));
    ChatCompletion completion = chatClient.CompleteChat(messages, options);
    Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");
} while (true);

