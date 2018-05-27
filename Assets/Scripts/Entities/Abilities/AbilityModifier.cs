namespace PathfinderRPG.Entities.Abilities
{
    using System;

    public class AbilityModifier
    {
        // TODO: Reconsider the use of "Types" for the modifiers
        private Type _abilityType;
        private int _modifier;

        /// <summary>
        /// Initialises a new instance of the <see cref="AbilityModifier" /> class
        /// </summary>
        /// <param name="abilityType">The character ability type</param>
        /// <param name="modifier">The modifier</param>
        public AbilityModifier(Type abilityType, int modifier)
        {
            this._abilityType = abilityType;
            this._modifier = modifier;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AbilityModifier" /> class from being created
        /// </summary>
        private AbilityModifier()
        {
        }

        /// <summary>
        /// Gets the ability modifier's character ability type
        /// </summary>
        public Type AbilityType
        {
            get { return _abilityType; }
        }

        /// <summary>
        /// Gets the ability modifier's modifier
        /// </summary>
        public int Modifier
        {
            get { return _modifier; }
        }
    }
}
