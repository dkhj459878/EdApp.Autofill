using System.Collections.Generic;
using EdApp.AutoFill.BL.Contract;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    /// <summary>
    /// Contains calculation types: for example: WindingDesignRoundWire,
    /// or WindingDesignRoundWire and etc.
    /// </summary>
    public class CalculationTypeDto : ModelDtoBase<CalculationTypeDto>, IIdentifier
    {
        protected bool Equals(CalculationTypeDto other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CalculationTypeDto) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type name. For example: WindingDesignRoundWire or WindingDesignRoundWire, etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Related parameters.
        /// </summary>
        public ICollection<ParameterDto> Parameters { get; set; }

        /// <summary>
        /// Attributes, gotten from Siemens.
        /// </summary>
        /// <returns></returns>
        public ICollection<AttributeDto> Attributes { get; set; }

        /// <summary>
        /// Attributes for simocalc, gotten from Siemens.
        /// </summary>
        /// <returns></returns>
        public ICollection<AttributesForSimocalcDto> AttributesForSimocalcs { get; set; }

        public static bool operator ==(CalculationTypeDto one, CalculationTypeDto other)
        {
            if (one is null)
            {
                return false;
            }
            return one.Equals((object)other);
        }

        public static bool operator !=(CalculationTypeDto one, CalculationTypeDto other)
        {
            return !(one == other);
        }
    }
}