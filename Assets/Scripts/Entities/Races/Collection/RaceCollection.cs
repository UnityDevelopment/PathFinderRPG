namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;
    using System.Linq;

    using PathfinderRPG.Utilities;

    public static class RaceCollection
    {
        private static List<RaceBase> _cachedRaces;

        /// <summary>
        /// Initialises static members of the <see cref="RaceCollection" /> class
        /// </summary>
        static RaceCollection()
        {
            CacheRaces();
        }

        /// <summary>
        /// Returns the display names of the character races
        /// </summary>
        /// <returns>A list of race display names</returns>
        public static List<string> GetDisplayNames()
        {
            List<string> names = new List<string>();

            foreach (RaceBase characterRace in _cachedRaces)
            {
                names.Add(characterRace.DisplayName);
            }

            return names;
        }

        /// <summary>
        /// Searches for a race within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name</param>
        /// <returns>A character race corresponding to the specified display name.  Returns null if a specific race is not found.</returns>
        public static RaceBase FindRace(string displayName)
        {
            return _cachedRaces.Find(delegate(RaceBase characterRace) { return characterRace.DisplayName.ToUpper() == displayName.ToUpper(); });
        }

        /// <summary>
        /// Caches the character races
        /// </summary>
        private static void CacheRaces()
        {
            if (_cachedRaces == null || _cachedRaces.Count == 0)
            { 
                _cachedRaces = ReflectiveEnumerator.GetEnumerableOfType<RaceBase>().ToList();
            }
        }
    }
}
