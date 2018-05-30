namespace PathfinderRPG.Entities.Attributes
{
    using System;

    /// <summary>
    /// Provides common functionality to all derived attribute classes
    /// </summary>
    public abstract class AttributeBase : EntityBase, IComparable<AttributeBase>
    {
        private string _displayName;
        private int _value;

        /// <summary>
        /// Initialises a new instance of the <see cref="AttributeBase" /> class
        /// </summary>
        protected AttributeBase()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AttributeBase" /> class and sets its value to <paramref name="value"/>
        /// </summary>
        /// <param name="score">The ability's value</param>
        protected AttributeBase(int value)
        {
            Initialise(value);
        }

        /// <summary>
        /// Gets or sets the attribute's display name
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            protected set { _displayName = value; }
        }

        /// <summary>
        /// Gets or sets the attribute's value
        /// </summary>
        public int Value
        {
            get { return _value; }
            protected set { _value = value; }
        }

        /// <summary>
        /// Compares the attribute to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other attribute</param>
        /// <returns>An integer used for determining sort order</returns>
        public int CompareTo(AttributeBase other)
        {
            return string.CompareOrdinal(DisplayName, other.DisplayName);
        }

        /// <summary>
        /// Initialises the instance of the object
        /// </summary>
        /// <param name="value">The attribute's vallue</param>
        protected void Initialise(int value)
        {
            _value = value;

            SetDisplayName();
        }

        /// <summary>
        /// Sets the display name of the character attribute
        /// </summary>
        protected abstract void SetDisplayName();
    }
}
