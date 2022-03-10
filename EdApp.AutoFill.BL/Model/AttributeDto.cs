using System;
using System.Collections.Generic;
using EdApp.AutoFill.BL.Contract;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    public class AttributeDto : ModelDtoBase<AttributeDto>, IIdentifier
    {
        protected override bool Equals(AttributeDto other)
        {
            return IsEqual(Name, other.Name) && IsEqual(Value, other.Value) && IsEqual(Unit,  other.Unit);
        }

        private bool IsEqual(string one, string other)
        {
            if (one == null && other == null) return true;
            if (string.IsNullOrEmpty(one) && string.IsNullOrEmpty(other))
            {
                return true;
            }

            return one.Equals(other, StringComparison.OrdinalIgnoreCase);
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

        public override string ToString()
        {
            return $"Name: {Name}, Value: {Value}, Unit: {Unit}";
        }
    }
}