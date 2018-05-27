namespace PathfinderRPG.Entities.Abilities
{
    public class Wisdom : AbilityBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Wisdom" /> class
        /// </summary>
        /// <param name="score">The ability's score</param>
        public Wisdom(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected override void SetDisplayOrder()
        {
            DisplayOrder = 5;
        }

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Wisdom";
        }
    }
}
