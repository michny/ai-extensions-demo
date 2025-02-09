using Microsoft.Extensions.AI;
using Azure.Identity;
using Azure.AI.OpenAI;


var aiKey = SecretsProvider.GetSecret("AzureAIKey");
var aiEndpoint = SecretsProvider.GetSecret("AzureAIEndpoint");
var chatModelName = "gpt-4o-mini";

// var credential = new DefaultAzureCredential();
// IChatClient client = new AzureOpenAIClient(new Uri(endpoint), credential)
IChatClient client = new AzureOpenAIClient(new Uri(aiEndpoint), new System.ClientModel.ApiKeyCredential(aiKey))
    .AsChatClient(chatModelName);

ChatMessage prompt = new(ChatRole.System, "You are a helpful, yet cheeky AI assistant. All your responses must be phrased as Yoda from Star Wars would do.");
List<ChatMessage> conversation = [ prompt ];

var text = await SpeechToText.RecognizeSpeechAsync();
Console.WriteLine("You said: " + text);

Console.WriteLine("Ask a question to the AI.");
while(true) 
{
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("You must put in a question");
        continue;
    }
    conversation.Add(new ChatMessage(ChatRole.User, input));
    var response = await client.CompleteAsync(conversation);
    Console.WriteLine(response.Message);
    if (response.Message is null)
    {
        Console.WriteLine("No response from AI");
        continue;
    }
    await TextToSpeech.SynthesizeAndPlayAudioAsync(response.Message.Text!);
}
