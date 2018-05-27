namespace PathfinderRPG.Entities.Attributes
{
    public class Experience : AttributeBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Level" /> class
        /// </summary>
        /// <param name="value">The attribute's value</param>
        public Experience(int value)
        {
            Initialise(value);
        }

        /// <summary>
        /// Sets the display name of the character attribute
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Experience";
        }
    }
}
