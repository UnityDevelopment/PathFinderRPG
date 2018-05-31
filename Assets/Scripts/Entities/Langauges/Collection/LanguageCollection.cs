namespace PathfinderRPG.Entities.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using PathfinderRPG.Entities.Races.Languages;
    using PathfinderRPG.Utilities;

    public static class LanguageCollection
    {
        private static List<Language> _cachedLanguages;

        /// <summary>
        /// Initialises static members of the <see cref="LanguageCollection" /> class
        /// </summary>
        static LanguageCollection()
        {
            CacheLanguages();
        }

        /// <summary>
        /// Returns the display names of the languages
        /// </summary>
        /// <returns>A list of language display names</returns>
        public static List<string> GetDisplayNames()
        {
            List<string> names = new List<string>();

            foreach (Language language in _cachedLanguages)
            {
                names.Add(language.DisplayName);
            }

            return names;
        }

        /// <summary>
        /// Returns all of the languages in the collection
        /// </summary>
        /// <returns>A list of languages</returns>
        public static List<Language> GetLanguages()
        {
            return _cachedLanguages;
        }

        /// <summary>
        /// Searches for a language within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name to find</param>
        /// <returns>Returns either the language if found, or null</returns>
        public static Language FindLanguage(string displayName)
        {
            return _cachedLanguages.Find(delegate(Language language) { return language.DisplayName.ToUpper() == displayName.ToUpper(); });
        }

        /// <summary>
        /// Caches the languages
        /// </summary>
        private static void CacheLanguages()
        {
            if (_cachedLanguages == null || _cachedLanguages.Count == 0)
            {
                _cachedLanguages = ReflectiveEnumerator.GetEnumerableOfType<Language>().ToList();
            }
        }
    }
}
