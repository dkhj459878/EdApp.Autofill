using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using EdApp.AutoFill.BL.Contract.Services;

namespace EdApp.AutoFill.BL.Service;

public class Deserializer : IDeserializer
{
    public T DeserializeTo<T>(FileInfo fullFilePath, JsonSerializerOptions options = null)
    {
        EnsureFileExists(fullFilePath);

        var readText = File.ReadAllText(fullFilePath.FullName);
        var jsonNode = System.Text.Json.Nodes.JsonNode.Parse(readText);
        var cleanedText = jsonNode?.ToString();

        EnsureJsonTextIsNotNull(cleanedText);

        return JsonSerializer.Deserialize<T>(cleanedText, GetJsonSerializerOptions(options));
    }

    private void EnsureJsonTextIsNotNull(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            const string message = "Conversion of the json file was not successful.";
            throw new ArgumentNullException(message);
        }
    }


    private void EnsureFileExists(FileInfo fullFilePath)
    {
        var fileName = fullFilePath.FullName;
        if (!File.Exists(fileName))
        {
            throw new ArgumentOutOfRangeException(nameof(fullFilePath), "File does not exist.");
        }
    }

    private JsonSerializerOptions GetJsonSerializerOptions(JsonSerializerOptions options)
    {
        var defaultOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return options ?? defaultOptions;
    }
}