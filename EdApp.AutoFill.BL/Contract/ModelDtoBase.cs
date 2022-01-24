using System;

namespace EdApp.AutoFill.BL.Contract
{
    public abstract class ModelDtoBase<T> : ICloneable<T>, ICloneable where T : class, ICloneable<T>, ICloneable, new()
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