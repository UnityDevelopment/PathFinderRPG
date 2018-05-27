namespace PathfinderRPG.Entities.Classes 
{
    using System.Collections.Generic;
    using System.Linq;

    using PathfinderRPG.Utilities;

    public static class ClassCollection
    {
        private static List<ClassBase> _cachedClasses;

        /// <summary>
        /// Initialises static members of the <see cref="ClassCollection" /> class
        /// </summary>
        static ClassCollection()
        {
            CacheClasses();
        }
        
        /// <summary>
        /// Returns the display names of the character classes
        /// </summary>
        /// <returns>A list of class display names</returns>
        public static List<string> GetDisplayNames()
        {
            List<string> names = new List<string>();

            foreach (ClassBase characterClass in _cachedClasses)
            {
                names.Add(characterClass.DisplayName);
            }

            return names;
        }

        /// <summary>
        /// Searches for a class within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name to find</param>
        /// <returns>Returns either the class if found, or null</returns>
        public static ClassBase FindClass(string displayName)
        {
            return _cachedClasses.Find(delegate(ClassBase characterClass) { return characterClass.DisplayName.ToUpper() == displayName.ToUpper(); });
        }

        /// <summary>
        /// Caches the character classes
        /// </summary>
        private static void CacheClasses()
        {
            if (_cachedClasses == null || _cachedClasses.Count == 0)
            {
                _cachedClasses = ReflectiveEnumerator.GetEnumerableOfType<ClassBase>().ToList();
            }
        }
    }
}
