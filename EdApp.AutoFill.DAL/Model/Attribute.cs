using System;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.DAL.Model
{
    public class Attribute : ModelBase<Attribute>, IIdentifier
    {
        protected override bool Equals(Attribute other)
        {
            return CalculationTypeId == other.CalculationTypeId && AttributesForSimocalcId == other.AttributesForSimocalcId && Name == other.Name && Value == other.Value && Unit == other.Unit && Description == other.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CalculationTypeId, AttributesForSimocalcId, Name, Value, Unit, Description);
        }

        public Attribute()
        {
        }

        public override Attribute Clone()
        {
            return base.Clone();
        }

        public int CalculationTypeId { get; set; }

        public CalculationType CalculationType { get; set; }

        public int AttributesForSimocalcId { get; set; }

        public AttributesForSimocalc AttributesForSimocalc { get; set; }

        public Attribute(string name, string value, string unit = "")
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