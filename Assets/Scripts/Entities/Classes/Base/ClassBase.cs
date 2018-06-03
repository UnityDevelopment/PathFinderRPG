namespace PathfinderRPG.Entities.Classes
{
    using System;

    public abstract class ClassBase : EntityBase, IComparable<ClassBase>
    {
        protected string _displayName;
        protected int _level;
        protected  Dice.DieType _hitDie;
        
         /// <summary>
        /// Initialises a new instance of the <see cref="ClassBase" /> class for the specified level
        /// </summary>
        protected ClassBase(int level)
        {
            Initialise(level);
        }

        /// <summary>
        /// Prevents a default instance of the<see cref="ClassBase" /> class from being created
        /// </summary>
        protected ClassBase()
        {
            // TODO: Hard coded initial level
            Initialise(1);
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
        /// Gets or sets the class's level
        /// </summary>
        public int Level
        {
            get { return _level; }
            private set { _level = value; }
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
        /// <param name="level">The level of the class</param>
        private void Initialise(int level)
        {
            Level = level;

            SetDisplayName();
            SetHitDie();
        }
    }
}