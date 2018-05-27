namespace PathfinderRPG.Entities.Abilities
{
    public class Strength : AbilityBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Strength" /> class
        /// </summary>
        /// <param name="score">The ability's score</param>
        public Strength(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected override void SetDisplayOrder()
        {
            DisplayOrder = 1;
        }

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Strength";
        }
    }
}
