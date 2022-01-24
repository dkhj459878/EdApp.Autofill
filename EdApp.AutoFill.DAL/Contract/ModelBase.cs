using System;

namespace EdApp.AutoFill.DAL.Contract
{
    public abstract class ModelBase<T> : ICloneable<T>, ICloneable where T : class, ICloneable<T>, ICloneable, new()
    {
        public virtual T Clone()
        {
            return (T) MemberwiseClone();
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}