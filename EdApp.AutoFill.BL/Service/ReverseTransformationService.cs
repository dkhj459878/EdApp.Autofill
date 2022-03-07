using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Extensions;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Service;

public class ReverseTransformationService : IReverseTransformationService
{
    private const string BaseCalculation = "BaseCalculation";
    private const string WindingDesignRoundWire = "WindingDesignRoundWireCalculation";
    private readonly IDeserializer _deserializer;
    private readonly IParameterService _parameterService;

    public ReverseTransformationService(IParameterService parameterService, IDeserializer deserializer)
    {
        _parameterService = parameterService;
        _deserializer = deserializer;
    }

    public string TransformReversely(FileInfo fileInfo, string calculationType)
    {
        var attributeDtos = _deserializer.DeserializeTo<MappingDtoCollection>(fileInfo).attributes;
        var parameterDtos = _parameterService.GetAllParameters(null, null, "CalculationType").ToList();

        var specificCalculationParameters = parameterDtos.Where(p =>
            p.CalculationType.Name.Equals(WindingDesignRoundWire, StringComparison.OrdinalIgnoreCase));
        var uniqueAttributeDtos = RemoveDuplicates(attributeDtos.ToList());
        var notFoundAttributesInParameters = new List<AttributeDto>();
        var dotNotationDictionary = new Dictionary<string, string>();
        foreach (var uniqueAttributeDto in uniqueAttributeDtos)
        {
            if (DoesNotExistAnyWhere(parameterDtos, uniqueAttributeDto))
            {
                notFoundAttributesInParameters.Add(uniqueAttributeDto.Clone());
                continue;
            }

            if (HasOnlyBaseCalculationParameters(parameterDtos, uniqueAttributeDto))
            {
                var parameterDto = GetBaseParameterByName(parameterDtos, uniqueAttributeDto);
                var (key, value) = GenerateDotNotationElementForBaseCalculation(parameterDto, uniqueAttributeDto);
                dotNotationDictionary.Add(key, value);
                continue;
            }

            if (HasOnlyRoundWireCalculationParameters(parameterDtos, uniqueAttributeDto))
            {
                var parameterDto = GetRoundWireParameterByName(parameterDtos, uniqueAttributeDto);
                var (key, value) = GenerateDotNotationElementForSpecificCalculation(parameterDto, uniqueAttributeDto);
                dotNotationDictionary.Add(key, value);
                continue;
            }

            if (!HasBothBaseAndRoundWireCalculationsParameters(parameterDtos, uniqueAttributeDto)) continue;
            {
                var parameterDto = GetRoundWireParameterByName(parameterDtos, uniqueAttributeDto);
                var bothParametersAndAttributes =
                    GenerateDotNotationElementBothForBaseAndSpecificCalculation(parameterDto, uniqueAttributeDto);
                foreach (var (key, value) in bothParametersAndAttributes) dotNotationDictionary.Add(key, value);
            }
        }

        var notMappedAttributeDtos = string.Empty;
        if (!notFoundAttributesInParameters.Any())
            return ConvertDotNotationToJson(dotNotationDictionary) + notMappedAttributeDtos;
        notMappedAttributeDtos = "Attention: Followed below attributes are not mapped:" + Environment.NewLine;
        notMappedAttributeDtos = notFoundAttributesInParameters.Aggregate(notMappedAttributeDtos,
            (current, notFoundAttribute) => current + (notFoundAttribute + Environment.NewLine));

        return ConvertDotNotationToJson(dotNotationDictionary) + notMappedAttributeDtos;
    }

    private bool HasBothBaseAndRoundWireCalculationsParameters(List<ParameterDto> parameterDtos,
        AttributeDto uniqueAttributeDto)
    {
        return HasBaseParameterWithName(parameterDtos, uniqueAttributeDto) &&
               HasRoundWireParameterWithName(parameterDtos, uniqueAttributeDto);
    }

    private bool HasOnlyRoundWireCalculationParameters(List<ParameterDto> parameterDtos,
        AttributeDto uniqueAttributeDto)
    {
        return !HasBaseParameterWithName(parameterDtos, uniqueAttributeDto) &&
               HasRoundWireParameterWithName(parameterDtos, uniqueAttributeDto);
    }

    private bool HasOnlyBaseCalculationParameters(List<ParameterDto> parameterDtos, AttributeDto uniqueAttributeDto)
    {
        return HasBaseParameterWithName(parameterDtos, uniqueAttributeDto) &&
               !HasRoundWireParameterWithName(parameterDtos, uniqueAttributeDto);
    }

    private bool DoesNotExistAnyWhere(List<ParameterDto> parameterDtos, AttributeDto uniqueAttributeDto)
    {
        return !HasBaseParameterWithName(parameterDtos, uniqueAttributeDto) &&
               !HasRoundWireParameterWithName(parameterDtos, uniqueAttributeDto);
    }

    private bool HasRoundWireParameterWithName(IEnumerable<ParameterDto> parameterDtos, AttributeDto attributeDto)
    {
        var hasRoundWireParameterWithName = parameterDtos.Where(p =>
            p.CalculationType.Name.Equals(WindingDesignRoundWire, StringComparison.OrdinalIgnoreCase)).Any(p => p.DesignWireRoundRequest.Trim().Equals(attributeDto.Name.Trim()));
        return hasRoundWireParameterWithName;
    }

    private bool HasBaseParameterWithName(IEnumerable<ParameterDto> parameterDtos, AttributeDto attributeDto)
    {
        var hasBaseParameterWithName = parameterDtos.Where(p =>
            p.CalculationType.Name.Equals(BaseCalculation, StringComparison.OrdinalIgnoreCase)).Any(p => p.ParametersForAllCalculationModules.Trim().Equals(attributeDto.Name.Trim(), StringComparison.OrdinalIgnoreCase));
        return hasBaseParameterWithName;
    }

    private ParameterDto GetBaseParameterByName(IEnumerable<ParameterDto> parameterDtos, AttributeDto attributeDto)
    {
        var baseCalculationParameter = parameterDtos.Where(p =>
            p.CalculationType.Name.Equals(BaseCalculation, StringComparison.OrdinalIgnoreCase)).First(p =>
            p.ParametersForAllCalculationModules.Trim().Equals(attributeDto.Name.Trim(), StringComparison.OrdinalIgnoreCase));
        return baseCalculationParameter;
    }

    private ParameterDto GetRoundWireParameterByName(IEnumerable<ParameterDto> parameterDtos, AttributeDto attributeDto)
    {
        var baseCalculationParameter = parameterDtos.Where(p =>
            p.CalculationType.Name.Equals(WindingDesignRoundWire, StringComparison.OrdinalIgnoreCase)).First(p =>
            p.DesignWireRoundRequest.Trim().Equals(attributeDto.Name.Trim(), StringComparison.OrdinalIgnoreCase));
        return baseCalculationParameter;
    }

    private IEnumerable<KeyValuePair<string, string>> GenerateDotNotationElementBothForBaseAndSpecificCalculation(
        ParameterDto parameterDto,
        AttributeDto attributeDto)
    {
        var value = attributeDto.Value;
        if (parameterDto.DataType.Equals("string", StringComparison.OrdinalIgnoreCase)) value = AddDoubleQuotes(value);

        var dotNotationForBaseAndSpecificCalculation = new List<KeyValuePair<string, string>>
        {
            KeyValuePair.Create($"{parameterDto.ParentEntity}.{parameterDto.Field}", value),
            KeyValuePair.Create($"{parameterDto.Field}", value)
        };

        return dotNotationForBaseAndSpecificCalculation;
    }

    private KeyValuePair<string, string> GenerateDotNotationElementForSpecificCalculation(ParameterDto parameterDto,
        AttributeDto attributeDto)
    {
        var value = attributeDto.Value;
        if (parameterDto.DataType.Equals("string", StringComparison.OrdinalIgnoreCase)) value = AddDoubleQuotes(value);

        return KeyValuePair.Create($"{parameterDto.Field}", value);
    }

    private KeyValuePair<string, string> GenerateDotNotationElementForBaseCalculation(ParameterDto parameterDto,
        AttributeDto attributeDto)
    {
        var value = attributeDto.Value;
        if (parameterDto.DataType.Equals("string", StringComparison.OrdinalIgnoreCase)) value = AddDoubleQuotes(value);

        return KeyValuePair.Create($"{parameterDto.ParentEntity}.{parameterDto.Field}", value);
    }

    private string AddDoubleQuotes(string value)
    {
        return string.IsNullOrEmpty(value) ? null : $@"""{value}""";
    }

    private bool ContainedOnlyInBaseCalculation(IEnumerable<ParameterDto> parameterDtos, string attributeName,
        string specificCalculationType)
    {
        return GetParameterDtoOnCalculationType(parameterDtos, BaseCalculation)
                   .Any(p => p.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase)) &&
               !GetParameterDtoOnCalculationType(parameterDtos, specificCalculationType)
                   .Any(p => p.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));
    }

    private bool ContainedOnlyInSpecificCalculation(IEnumerable<ParameterDto> parameterDtos, string attributeName,
        string specificCalculationType)
    {
        return !GetParameterDtoOnCalculationType(parameterDtos, BaseCalculation)
                   .Any(p => p.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase)) &&
               GetParameterDtoOnCalculationType(parameterDtos, specificCalculationType)
                   .Any(p => p.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));
    }

    private bool ContainedBothInBaseAndSpecificCalculation(IEnumerable<ParameterDto> parameterDtos,
        string attributeName, string specificCalculationType)
    {
        return GetParameterDtoOnCalculationType(parameterDtos, BaseCalculation)
                   .Any(p => p.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase)) &&
               GetParameterDtoOnCalculationType(parameterDtos, specificCalculationType)
                   .Any(p => p.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));
    }

    private ParameterDto GetBaseParameterByName(IEnumerable<ParameterDto> parameterDtos, string parameterName)
    {
        return parameterDtos
            .Where(p => p.CalculationType.Name.Equals(BaseCalculation, StringComparison.OrdinalIgnoreCase))
            .Single(p => p.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));
    }

    private ParameterDto GetSpecificParameterByName(IEnumerable<ParameterDto> parameterDtos, string parameterName,
        string specificCalculationType)
    {
        return parameterDtos
            .Where(p => p.CalculationType.Name.Equals(specificCalculationType, StringComparison.OrdinalIgnoreCase))
            .Single(p => p.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));
    }

    private IEnumerable<ParameterDto> GetParameterDtoOnCalculationType(IEnumerable<ParameterDto> parameterDtos,
        string calculationType)
    {
        return parameterDtos.Where(p =>
            p.CalculationType.Name.Equals(calculationType, StringComparison.OrdinalIgnoreCase));
    }

    private IReadOnlyCollection<AttributeDto> RemoveDuplicates(List<AttributeDto> attributeDtos)
    {
        return RequestRemoveDuplicates(attributeDtos);
    }

    private List<AttributeDto> RequestRemoveDuplicates(List<AttributeDto> attributes)
    {
        var groupsWithDupl = attributes.GroupBy(x => x.Name).Where(gr => gr.Count() > 1).ToList();

        var filteringNames = groupsWithDupl.Select(x => x.Key).ToList();
        attributes = attributes.Where(x => !filteringNames.Contains(x.Name)).ToList();


        foreach (var group in groupsWithDupl)
            if (group.All(x => string.IsNullOrEmpty(x.Value)))
            {
                attributes.Add(group.FirstOrDefault());
            }
            else if (group.Where(x => !string.IsNullOrEmpty(x.Value)).Count() == 1)
            {
                attributes.Add(group.Where(x => !string.IsNullOrEmpty(x.Value)).First());
            }
            else
            {
                var withValueList = group.Where(x => !string.IsNullOrEmpty(x.Value)).ToList();

                //TODO: TIME TESTING IMPLEMENTATION
                attributes.Add(group.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value)));
                //throw new BusinessLogicException($"In simocalc calculation request exist '{withValueList.First().Name}' Attibute with different values: {string.Join(' ', withValueList.Select(x => x.Value).Distinct())}");
            }

        return attributes;
    }

    private string ConvertDotNotationToJson(Dictionary<string, string> data)
    {
        var root = new Node(null, null);
        foreach (var (dotNotationKey, dotNotationValue) in data)
        {
            const string delimiter = ".";
            var keys = dotNotationKey.Split(delimiter);
            var previousNode = root;
            foreach (var key in keys)
            {
                if (key.IsLast(keys))
                {
                    var leaf = new Node(key, dotNotationValue);
                    if (previousNode.HasChild(leaf)) throw new InvalidOperationException("Keys repeated.");
                    previousNode.Add(leaf);
                    previousNode = root;
                    continue;
                }

                if (previousNode.HasChildWithName(key))
                {
                    previousNode = previousNode.GetChild(key);
                    continue;
                }

                var newNode = new Node(key, null);
                previousNode.Add(newNode);
                previousNode = newNode;
            }
        }

        return root.Publish();
    }

    private class MappingDtoCollection
    {
        public List<AttributeDto> attributes { get; set; }
    }

    private class Node
    {
        private const string OpeningCurlyBrace = "{";
        private const string ClosingCurlyBrace = "}";
        private const string Comma = ",";
        private const string DoubleQuote = @"""";
        private const string Colon = ":";

        private readonly IDictionary<string, Node> _children = new Dictionary<string, Node>();

        internal Node(string key, string value)
        {
            Key = key;
            Value = value;
        }

        private bool IsLast { get; set; }

        private Node Parent { get; set; }

        private string Key { get; }

        private string Value { get; }

        private bool IsNode => _children.Any();

        private bool IsRoot => Key is null && Value is null;

        internal bool HasChild(Node node)
        {
            return _children.ContainsKey(node.Key);
        }

        internal string Publish(StringBuilder stringBuilder = null)
        {
            if (IsRoot && !_children.Any()) return "{}";
            stringBuilder ??= new StringBuilder();
            if (IsRoot)
            {
                stringBuilder.Append(OpeningCurlyBrace);
                foreach (var child in _children) child.Value.Publish(stringBuilder);
                stringBuilder.Append(ClosingCurlyBrace);
                return stringBuilder.ToString();
            }

            stringBuilder.Append(DoubleQuote + Key + DoubleQuote + Colon);
            if (IsNode)
            {
                stringBuilder.Append(OpeningCurlyBrace);
                foreach (var child in _children) child.Value.Publish(stringBuilder);
                stringBuilder.Append(ClosingCurlyBrace);
                if (!IsLast) stringBuilder.Append(Comma);
                return stringBuilder.ToString();
            }

            // Is Leaf.
            stringBuilder.Append(Value);
            if (IsLast) return stringBuilder.ToString();
            stringBuilder.Append(Comma);
            return stringBuilder.ToString();
        }

        internal bool HasChildWithName(string name)
        {
            return _children.ContainsKey(name);
        }

        internal Node GetChild(string name)
        {
            return _children[name];
        }

        internal void Add(Node node)
        {
            var last = _children.Any() ? _children.Last().Value : null;
            if (last is not null) last.IsLast = false;
            _children.Add(node.Key, node);
            node.Parent = this;
            node.IsLast = true;
        }
    }
}