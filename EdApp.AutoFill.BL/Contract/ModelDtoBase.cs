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

        protected abstract bool Equals(T other);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ModelDtoBase<T> one, ModelDtoBase<T> other)
        {
            if (ReferenceEquals(one, other))
            {
                return true;
            }

            if (one is null)
            {
                return false;
            }
            return one.Equals(other);
        }

        public static bool operator !=(ModelDtoBase<T> one, ModelDtoBase<T> other)
        {
            return !(one == other);
        }
    }
}