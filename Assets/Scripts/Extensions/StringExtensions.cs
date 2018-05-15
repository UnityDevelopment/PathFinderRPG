namespace PathFinderRPG.Extensions
{
    public static class StringExtensions
    {
        public static string ToString(this int value, bool prefixPositiveSign)
        {
            string decoration = string.Empty;

            if (prefixPositiveSign)
            {
                if (value > 0)
                {
                    decoration = "+";
                }
            }

            return decoration + value.ToString();
        }
    }
}
