namespace PathfinderRPG.Entities.Attributes
{
    public class HitDie : AttributeBase
    {
        /// <summary>
        /// Gets the attribute's value
        /// </summary>
        public new Dice.DieType Value
        {
            get { return Value; }
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Level" /> class
        /// </summary>
        /// <param name="value">The attribute's value</param>
        public HitDie(Dice.DieType value)
        {
            Initialise((int)value);
        }

        /// <summary>
        /// Sets the display name of the character attribute
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Hit Die";
        }
    }
}
