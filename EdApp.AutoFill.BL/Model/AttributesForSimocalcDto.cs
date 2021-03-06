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
        protected override bool Equals(AttributesForSimocalcDto other)
        {
            return CalculationTypeId == other.CalculationTypeId;
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
    }
}