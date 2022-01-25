using System.Collections.Generic;
using EdApp.AutoFill.BL.Contract;
using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    /// <summary>
    /// Contains model type, for example: Request or Response (or possibly Common).
    /// </summary>
    public class ModelTypeDto : ModelDtoBase<ModelTypeDto>, IIdentifier
    {
        protected override bool Equals(ModelTypeDto other)
        {
            return Name == other.Name;
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
        /// Type name (Request, Response or Common).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Related parameters.
        /// </summary>
        public ICollection<ModelTypeDto> Parameters { get; set; }
    }
}