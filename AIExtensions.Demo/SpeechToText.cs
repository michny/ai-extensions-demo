using Azure.AI.OpenAI;
using OpenAI.Audio;

public static class SpeechToText
{
    private static AudioClient audioClient;

    static SpeechToText()
    {
        var aiEndpoint = SecretsProvider.GetSecret("AzureAIEndpoint");
        var whisperKey = SecretsProvider.GetSecret("AzureWhisperKey");
        var whisperModelName = "whisper";
        audioClient = new AzureOpenAIClient(new Uri(aiEndpoint), new System.ClientModel.ApiKeyCredential(whisperKey)).GetAudioClient(whisperModelName);
    }

    public static async Task<string> RecognizeSpeechAsync()
    {
        var audioFilePath = "Assets/wikipediaOcelot.wav"; // TODO Figure out a way to record audio on the fly!
        var audioTranscription = await audioClient.TranscribeAudioAsync(audioFilePath);
        return audioTranscription.Value.Text;
    }
}
