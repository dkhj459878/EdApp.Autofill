using System;
using System.Collections.Generic;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.DAL.Model
{
    /// <summary>
    /// Contains calculation types: for example: WindingDesignRoundWire,
    /// or WindingDesignRoundWire and etc.
    /// </summary>
    public class CalculationType : IIdentifier, ICloneable
    {
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

        public object Clone()
        {
            return new CalculationType() {Id = Id, Name = Name};
        }
    }
}
