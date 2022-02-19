using System;
using System.Collections.Generic;
using System.Linq;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Extensions
{
    public static class AttibuteDtoExtensions
    {
        public static IReadOnlyCollection<AttributeDto> Subtract(this IReadOnlyCollection<AttributeDto> menuendCollection, IReadOnlyCollection<AttributeDto> substrahendCollection)
        {
            var common = menuendCollection.Where(x => !substrahendCollection.Any(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase)));
            return new List<AttributeDto>(common);
        }

        public static IReadOnlyCollection<AttributeDto> HasCommonWith(this IReadOnlyCollection<AttributeDto> one, IReadOnlyCollection<AttributeDto> someOther)
        {
            return new List<AttributeDto>(one.Where(x => someOther.Any(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase))));
        }

        public static IReadOnlyCollection<AttributeDto> HasSimilarWith(this IReadOnlyCollection<AttributeDto> one, IReadOnlyCollection<AttributeDto> theOther)
        {
            return new List<AttributeDto>(one.Where(x => theOther.Any(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase) && x.Value.Like(y.Value))));
        }

        public static IReadOnlyCollection<AttributeDto> HasEqualWith(this IReadOnlyCollection<AttributeDto> one, IReadOnlyCollection<AttributeDto> theOther)
        {
            return new List<AttributeDto>(one.Where(x => theOther.Any(y => y == x)));
        }
    }
}
