namespace PathfinderRPG.Entities.Abilities
{
    public class Intelligence : AbilityBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Intelligence" /> class
        /// </summary>
        /// <param name="score">The ability's score</param>
        public Intelligence(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected override void SetDisplayOrder()
        {
            DisplayOrder = 4;
        }

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Intelligence";
        }
    }
}
