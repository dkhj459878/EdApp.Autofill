using System;
using System.Reflection;
using EdApp.AutoFill.BL.Attributes;
using EdApp.AutoFill.BL.Enums;

namespace EdApp.AutoFill.BL.Extensions;

public static class DataTypeExtensions
{
    /// <summary>
    /// Return enum value for data type.
    /// </summary>
    /// <returns>Data type value string</returns>
    public static string GetDataTypeValue(this Enum enumVal)
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attribute = memInfo[0].GetCustomAttribute<DataTypeAttribute>();
        return attribute is not null ? attribute.Value : enumVal.ToString();
    }

    public static DataType GetDataType(this string dataTypeAsString)
    {
        const string stringType = "string";
        const string integerType = "int";
        const string floatType = "float";
        const string boolType = "bool";
        return dataTypeAsString switch
        {
            stringType => DataType.String,
            integerType => DataType.Integer,
            floatType => DataType.Float,
            boolType => DataType.Bool,
            _ => throw new ArgumentOutOfRangeException(nameof(dataTypeAsString), dataTypeAsString, "There is not corresponding DataType value for this string")
        };
    }
}