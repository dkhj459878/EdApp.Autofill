using System;

namespace EdApp.AutoFill.DAL.Contract
{
    public interface ICloneable<out T> where T : class, ICloneable
    {
        T Clone();
    }
}