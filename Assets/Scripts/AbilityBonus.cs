using UnityEngine;

namespace PathFinderRPG
{
    public class AbilityBonus : ScriptableObject
    {
        private CharacterAbility _ability;
        private int _value;

        /// <summary>
        /// Initialises the ability bonuses
        /// </summary>
        /// <param name="ability">The ability</param>
        /// <param name="value">The bonus value</param>
        private void Init(CharacterAbility ability, int value)
        {
            _ability = ability;
            _value = value;
        }

        /// <summary>
        /// Creates and instance of the AbilityBonus
        /// </summary>
        /// <param name="ability">The ability</param>
        /// <param name="value">The bonus value</param>
        /// <returns>AbilityBonus</returns>
        public static AbilityBonus CreateInstance(CharacterAbility ability, int value)
        {
            AbilityBonus abilityBonus = ScriptableObject.CreateInstance<AbilityBonus>();

            abilityBonus.Init(ability, value);

            return abilityBonus;
        }

        /// <summary>
        /// Returns the CharacterAbility of the AbilityBonus
        /// </summary>
        public CharacterAbility Ability
        {
            get { return _ability; }
        }

        /// <summary>
        /// Returns the value of the AbilityBonus
        /// </summary>
        public int Value
        {
            get { return _value; }
        }

    }
}
