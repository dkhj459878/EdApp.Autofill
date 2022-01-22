using System.Collections.Generic;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    /// <summary>
    ///     Contains calculation types: for example: WindingDesignRoundWire,
    ///     or WindingDesignRoundWire and etc.
    /// </summary>
    public class CalculationTypeDto : IIdentifier
    {
        /// <summary>
        ///     Type name. For example: WindingDesignRoundWire or WindingDesignRoundWire, etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Related parameters.
        /// </summary>
        public ICollection<ParameterDto> Parameters { get; set; }

        /// <summary>
        ///     Identifier.
        /// </summary>
        public int Id { get; set; }
    }
}