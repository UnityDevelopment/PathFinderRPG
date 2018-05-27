namespace PathfinderRPG.Entities.Abilities
{
    using System;

    /// <summary>
    /// Provides common functionality to all derived ability classes
    /// </summary>
    public abstract class AbilityBase : ObjectBase, IComparable<AbilityBase>
    {
        private int _displayOrder;
        private string _displayName;
        private int _score;
        private int _modifier;

        /// <summary>
        /// Initialises a new instance of the <see cref="AbilityBase" /> class
        /// </summary>
        protected AbilityBase()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AbilityBase" /> class and sets its score value to <paramref name="score"/>
        /// </summary>
        /// <param name="score">The ability's score</param>
        protected AbilityBase(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Gets or sets the ability's display order
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            protected set { _displayOrder = value; }
        }

        /// <summary>
        /// Gets or sets the ability's display name
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            protected set { _displayName = value; }
        }

        /// <summary>
        /// Gets or sets the ability's score
        /// </summary>
        public int Score
        {
            get { return _score; }
            protected set { _score = value; }
        }

        /// <summary>
        /// Gets or sets the ability's modifier
        /// </summary>
        public int Modifier
        {
            get { return _modifier; }
            protected set { _modifier = value; }
        }

        /// <summary>
        /// Returns the ability modifier for the specified score and racial modifier
        /// </summary>
        /// <param name="abilityScore">The ability's score</param>
        /// <param name="raceAbilityModifier">The ability's racial modifier</param>
        /// <returns>The calculated ability modifier based upon the specified ability score and racial ability modifier</returns>
        public static int CalculateAbilityModifier(int abilityScore, int raceAbilityModifier)
        {
            return CalculateModifier(abilityScore + raceAbilityModifier);
        }

        /// <summary>
        /// Compares the ability to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other ability</param>
        /// <returns>An integer used for determining sort order</returns>
        public int CompareTo(AbilityBase other)
        {
            return DisplayOrder.CompareTo(other.DisplayOrder);
        }

        /// <summary>
        /// Initialises the instance of the object
        /// </summary>
        /// <param name="score">The ability's score</param>
        protected void Initialise(int score)
        {
            _score = score;
            _modifier = CalculateModifier(_score);

            SetDisplayOrder();
            SetDisplayName();
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected abstract void SetDisplayOrder();

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected abstract void SetDisplayName();

        /// <summary>
        /// Calculates the ability's modifier
        /// </summary>
        /// <param name="score">The ability's score</param>
        /// <returns>The modifier for the ability based on its score</returns>
        private static int CalculateModifier(int score)
        {
            double modifier = (score - 10) / 2;

            return (int)Math.Floor(modifier);
        }
    }
}
