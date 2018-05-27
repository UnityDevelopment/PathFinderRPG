namespace PathfinderRPG.Entities.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using PathfinderRPG.Utilities;

    public static class AbilityCollection
    {
        private static List<AbilityBase> _cachedAbilities;

        /// <summary>
        /// Initialises static members of the <see cref="AbilityCollection" /> class
        /// </summary>
        static AbilityCollection()
        {
            CacheAbilities();
        }

        /// <summary>
        /// Returns the display names of the character abilities
        /// </summary>
        /// <returns>A list of ability display names</returns>
        public static List<string> GetDisplayNames()
        {
            List<string> names = new List<string>();

            foreach (AbilityBase characterAbility in _cachedAbilities)
            {
                names.Add(characterAbility.DisplayName);
            }

            return names;
        }

        /// <summary>
        /// Searches for an ability within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name to find</param>
        /// <returns>Returns either the ability if found, or null</returns>
        public static AbilityBase FindAbility(string displayName)
        {
            return _cachedAbilities.Find(delegate(AbilityBase characterAbility) { return characterAbility.DisplayName.ToUpper() == displayName.ToUpper(); });
        }

        /// <summary>
        /// Caches the character abilities
        /// </summary>
        private static void CacheAbilities()
        {
            if (_cachedAbilities == null || _cachedAbilities.Count == 0)
            {
                _cachedAbilities = ReflectiveEnumerator.GetEnumerableOfType<AbilityBase>(0).ToList();
            }
        }
    }
}
