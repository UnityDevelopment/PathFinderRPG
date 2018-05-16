namespace PathFinderRPG.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Provides a positive sign prefix before integers
        /// </summary>
        /// <param name="value">The integer</param>
        /// <param name="prefixPositiveSign">Whether to apply the prefix or not</param>
        /// <returns></returns>
        public static string ToString(this int value, bool prefixPositiveSign)
        {
            string prefix = string.Empty;

            if (prefixPositiveSign)
            {
                if (value > 0)
                {
                    prefix = "+";
                }
            }

            return prefix + value.ToString();
        }
    }
}
