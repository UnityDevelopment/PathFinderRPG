namespace PathfinderRPG.Entities.Attributes
{
    using System.Collections.Generic;
    using System.Linq;

    using PathfinderRPG.Utilities;

    public static class AttributeCollection
    {
        private static List<AttributeBase> _cachedAttributes;

        /// <summary>
        /// Initialises static members of the <see cref="AttributeCollection" /> class
        /// </summary>
        static AttributeCollection()
        {
            CacheAttributes();
        }

        /// <summary>
        /// Returns the display names of the character attributes
        /// </summary>
        /// <returns>A list of attribute display names</returns>
        public static List<string> GetDisplayNames()
        {
            List<string> names = new List<string>();

            foreach (AttributeBase characterAttribute in _cachedAttributes)
            {
                names.Add(characterAttribute.DisplayName);
            }

            return names;
        }

        /// <summary>
        /// Searches for an attribute within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name to find</param>
        /// <returns>Returns either the attribute if found, or null</returns>
        public static AttributeBase FindAttribute(string displayName)
        {
            return _cachedAttributes.Find(delegate(AttributeBase characterAttribute) { return characterAttribute.DisplayName.ToUpper() == displayName.ToUpper(); });
        }

        /// <summary>
        /// Caches the character attributes
        /// </summary>
        private static void CacheAttributes()
        {
            if (_cachedAttributes == null || _cachedAttributes.Count == 0)
            {
                _cachedAttributes = ReflectiveEnumerator.GetEnumerableOfType<AttributeBase>(0).ToList();
            }
        }
    }
}
