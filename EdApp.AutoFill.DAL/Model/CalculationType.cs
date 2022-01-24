﻿using System.Collections.Generic;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.DAL.Model
{
    /// <summary>
    /// Contains calculation types: for example: WindingDesignRoundWire,
    /// or WindingDesignRoundWire and etc.
    /// </summary>
    public class CalculationType : ModelBase<CalculationType>, IIdentifier
    {
        protected bool Equals(CalculationType other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CalculationType) obj);
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
        public ICollection<Parameter> Parameters { get; set; }

        /// <summary>
        /// Attributes, gotten from Siemens.
        /// </summary>
        /// <returns></returns>
        public ICollection<Attribute> Attributes { get; set; }

        /// <summary>
        /// Attributes for simocalc, gotten from Siemens.
        /// </summary>
        /// <returns></returns>
        public ICollection<AttributesForSimocalc> AttributesForSimocalcs { get; set; }

        public static bool operator ==(CalculationType one, CalculationType other)
        {
            if (one is null)
            {
                return false;
            }
            return one.Equals((object)other);
        }

        public static bool operator !=(CalculationType one, CalculationType other)
        {
            return !(one == other);
        }
    }
}
