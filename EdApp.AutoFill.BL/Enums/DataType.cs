using EdApp.AutoFill.BL.Attributes;

namespace EdApp.AutoFill.BL.Enums;

public enum DataType
{
    [DataType]
    None = 0,
    [DataType("string")]
    String = 1,
    [DataType("integer")]
    Integer = 2,
    [DataType("float")]
    Float = 3,
    [DataType("bool")]
    Bool = 4
}