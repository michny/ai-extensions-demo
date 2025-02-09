using Microsoft.CognitiveServices.Speech;

public class TextToSpeech 
{
    public static async Task SynthesizeAndPlayAudioAsync(string text)
    {
        // Load configuration from secure storage or app settings
        string key= SecretsProvider.GetSecret("AzureSpeechKey");
        string region = "westeurope";

        var speechConfig = SpeechConfig.FromSubscription(key, region);

        string voiceName = "en-US-GuyNeural";
        // string voiceName = "en-US-JennyNeural";
        // string voiceName = "en-US-AriaNeural";

        speechConfig.SetProperty(PropertyId.SpeechServiceConnection_SynthVoice, voiceName);

        using var synthesizer = new SpeechSynthesizer(speechConfig);

        using var memoryStream = new MemoryStream();
        synthesizer.SynthesisCompleted += (s, e) =>
        {
            if (e.Result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                if (memoryStream is null || !memoryStream.CanSeek) return;
                memoryStream.Seek(0, SeekOrigin.Begin);
                #if WINDOWS
                    using var player = new SoundPlayer(memoryStream);
                    player.PlaySync();
                #else
                    Console.WriteLine("Audio playback is not supported on this platform.");
                #endif
            }
            else
            {
                Console.WriteLine($"Speech synthesis failed: {e.Result.Reason}");
            }
        };

        await synthesizer.SpeakTextAsync(text);
    }    
}
