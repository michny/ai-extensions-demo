# AI Extensions Speech-to-Speech Chat

This demo is an attempt at creating a conversational chat where the input and output are both speech. 

- The input speech is transformed to text using a `whisper` model
- The answer to the input is generated via a chat model such as `gpt-4o-mini`
- The output is converted to speech using an Azure Speech Service resource

## Running the App

Create an `Azure AI service` in Azure and deploy a `gpt-4o-mini` model (with the same name) and a `whisper` model (with the same name). 
Initialize user secrets locally if not already done:

```
dotnet user-secrets init
```

Fetch the Key from the deployed `gpt-4o-mini` model and add it to your local user secrets:

```
dotnet user-secrets set "AzureAIKey" "<your-gpt-key>"
```

Fetch the Key from the deployed `whisper` model and add it to your local user secrets:

```
dotnet user-secrets set "AzureWhisperKey" "<your-whisper-key>"
```

Add the endpoint of the resource to your local user secrets. The endpoint should look something like this `https://<my-resource-name>.openai.azure.com/`:

```
dotnet user-secrets set "AzureAIEndpoint" "<your-endpoint>"
```

Create a `Speech Service` in Azure and put the Key into your local user secrets:

```
dotnet user-secrets set "AzureSpeechKey" "<your-speech-key>"
```

Add the region you deployed the `Speech Service` in to your local user secrets:

```
dotnet user-secrets set "AzureSpeechRegion" "<your-region>"
```

Run the Console Application.
