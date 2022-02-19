using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services;

/// <summary>
/// From Siemens side we have valid json workload that is suitable for sending it to the simocalc service. But we need reverse work: prepare workload for Ed-app end-point, which after some processing convert it into exactly, that Siemens provided.
/// </summary>
public interface IJsonTreeService
{
    /// <summary>
    /// Converts json into tree structure.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    Node ConvertToNode(string json);

    /// <summary>
    /// Revert convert processed tree structure into json.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    string ConvertToJson(Node node);
}