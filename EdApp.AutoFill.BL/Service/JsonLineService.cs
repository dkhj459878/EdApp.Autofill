using System;
using System.Text.RegularExpressions;
using EdApp.AutoFill.BL.Contract.Services;

namespace EdApp.AutoFill.BL.Service;

public class JsonLineService : IJsonLineService
{
    private const string PatternMatchValue = "^(\\s*\\\"[a-zA-Z1 - 9]+\\d*\\\":\\s+\\\"*)[^,\\\"]+";

    public string UpdateValue(string line, string newValue)
    {
        EnsureLineHasCorrectFormat(line);
        return Regex.Replace(line, PatternMatchValue, newValue);
    }

    private void EnsureLineHasCorrectFormat(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            const string errorMessageNullOrEmpty = "Empty line parameter can not be " +
                                        "processed with the UpdateValue method of " +
                                        "an any IJsonLineService interface implementation.";
            throw new ArgumentNullException(nameof(line), errorMessageNullOrEmpty);
        }

        var regex = new Regex(PatternMatchValue);
        if (regex.Matches(line).Count == 1) return;
        var errorMessage = $"Line '{line}' has wrong format.";
        throw new InvalidOperationException(errorMessage);
    }
}