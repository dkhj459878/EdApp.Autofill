using System;
using System.Collections.Generic;
using System.Linq;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Enums;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Service;

public class JsonTreeService : IJsonTreeService
{
    #region ctor

    public JsonTreeService(IJsonDataLoaderService jsonDataLoaderService, IParameterService parameterService)
    {
        _jsonDataLoaderService = jsonDataLoaderService;
        _parameterService = parameterService;
    }

    #endregion

    public Node ConvertToNode(string json)
    {
        IEnumerable<AttributeDto> attributes =
            _jsonDataLoaderService.GetJsonDataConvertedToObject(JsonKind.SiemensJson);
        var unwrappedNode =
            GetRootUnwrappedNode((IReadOnlyCollection<ParameterDto>) _parameterService.GetAllParameters(), attributes);
        throw new NotImplementedException();
    }

    public string ConvertToJson(Node node)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     *9-
    ///     Transforms collection of the simocalc attributes into the collection of the unwrapped
    ///     nodes (with full qualified node keys (names) about node, for example, 'root.BaseCalculation.Tra.BgRotorSheet.Id'.)
    ///     as a preliminary step for transformation of the simocalc attributes collection into the nodes structure, and then
    ///     consequently would be transformed as a
    ///     json file (not through this method).
    /// </summary>
    /// <param name="parameters">
    ///     Parameters of the mapping table that help to map simocalc
    ///     attributes into a collection of unwrapped nodes.
    /// </param>
    /// <param name="attributes">Simocalc attributes collection.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when some attribute name in a simocalc
    ///     attribute are not found into mapping table. Needed to be somehow handled manually.
    /// </exception>
    private UnwrappedNode GetRootUnwrappedNode(IReadOnlyCollection<ParameterDto> parameters,
        IEnumerable<AttributeDto> attributes)
    {
        ICollection<UnwrappedNode> unwrappedNodes = new List<UnwrappedNode>();
        var parentUnwrappedNode = GenerateParentUnwrappedNode();

        foreach (var attribute in attributes)
        {
            var correspondingParameter = GetCorrespondingParameter(parameters, attribute);

            #region Defensive code

            EnsureParameterIsNotNull(correspondingParameter, attribute);

            #endregion

            // We add parentUnwrapped node into the method, that generate unwrapped node, to refer to it 
            // as a parent node to maintain consistency of the tree.
            var unwrappedNode = GenerateUnwrappedNode(attribute, correspondingParameter, parentUnwrappedNode);
            unwrappedNodes.Add(unwrappedNode);
        }

        parentUnwrappedNode.Descendants = unwrappedNodes;
        // This is implicitly, but here is we are processing descendants in such a way, that they are grouped
        // by
        // keys and from their full qualified path parameter value we remove this key values they are grouped by. 
        // For further details, please see Descendants property getter realization in the UnwrappedNode
        // object. Lazy loading pattern is realized here by the way.
        parentUnwrappedNode.Descendants = parentUnwrappedNode.Descendants;
        return parentUnwrappedNode;
    }

    private static UnwrappedNode GenerateParentUnwrappedNode()
    {
        const string root = "root";
        var parentUnwrappedNode = new UnwrappedNode
        {
            Key = root,
            FullQualifiedPath = root
        };
        parentUnwrappedNode.Parent = parentUnwrappedNode;
        return parentUnwrappedNode;
    }

    private static UnwrappedNode GenerateUnwrappedNode(AttributeDto attribute, ParameterDto parameter,
        UnwrappedNode parentUnwrappedNode)
    {
        const string prefixForBaseCalculation = "BaseCalculation";
        var key = string.IsNullOrEmpty(parameter.ParentEntity)
            ? string.Empty
            : $"{prefixForBaseCalculation}";
        var isLeaf = string.IsNullOrEmpty(key);
        var unwrappedNode = new UnwrappedNode
        {
            FullQualifiedPath = isLeaf
                ? $"{parameter.Field}"
                : $"{prefixForBaseCalculation}.{parameter.Field}",
            LeafName = isLeaf ? parameter.Field : null,
            Value = attribute.Value,
            Type = parameter.DataType,
            Parent = parentUnwrappedNode
        };
        return unwrappedNode;
    }

    private ParameterDto GetCorrespondingParameter(IEnumerable<ParameterDto> parameters, AttributeDto attribute)
    {
        return parameters.SingleOrDefault(p => p.Name.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase));
    }

    private void EnsureParameterIsNotNull(ParameterDto parameter, AttributeDto attribute)
    {
        if (parameter is null)
        {
            const string message =
                "There is not corresponding data in mapping table for attribute with name '{0}'. You should do something manually with this case.";
            throw new InvalidOperationException(string.Format(message, attribute.Name));
        }
    }

    #region Fields

    private readonly IJsonDataLoaderService _jsonDataLoaderService;
    private readonly IParameterService _parameterService;

    #endregion
}