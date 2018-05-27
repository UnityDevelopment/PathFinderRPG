namespace PathfinderRPG.Entities.Races
{
    using System;
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;

    public abstract class RaceBase : ObjectBase, IComparable<RaceBase>
    {
        private string _displayName;
        private List<AbilityModifier> _abilityModifiers;

        /// <summary>
        /// Initialises a new instance of the <see cref="RaceBase" /> class
        /// </summary>
        protected RaceBase()
        {
            Initialise();
        }

        /// <summary>
        /// Gets or sets the display name of the race
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            protected set { _displayName = value; }
        }

        /// <summary>
        /// Gets or sets the ability modifiers of the race
        /// </summary>
        public List<AbilityModifier> AbilityModifiers
        {
            get { return _abilityModifiers; }
            protected set { _abilityModifiers = value; }
        }

        /// <summary>
        /// Compares the race to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other race</param>
        /// <returns>An integer used for determining sort order</returns>
        public int CompareTo(RaceBase other)
        {
            return string.CompareOrdinal(DisplayName, other.DisplayName);
        }

        /// <summary>
        /// Returns an ability modifier for the specified type and returns its modifier
        /// </summary>
        /// <typeparam name="T">The type of ability</typeparam>
        /// <returns>An integer value representing the ability modifier for the specified type of ability.  Returns 0 if the type of ability modifier was not found.</returns>
        public int GetModifier<T>()
        {
            int modifier;
            AbilityModifier abilityModifier = FindModifier<T>();

            if (abilityModifier != null)
            {
                modifier = abilityModifier.Modifier;
            }
            else
            {
                modifier = 0;
            }

            return modifier;
        }

        /// <summary>
        /// Sets the racial ability modifiers of the character race
        /// </summary>
        protected abstract void SetRacialAbilityModifiers();

        /// <summary>
        /// Sets the display name of the character race
        /// </summary>
        protected abstract void SetDisplayName();

        /// <summary>
        /// Initialises the instance of the object
        /// </summary>
        private void Initialise()
        {
            SetDisplayName();
            SetRacialAbilityModifiers();
        }

        /// <summary>
        /// Finds and returns an ability modifier of the specified type
        /// </summary>
        /// <typeparam name="T">The type of ability modifier</typeparam>
        /// <returns>An ability modifier of the specified type.  Returns null if the specified type is not found.</returns>
        private AbilityModifier FindModifier<T>()
        {
            return _abilityModifiers.Find(delegate(AbilityModifier abilityModifier) { return abilityModifier.AbilityType == typeof(T); });
        }
    }
}
