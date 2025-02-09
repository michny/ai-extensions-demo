using Microsoft.CognitiveServices.Speech;

public class TextToSpeech 
{
    public static async Task SynthesizeAndPlayAudioAsync(string text)
    {
        // Load configuration from secure storage or app settings
        string key= SecretsProvider.GetSecret("AzureSpeechKey");
        string region = SecretsProvider.GetSecret("AzureSpeechRegion");;

        var speechConfig = SpeechConfig.FromSubscription(key, region);

        // TODO figure out how to use custom voices
        // string voiceName = "da-DK-ChristelNeural";
        string voiceName = "da-DK-JeppeNeural";
        // string voiceName = "en-US-GuyNeural";
        // string voiceName = "en-US-JennyNeural";
        // string voiceName = "en-US-AriaNeural";

        speechConfig.SpeechSynthesisVoiceName = voiceName;

        using var synthesizer = new SpeechSynthesizer(speechConfig);
        using var result = await synthesizer.SpeakTextAsync(text);
        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            Console.WriteLine($"Speech synthesized for text [{text}]");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

            if (cancellation.Reason == CancellationReason.Error)
            {
                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                Console.WriteLine($"CANCELED: Did you update the subscription info?");
            }
        }
        // using var memoryStream = new MemoryStream();
        // synthesizer.SynthesisCompleted += (s, e) =>
        // {
        //     if (e.Result.Reason == ResultReason.SynthesizingAudioCompleted)
        //     {
        //         if (memoryStream is null || !memoryStream.CanSeek) return;
        //         memoryStream.Seek(0, SeekOrigin.Begin);
        //         #if WINDOWS
        //             using var player = new SoundPlayer(memoryStream);
        //             player.PlaySync();
        //         #else
        //             Console.WriteLine("Audio playback is not supported on this platform.");
        //         #endif
        //     }
        //     else
        //     {
        //         Console.WriteLine($"Speech synthesis failed: {e.Result.Reason}");
        //     }
        // };

        // await synthesizer.SpeakTextAsync(text);
    }    
}
