namespace PathfinderRPG.Entities.Attributes
{
    public class Health : AttributeBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Health" /> class
        /// </summary>
        /// <param name="value">The attribute's value</param>
        public Health(int value)
        {
            Initialise(value);
        }

        /// <summary>
        /// Sets the display name of the character attribute
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Health";
        }
    }
}
