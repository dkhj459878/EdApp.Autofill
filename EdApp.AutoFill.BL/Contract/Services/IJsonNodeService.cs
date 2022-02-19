using System.Collections.Generic;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services;

/// <summary>
/// Handles json formatting (text) file like a collection of string lines
/// for updating them.
/// </summary>
public interface IJsonNodeService
{
    /// <summary>
    /// Converts json text file in a enumeration of string lines with additional parameters for facilitating handling with this json file, and in particularity for updating it.
    /// </summary>
    /// <returns></returns>
    IEnumerable<JsonNode> GetJsonNodes();

    /// <summary>
    /// Joins enumeration, that presents json text file lines, in a json file back.
    /// </summary>
    /// <param name="jsonNodes">Line in a json file representation like an object, that we are familiar with.</param>
    /// <returns>Json file as a string.</returns>
    string GetJson(IEnumerable<JsonNode> jsonNodes);

    /// <summary>
    /// Update node value in a json file representation like an enumeration corresponding object.
    /// </summary>
    /// <param name="jsonNodes"></param>
    /// <param name="jsonNodePath"></param>
    /// <param name="attributeName"></param>
    /// <param name="value"></param>
    void UpdateNodeValue(IEnumerable<JsonNode> jsonNodes, string jsonNodePath, string attributeName, string value);
}