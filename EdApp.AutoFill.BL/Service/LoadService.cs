using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using EdApp.AutoFill.BL.Constant;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Enums;
using EdApp.AutoFill.BL.Extensions;
using EdApp.AutoFill.BL.Model;
using EdApp.AutoFill.DAL.Contract;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace EdApp.AutoFill.BL.Service;

/// <inheritdoc cref="ILoadAllDataService" />
public class LoadService : ILoadAllDataService
{
    private const string ExcelFileFullPath = @"d:\Work\Siemens\docs\Mapping field names_2022_03_02.xlsx";
    private const string BaseCalculation = "BaseCalculation";
    private const string WindingDesignFlatWire = "WindingDesignFlatWireCalculation";
    private const string WindingDesignRoundWire = "WindingDesignRoundWireCalculation";
    private const string DynamicTorque = "DynamicTorque";
    private const string Request = "Request";
    private const string Response = "Response";
    private const string Common = "Common";
    private const string Mandatory = "mandatory";
    private const int StartRowIndexProd = 12;
    private const int EndRowIndexProd = 1740;
    private const int StartRowIndex = StartRowIndexProd;
    private const int EndRowIndex = EndRowIndexProd;
    private readonly IAttributeDtoService _attributeDtoService;
    private readonly IAttributesForSimocalcService _attributesForSimocalcService;
    private readonly ICalculationTypeService _calculationTypeService;
    private readonly IExcel _excelManager;
    private readonly IJsonDataLoaderService _jsonDataLoaderService;
    private readonly IModelTypeService _modelTypeService;
    private readonly IParameterService _parameterService;
    private CalculationTypeDto _basicCalculation;
    private ModelTypeDto _common;
    private CalculationTypeDto _dynamicTorque;
    private ModelTypeDto _request;
    private ModelTypeDto _response;
    private CalculationTypeDto _windingDesignFlatWire;
    private CalculationTypeDto _windingDesignRoundWire;

    public LoadService(IParameterService parameterService, ICalculationTypeService calculationTypeService,
        IModelTypeService modelTypeService, IExcel excelManager, IJsonDataLoaderService jsonDataLoaderService,
        IAttributeDtoService attributeDtoService, IAttributesForSimocalcService attributesForSimocalcService)
    {
        _calculationTypeService = calculationTypeService;
        _modelTypeService = modelTypeService;
        _excelManager = excelManager;
        _parameterService = parameterService;
        _jsonDataLoaderService = jsonDataLoaderService;
        _attributeDtoService = attributeDtoService;
        _attributesForSimocalcService = attributesForSimocalcService;
    }

    public void LoadAll()
    {
        //LoadCalculationTypes();
        //LoadModelTypes();
        //SetupEnvironmentParameters();
        //LoadParameters();
        //LoadAttributesForSimocalcDto();
        //LoadAttributes();
        CompareAttributes();
    }

    private void CompareAttributes()
    {
        #region Setup data

        var siemensAttributes = GetSiemensAttributes();
        var myAttributes = GetMyAttributes();
        var commonParameters = GetCommonParameters();
        var notInSiemens = myAttributes.Subtract(siemensAttributes);
        var notInMyJson = siemensAttributes.Subtract(myAttributes);
        var anywhere = myAttributes.HasCommonWith(siemensAttributes);
        var commonEqual = myAttributes.HasEqualWith(siemensAttributes);
        var commonSimilar = myAttributes.HasSimilarWith(siemensAttributes);
        var notEqual = myAttributes.Subtract(commonEqual);

        var notEqualComparison = notEqual.Select(ne =>
        {
            var siemensAttributeDto =
                siemensAttributes.Single(sa => sa.Name.Equals(ne.Name, StringComparison.OrdinalIgnoreCase));
            return new
            {
                ne.Name,
                Value = (ne.Value ?? string.Empty).Equals(siemensAttributeDto.Value ?? string.Empty, StringComparison.OrdinalIgnoreCase) ? "The same" : @$"{ne.Value ?? string.Empty} | {siemensAttributeDto.Value ?? string.Empty}",
                Unit = (ne.Unit ?? string.Empty).Equals(siemensAttributeDto.Unit ?? string.Empty, StringComparison.OrdinalIgnoreCase) ? "The same" : @$"{ne.Unit ?? string.Empty} | {siemensAttributeDto.Unit ?? string.Empty}",
                Description = (ne.Description ?? string.Empty).Equals((siemensAttributeDto.Description ?? string.Empty), StringComparison.OrdinalIgnoreCase) ? "The same" :
                    @$"""{ne.Description ?? string.Empty}"" | ""{siemensAttributeDto.Description ?? string.Empty}""",
            };
        });
        var jsonDifference = JsonSerializer.Serialize(notEqualComparison);

        #endregion



        var attributesNotInMy = myAttributes.Subtract(commonSimilar);

        var notInSiemensResult = GetData(notInSiemens, commonParameters);
        var notInMyJsonResult = GetData(notInMyJson, commonParameters);

        var requiredUpdatedInMyEndPointMode =
            GetRequiredUpdateInMyEndPointModel(attributesNotInMy, commonParameters, siemensAttributes);

        var attributeNotInSiemens = siemensAttributes.Subtract(commonSimilar);

        var attributeAreEqual = attributesNotInMy.HasCommonWith
            (attributeNotInSiemens);

        var jsonEdApp = GetJsonFromAttributeDto(siemensAttributes, commonParameters);

        //var retrieving = commonParameters.Where(p =>
        //differenceAttributesInMy.Any(d =>
        //d.Name.Equals(p.ParametersForAllCalculationModules, StringComparison.OrdinalIgnoreCase)));

        var result = attributeNotInSiemens.Select(x =>
        {
            var parameter = commonParameters.SingleOrDefault(x =>
                x.ParametersForAllCalculationModules.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
            var myAttribute = myAttributes.Single(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
            return new
            {
                parameter?.ParentEntity,
                parameter?.Field,
                OldValue = myAttribute.Value ?? string.Empty,
                NewValue = x.Value ?? string.Empty,
                NewUnit = x.Unit ?? string.Empty,
                OldUnit = myAttribute.Unit ?? string.Empty
            };
        }).ToArray();

        var siemensJsonOrdered = JsonSerializer.Serialize(siemensAttributes.OrderBy(x => x.Name));
        var myJsonOrdered = JsonSerializer.Serialize(myAttributes.OrderBy(x => x.Name));

        var jsonResult = JsonSerializer.Serialize(result);

        var json = JsonSerializer.Serialize(result);
    }

    private string GetJsonFromAttributeDto(IReadOnlyCollection<AttributeDto> siemensAttributes,
        IEnumerable<ParameterDto> commonParameters)
    {
        throw new NotImplementedException();
    }


    private string GetData(IReadOnlyCollection<AttributeDto> notInSiemens, IEnumerable<ParameterDto> commonParameters)
    {
        var result = notInSiemens.Select(x =>
        {
            var attribute =
                commonParameters.FirstOrDefault(p => p.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
            if (attribute is null) return $"{x.Name}:{x.Value} -> no data from Siemens";

            return $"{attribute.ParentEntity}.{attribute.Name}: {x.Value} -> no data from ";
        }).ToArray();
        return string.Join(Environment.NewLine, result);
    }


    private string GetRequiredUpdateInMyEndPointModel(IEnumerable<AttributeDto> attributesNotInMy,
        IEnumerable<ParameterDto> commonParameters, IReadOnlyCollection<AttributeDto> fromSiemens)
    {
        var result = attributesNotInMy.Select(x =>
        {
            var attribute =
                commonParameters.SingleOrDefault(p => p.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
            var attributeFromSiemens = fromSiemens.SingleOrDefault(s => s.Name.Equals(x.Name));
            if (attribute is null)
            {
                if (attributeFromSiemens is not null) return $"{x.Name}:{x.Value} -> {attributeFromSiemens.Value}";
                return $"{x.Name}:{x.Value} -> no data from Siemens";
            }

            if (attributeFromSiemens is not null)
                return $"{attribute.ParentEntity}.{attribute.Name}: {x.Value} -> {attributeFromSiemens.Value}";
            return $"{attribute.ParentEntity}.{attribute.Name}: {x.Value} -> no data from ";
        }).ToArray();
        return string.Join(Environment.NewLine, result);
    }

    private IEnumerable<ParameterDto> GetCommonParameters()
    {
        return _parameterService.GetAllParameters();
    }

    private IReadOnlyCollection<AttributeDto> GetMyAttributes()
    {
        return (IReadOnlyCollection<AttributeDto>) _jsonDataLoaderService.GetJsonDataConvertedToObject(JsonKind.MyJson);
    }

    private IReadOnlyCollection<AttributeDto> GetSiemensAttributes()
    {
        return (IReadOnlyCollection<AttributeDto>) _jsonDataLoaderService.GetJsonDataConvertedToObject(
            JsonKind.SiemensJson);
    }

    private void LoadAttributesForSimocalcDto()
    {
        if (!_attributesForSimocalcService.GetAllAttributesForSimocalcs().IsNullOrEmpty()) return;

        AttributesForSimocalcDto attributesForSimocalcDto = new()
        {
            CalculationType = _windingDesignFlatWire
        };
        _attributesForSimocalcService.AddAttributesForSimocalc(attributesForSimocalcDto);
    }

    private void LoadAttributes()
    {
        if (!_attributeDtoService.GetAllAttributeDtos().IsNullOrEmpty()) return;
        var attributes = _jsonDataLoaderService.GetJsonDataConvertedToObject(JsonKind.SiemensJson);
        if (attributes.IsNullOrEmpty())
        {
            const string errorMessage = "Attribute data have not been deserialized correctly.";
            throw new InvalidOperationException(errorMessage);
        }

        var attributesForSimocalcDto = _attributesForSimocalcService
            .GetAllAttributesForSimocalcs(x => x.CalculationTypeId == _windingDesignFlatWire.Id).Single();
        foreach (var attribute in attributes)
        {
            attribute.CalculationType = _windingDesignFlatWire;
            attribute.AttributeDtosForSimocalc = attributesForSimocalcDto;
            _attributeDtoService.AddAttributeDto(attribute);
        }
    }

    private void LoadSimocalcJson()
    {
        if (!_parameterService.GetAllParameters().IsNullOrEmpty()) return;
    }


    public T DeserializeRow<T>(ExcelManager excelManager, Dictionary<PropertyInfo, int> map,
        int index) where T : class, IIdentifier, new()
    {
        var deserializing = new T();
        foreach (var (propertyInfo, value) in map)
            AssignTo(deserializing, propertyInfo, excelManager.GetCellValue(index, value));

        return deserializing;
    }


    private void AssignTo<T>(T obj, PropertyInfo propertyInfo, object value)
    {
        propertyInfo.SetValue(obj, value, null);
    }

    private void LoadParameters()
    {
        if (!_parameterService.GetAllParameters().IsNullOrEmpty()) return;
        // Open excel sheet.
        var excel = new Application();
        var wb = excel.Workbooks.Open(ExcelFileFullPath);
        var worksheet = (Worksheet) wb.Sheets[1];
        try
        {
            var index = StartRowIndex - 1;
            while (index < EndRowIndex)
            {
                index++;
                if (IsNoData(worksheet, index)) continue;

                var commonData = GenerateCommonParameter(worksheet, index);
                if (commonData != null) _parameterService.AddParameter(commonData);

                var flatRequest = GenerateFlatRequestParameter(worksheet, index);
                if (flatRequest != null) _parameterService.AddParameter(flatRequest);

                var flatResponse = GenerateFlatResponseParameter(worksheet, index);
                if (flatResponse != null) _parameterService.AddParameter(flatResponse);

                var roundRequest = GenerateRoundRequestParameter(worksheet, index);
                if (roundRequest != null) _parameterService.AddParameter(roundRequest);

                var roundResponse = GenerateRoundResponseParameter(worksheet, index);
                if (roundResponse != null) _parameterService.AddParameter(roundResponse);

                var torqueRequest = GenerateTorqueRequestParameter(worksheet, index);
                if (torqueRequest != null) _parameterService.AddParameter(torqueRequest);

                var torqueResponse = GenerateTorqueResponseParameter(worksheet, index);
                if (torqueResponse != null) _parameterService.AddParameter(torqueResponse);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            // Clear resources.
            wb.Close();
            excel.Quit();
            Marshal.FinalReleaseComObject(wb);
            Marshal.FinalReleaseComObject(excel);
        }
    }

    private void SetupEnvironmentParameters()
    {
        _basicCalculation = _calculationTypeService
            .GetAllCalculationTypes(ct => ct.Name == BaseCalculation).Single();
        _windingDesignFlatWire = _calculationTypeService
            .GetAllCalculationTypes(ct => ct.Name == WindingDesignFlatWire).Single();
        _windingDesignRoundWire = _calculationTypeService
            .GetAllCalculationTypes(ct => ct.Name == WindingDesignRoundWire).Single();
        _dynamicTorque = _calculationTypeService
            .GetAllCalculationTypes(ct => ct.Name == DynamicTorque).Single();
        _request = _modelTypeService.GetAllModelTypes(mt => mt.Name == Request).Single();
        _response = _modelTypeService.GetAllModelTypes(mt => mt.Name == Response).Single();
        _common = _modelTypeService.GetAllModelTypes(mt => mt.Name == Common).Single();
    }

    private void LoadModelTypes()
    {
        if (!_modelTypeService.GetAllModelTypes().IsNullOrEmpty()) return;
        var request = new ModelTypeDto
        {
            Name = Request
        };
        var response = new ModelTypeDto
        {
            Name = Response
        };
        var common = new ModelTypeDto
        {
            Name = Common
        };
        _modelTypeService.AddModelType(request);
        _modelTypeService.AddModelType(response);
        _modelTypeService.AddModelType(common);
    }

    private void LoadCalculationTypes()
    {
        if (!_calculationTypeService.GetAllCalculationTypes().IsNullOrEmpty()) return;
        var windingDesignFlatWireInstance = new CalculationTypeDto
        {
            Name = WindingDesignFlatWire
        };
        var windingDesignRoundWireInstance = new CalculationTypeDto
        {
            Name = WindingDesignRoundWire
        };
        var dynamicTorque = new CalculationTypeDto
        {
            Name = DynamicTorque
        };
        var baseCalculation = new CalculationTypeDto
        {
            Name = BaseCalculation
        };
        _calculationTypeService.AddCalculationType(windingDesignFlatWireInstance);
        _calculationTypeService.AddCalculationType(windingDesignRoundWireInstance);
        _calculationTypeService.AddCalculationType(dynamicTorque);
        _calculationTypeService.AddCalculationType(baseCalculation);
    }

    #region Validations

    private bool IsNoData(Worksheet worksheet, int rowIndex)
    {
        return GetCommonRequest(worksheet, rowIndex).IsNullOrEmpty()
               && GetFlatRequest(worksheet, rowIndex).IsNullOrEmpty()
               && GetFlatResponse(worksheet, rowIndex).IsNullOrEmpty()
               && GetRoundRequest(worksheet, rowIndex).IsNullOrEmpty()
               && GetRoundResponse(worksheet, rowIndex).IsNullOrEmpty()
               && GetTorqueRequest(worksheet, rowIndex).IsNullOrEmpty()
               && GetTorqueResponse(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoCommonData(Worksheet worksheet, int rowIndex)
    {
        return GetVariableNameParametersForAllCalculationModules(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoFlatRequestData(Worksheet worksheet, int rowIndex)
    {
        return GetFlatRequest(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoFlatResponseData(Worksheet worksheet, int rowIndex)
    {
        return GetFlatResponse(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoRoundRequestData(Worksheet worksheet, int rowIndex)
    {
        return GetRoundRequest(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoRoundResponseData(Worksheet worksheet, int rowIndex)
    {
        return GetRoundResponse(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoTorqueRequestData(Worksheet worksheet, int rowIndex)
    {
        return GetTorqueRequest(worksheet, rowIndex).IsNullOrEmpty();
    }

    private bool IsNoTorqueResponseData(Worksheet worksheet, int rowIndex)
    {
        return GetTorqueResponse(worksheet, rowIndex).IsNullOrEmpty();
    }

    #endregion

    #region Values calculations.

    private ParameterDto GenerateCommonParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoCommonData, GetCommonRequest, _common, _basicCalculation, worksheet,
            rowIndex);
    }

    private ParameterDto GenerateFlatRequestParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoFlatRequestData, GetFlatRequest, _request, _windingDesignFlatWire, worksheet,
            rowIndex);
    }

    private ParameterDto GenerateFlatResponseParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoFlatResponseData, GetFlatResponse, _response, _windingDesignFlatWire, worksheet,
            rowIndex);
    }

    private ParameterDto GenerateRoundRequestParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoRoundRequestData, GetRoundRequest, _request, _windingDesignRoundWire, worksheet,
            rowIndex);
    }

    private ParameterDto GenerateRoundResponseParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoRoundResponseData, GetRoundResponse, _response, _windingDesignRoundWire,
            worksheet,
            rowIndex);
    }

    private ParameterDto GenerateParameters(Func<Worksheet, int, bool> checkIfDataPresent,
        Func<Worksheet, int, string> getKeyData, ModelTypeDto modelType,
        CalculationTypeDto calculationType, Worksheet worksheet, int rowIndex)
    {
        if (!checkIfDataPresent(worksheet, rowIndex))
            return new ParameterDto
            {
                Name = GetNameParameter(worksheet, rowIndex),
                ModelType = modelType,
                CalculationType = calculationType,
                MandatoryParameter = GetMandatoryParameter(worksheet, rowIndex),
                MandatoryValue = GetMandatoryValue(worksheet, rowIndex),
                VariableName = GetVariableName(worksheet, rowIndex),
                DescriptionEn = GetDescriptionEn(worksheet, rowIndex),
                Unit = GetUnit(worksheet, rowIndex),
                ParametersForAllCalculationModules =
                    GetVariableNameParametersForAllCalculationModules(worksheet, rowIndex),
                ParentEntity = GetParentEntity(worksheet, rowIndex),
                ExampleFlatDoubleCadge = GetExampleFlatDoubleCadge(worksheet, rowIndex),
                ExampleFlatRotorSingleCadge = GetExampleFlatRotorSingleCadge(worksheet, rowIndex),
                DesignWireFlatRequest = GetFlatRequest(worksheet, rowIndex),
                DesignWireFlatResponse = GetFlatResponse(worksheet, rowIndex),
                DesignWireRoundRequest = GetRoundRequest(worksheet, rowIndex),
                DesignWireRoundResponse = GetRoundResponse(worksheet, rowIndex),
                TorqueRequest = GetTorqueRequest(worksheet, rowIndex),
                TorqueResponse = GetTorqueResponse(worksheet, rowIndex),
                DataType = GetDataType(worksheet, rowIndex),
                Field = GetField(worksheet, rowIndex),
                RelevantForHash = GetRelevantForHash(worksheet, rowIndex),
                UIName = GetUIName(worksheet, rowIndex)
            };
        return null;
    }

    private ParameterDto GenerateTorqueResponseParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoTorqueResponseData, GetTorqueResponse, _response, _dynamicTorque, worksheet,
            rowIndex);
    }

    private ParameterDto GenerateTorqueRequestParameter(Worksheet worksheet, int rowIndex)
    {
        return GenerateParameters(IsNoTorqueRequestData, GetTorqueRequest, _request, _dynamicTorque, worksheet,
            rowIndex);
    }

    #endregion

    #region Retrive values

    private string GetCommonRequest(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.ParametersForAllCalculationModules()] as Range).Value
               ?.ToString().Trim() ??
               string.Empty;
    }

    private string GetFlatRequest(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.FlatRequest()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetFlatResponse(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.FlatResponse()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetRoundRequest(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.RoundRequest()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetRoundResponse(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.RoundResponse()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetTorqueRequest(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.TorqueRequest()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetTorqueResponse(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.TorqueResponse()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetField(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.Field()] as Range).Value?.ToString().Trim() ?? string.Empty;
    }

    private string GetUIName(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.UIName()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetDataType(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.DataType()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetUnit(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.Unit()] as Range).Value?.ToString().Trim() ?? string.Empty;
    }

    private bool GetMandatoryParameter(Worksheet worksheet, int rowIndex)
    {
        string content = (worksheet.Cells[rowIndex, ColumnIndexes.MandatoryParameter()] as Range).Value?.ToString()
            .Trim();
        return content != null && content.Equals(Mandatory, StringComparison.InvariantCultureIgnoreCase);
    }

    private string GetNameParameter(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.Name()] as Range).Value?.ToString().Trim() ?? string.Empty;
    }

    private bool GetMandatoryValue(Worksheet worksheet, int rowIndex)
    {
        string content = (worksheet.Cells[rowIndex, ColumnIndexes.MandatoryValue()] as Range).Value?.ToString()
            .Trim();
        return content != null && content.Equals(Mandatory, StringComparison.InvariantCultureIgnoreCase);
    }

    private bool GetRelevantForHash(Worksheet worksheet, int rowIndex)
    {
        string content =
            (worksheet.Cells[rowIndex, ColumnIndexes.RelevantForHash()] as Range).Value?.ToString().Trim() ??
            string.Empty;
        return !content.IsNullOrEmpty();
    }

    private string GetDescriptionEn(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.DescriptionEn()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetVariableName(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.VariableName()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetVariableNameParametersForAllCalculationModules(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.ParametersForAllCalculationModules()] as Range).Value
               ?.ToString().Trim() ??
               string.Empty;
    }

    private string GetParentEntity(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.ParentEntity()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetExampleFlatDoubleCadge(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.ExampleExampleFlatDoubleCadgeRoundDoubleCadge()] as Range).Value
               ?.ToString().Trim() ??
               string.Empty;
    }

    private string GetExampleRoundDoubleCadge(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.ExampleRoundDoubleCadge()] as Range).Value?.ToString().Trim() ??
               string.Empty;
    }

    private string GetExampleFlatRotorSingleCadge(Worksheet worksheet, int rowIndex)
    {
        return (worksheet.Cells[rowIndex, ColumnIndexes.ExampleFlatRotorSingleCadge()] as Range).Value?.ToString()
               .Trim() ??
               string.Empty;
    }

    #endregion
}