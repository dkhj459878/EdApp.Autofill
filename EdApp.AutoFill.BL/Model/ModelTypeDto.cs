using System.Collections.Generic;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    /// <summary>
    ///     Contains model type, for example: Request or Response (or possibly Common).
    /// </summary>
    public class ModelTypeDto : IIdentifier
    {
        /// <summary>
        ///     Type name (Request, Response or Common).
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