using System.Collections.Generic;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.DAL.Model
{
    /// <summary>
    /// Contains model type, for example: Request or Response (or possibly Common).
    /// </summary>
    public class ModelTypeDto : IIdentifier
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type name (Request, Response or Common).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Related parameters.
        /// </summary>
        public ICollection<Parameter> Parameters { get; set; }
    }
}