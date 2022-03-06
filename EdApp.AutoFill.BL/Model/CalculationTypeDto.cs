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
        protected override bool Equals(CalculationTypeDto other)
        {
            return Name == other.Name;
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
        /// DataType name. For example: WindingDesignRoundWire or WindingDesignRoundWire, etc.
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
    }
}