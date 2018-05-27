namespace PathfinderRPG.Entities.Abilities
{
    public class Constitution : AbilityBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Constitution" /> class
        /// </summary>
        /// <param name="score">The ability's score</param>
        public Constitution(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected override void SetDisplayOrder()
        {
            DisplayOrder = 3;
        }

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Constitution";
        }
    }
}
