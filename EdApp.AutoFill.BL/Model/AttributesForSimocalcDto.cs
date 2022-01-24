using System.Collections.Generic;
using EdApp.AutoFill.BL.Contract;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    /// <summary>
    /// Contains information about attributes gotten from Siemens.
    /// </summary>
    public class AttributesForSimocalcDto : ModelDtoBase<AttributesForSimocalcDto>, IIdentifier
    {
        protected bool Equals(AttributesForSimocalcDto other)
        {
            return CalculationTypeId == other.CalculationTypeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AttributesForSimocalcDto) obj);
        }

        public override int GetHashCode()
        {
            return CalculationTypeId;
        }

        /// <summary>
        /// AttributesForSimocalcDto identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Deserialized collection of the attribute in a model representation.
        /// </summary>
        public ICollection<AttributeDto> Attributes { get; set; }

        /// <summary>
        /// Calculation type identifier: DesignWindingFlatWire and so on.
        /// </summary>
        public int CalculationTypeId { get; set; }

        /// <summary>
        /// Calculation type: DesignWindingFlatWire and so on.
        /// </summary>
        public CalculationTypeDto CalculationType { get; set; }

        public static bool operator ==(AttributesForSimocalcDto one, AttributesForSimocalcDto other)
        {
            if (one is null)
            {
                return false;
            }
            return one.Equals((object)other);
        }

        public static bool operator !=(AttributesForSimocalcDto one, AttributesForSimocalcDto other)
        {
            return !(one == other);
        }
    }
}