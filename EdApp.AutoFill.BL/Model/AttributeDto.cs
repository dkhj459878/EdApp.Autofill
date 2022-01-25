using System;
using EdApp.AutoFill.BL.Contract;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    public class AttributeDto : ModelDtoBase<AttributeDto>, IIdentifier
    {
        protected override bool Equals(AttributeDto other)
        {
            return CalculationTypeId == other.CalculationTypeId && AttributeDtosForSimocalcId == other.AttributeDtosForSimocalcId && Name == other.Name && Value == other.Value && Unit == other.Unit && Description == other.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CalculationTypeId, AttributeDtosForSimocalcId, Name, Value, Unit, Description);
        }

        public override AttributeDto Clone()
        {
            // Make shadow copy.
            var cloning = base.Clone();

            if (CalculationType is not null)
            {
                cloning.CalculationType = CalculationType.Clone();
            }

            if (AttributeDtosForSimocalc is not null)
            {
                cloning.AttributeDtosForSimocalc = AttributeDtosForSimocalc.Clone();
            }
            return cloning;
        }

        public AttributeDto()
        {
        }

        public int CalculationTypeId { get; set; }

        public CalculationTypeDto CalculationType { get; set; }

        public int AttributeDtosForSimocalcId { get; set; }

        public AttributesForSimocalcDto AttributeDtosForSimocalc { get; set; }

        public AttributeDto(string name, string value, string unit = "")
        {
            Name = name; Value = value; Unit = unit;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    }
}