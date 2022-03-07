using System.Collections.Generic;
using System.Linq;

namespace EdApp.AutoFill.BL.Extensions;

public static class ArrayExtensions
{
    public static bool IsLast<T>(this T element, IList<T> enumeration)
    {
        var count = enumeration.Count();
        return enumeration.IndexOf(element) == count - 1;
    }
}