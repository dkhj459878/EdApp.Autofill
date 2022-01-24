﻿using System.Collections.Generic;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.DAL.Model
{
    /// <summary>
    /// Contains information about attributes gotten from Siemens.
    /// </summary>
    public class AttributesForSimocalc : ModelBase<AttributesForSimocalc>, IIdentifier
    {
        protected bool Equals(AttributesForSimocalc other)
        {
            return CalculationTypeId == other.CalculationTypeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AttributesForSimocalc) obj);
        }

        public override int GetHashCode()
        {
            return CalculationTypeId;
        }

        /// <summary>
        /// AttributesForSimocalc identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Deserialized collection of the attribute in a model representation.
        /// </summary>
        public ICollection<Attribute> Attributes { get; set; }

        /// <summary>
        /// Calculation type identifier: DesignWindingFlatWire and so on.
        /// </summary>
        public int CalculationTypeId { get; set; }

        /// <summary>
        /// Calculation type: DesignWindingFlatWire and so on.
        /// </summary>
        public CalculationType CalculationType { get; set; }

        public static bool operator ==(AttributesForSimocalc one, AttributesForSimocalc other)
        {
            if (one is null)
            {
                return false;
            }
            return one.Equals((object)other);
        }

        public static bool operator !=(AttributesForSimocalc one, AttributesForSimocalc other)
        {
            return !(one == other);
        }
    }
}