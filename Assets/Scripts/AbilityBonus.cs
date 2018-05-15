using UnityEngine;

namespace PathFinderRPG
{
    public class AbilityBonus : ScriptableObject
    {
        private CharacterAbility _ability;
        private int _value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="value"></param>
        private void Init(CharacterAbility ability, int value)
        {
            _ability = ability;
            _value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AbilityBonus CreateInstance(CharacterAbility ability, int value)
        {
            AbilityBonus abilityBonus = ScriptableObject.CreateInstance<AbilityBonus>();

            abilityBonus.Init(ability, value);

            return abilityBonus;
        }

        /// <summary>
        /// 
        /// </summary>
        public CharacterAbility Ability
        {
            get { return _ability; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get { return _value; }
        }

    }
}
