using System;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.DAL.Model
{
    public class AttributeDto : IIdentifier, ICloneable
    {
        public AttributeDto()
        {
        }

        public int CalculationTypeId { get; set; }

        public CalculationType CalculationType { get; set; }

        public int AttributesForSimocalcId { get; set; }

        public AttributesForSimocalc AttributesForSimocalc { get; set; }

        public AttributeDto(string name, string value, string unit = "")
        {
            Name = name; Value = value; Unit = unit; 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}