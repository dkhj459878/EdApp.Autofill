using System;

namespace EdApp.AutoFill.BL.Contract
{
    public interface ICloneable<out T> where T : class, ICloneable
    {
        T Clone();
    }
}