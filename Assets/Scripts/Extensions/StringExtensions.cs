namespace PathfinderRPG.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Provides a positive sign prefix before integers for values greater than 0
        /// </summary>
        /// <param name="value">The integer</param>
        /// <param name="prefixPositiveSign">Whether to apply the prefix or not</param>
        /// <returns>A string representation of the integer value, including a + prefix if the value is above 0</returns>
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
