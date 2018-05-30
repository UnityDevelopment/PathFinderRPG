namespace PathfinderRPG.Entities.Classes
{
    using System;

    public abstract class ClassBase : EntityBase, IComparable<ClassBase>
    {
        protected string _displayName;
        protected  Dice.DieType _hitDie;

        /// <summary>
        /// Initialises a new instance of the <see cref="ClassBase" /> class
        /// </summary>
        protected ClassBase()
        {
            Initialise();
        }

        /// <summary>
        /// Gets or sets the class's display name
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            protected set { _displayName = value; }
        }

        /// <summary>
        /// Gets or sets the class's hit die
        /// </summary>
        public Dice.DieType HitDie
        {
            get { return _hitDie; }
            protected set { _hitDie = value; }
        }

        /// <summary>
        /// Compares the class to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other class</param>
        /// <returns>An integer used for determining sort order</returns>
        public int CompareTo(ClassBase other)
        {
            return string.CompareOrdinal(DisplayName, other.DisplayName);
        }

        /// <summary>
        /// Sets the display name of the character class
        /// </summary>
        protected abstract void SetDisplayName();

        /// <summary>
        /// Sets the hit die of the character class
        /// </summary>
        protected abstract void SetHitDie();

        /// <summary>
        /// Initialises the instance of the object
        /// </summary>
        private void Initialise()
        {
            SetDisplayName();
            SetHitDie();
        }
    }
}