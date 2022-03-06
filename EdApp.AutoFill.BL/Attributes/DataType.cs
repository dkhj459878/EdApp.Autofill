using System;

namespace EdApp.AutoFill.BL.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DataTypeAttribute : Attribute
{
    private readonly string _value;

    public DataTypeAttribute(string value = null)
    {
        _value = value;
    }

    public virtual string Value => _value;
}