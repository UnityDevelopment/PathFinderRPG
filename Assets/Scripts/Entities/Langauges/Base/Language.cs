namespace PathfinderRPG.Entities.Races.Languages
{
    using System;

    public abstract class Language : EntityBase, IComparable<Language>, IEquatable<Language>
    {
        private string _displayName;

        /// <summary>
        /// Initialises a new instance of the <see cref="Language" /> class
        /// </summary>
        protected Language()
        {
            Initialise();
        }

        /// <summary>
        /// Gets or sets the display name of the language
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            protected set { _displayName = value; }
        }

        /// <summary>
        ///  Determines whether the specified languages are equal
        /// </summary>
        /// <param name="language1">The first language to compare</param>
        /// <param name="language2">The first language to compare</param>
        /// <returns>true if the specified languages are equal; otherwise, false</returns>
        public static bool operator ==(Language language1, Language language2)
        {
            if (((object)language1) == null || ((object)language2) == null)
                return Object.Equals(language1, language2);

            return language1.Equals(language2);
        }

        /// <summary>
        /// Determines whether the specified languages are not equal
        /// </summary>
        /// <param name="language1">The first language to compare</param>
        /// <param name="language2">The first language to compare</param>
        /// <returns>true if the specified languages are not equal; otherwise, false</returns>
        public static bool operator !=(Language language1, Language language2)
        {
            if (((object)language1) == null || ((object)language2) == null)
                return !Object.Equals(language1, language2);

            return !(language1.Equals(language2));
        }

        /// <summary>
        /// Compares the language to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other language</param>
        /// <returns>An integer used for determining sort order</returns>
        public int CompareTo(Language other)
        {
            return DisplayName.CompareTo(other.DisplayName);
        }

        /// <summary>
        /// Determines whether the specified language is equal to the current language
        /// </summary>
        /// <param name="other">The language to compare with the current language</param>
        /// <returns>true if the specified language is equal to the current language; otherwise, false</returns>
        public bool Equals(Language other)
        {
            bool result;

            if (other == null)
            {
                result = false;
            }
            else
            {
                if (this.DisplayName.ToUpper() == other.DisplayName.ToUpper())
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other">The object to compare with the current object</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(Object obj)
        {
            bool result;

            if (obj == null)
            {
                result = false;
            }
            else
            {
                Language language = obj as Language;

                if (language == null)
                {
                    result = false;
                }
                else
                {
                    result = Equals(language);
                }
            }

            return result;
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return this.DisplayName.GetHashCode();
        }

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
        }
    }
}
