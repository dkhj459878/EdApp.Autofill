using System;

namespace EdApp.AutoFill.BL.Extensions
{
    public static class StringCleverComparison
    {
        public static bool Like(this string oneString, string other)
        {
            if (string.IsNullOrWhiteSpace(oneString) && string.IsNullOrWhiteSpace(other))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(oneString) || string.IsNullOrWhiteSpace(other))
            {
                return false;
            }

            return oneString.Equals(other, StringComparison.OrdinalIgnoreCase);
        }
    }
}
