using EdApp.AutoFill.BL.Enums;

namespace EdApp.AutoFill.BL.Model;

public readonly struct NodeValue
{
    public NodeValue(string value, DataType dataDataType )
    {
        Value = value;
        DataType = dataDataType;
    }

    /// <summary>
    ///     Data type. It means: int, float (any numeric), bool, string.
    ///     Used for correct writing attribute value in json notation.
    ///     It means either with double commas or not.
    /// </summary>
    public DataType DataType { get; }

    /// <summary>
    ///     Data value in string format.
    /// </summary>
    public string Value { get; }

    public string Print()
    {
        return DataType == DataType.String ? $"\"{Value}\"" : Value;
    }
}