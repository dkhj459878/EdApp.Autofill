using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace EdApp.AutoFill.BL.Extensions
{
    public static class NullOrEmptyExtensions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrEmpty<TEntity>(this IEnumerable<TEntity> enumerable)
        {
            if (enumerable is null) return true;

            return !enumerable.Any();
        }

        public static bool IsNullOrEmpty<TEntity>(this ICollection<TEntity> enumerable)
        {
            if (enumerable is null) return true;

            return !enumerable.Any();
        }
    }
}