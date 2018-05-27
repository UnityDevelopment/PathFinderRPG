namespace PathfinderRPG.Entities.Abilities
{
    public class Charisma : AbilityBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Charisma" /> class
        /// </summary>
        /// <param name="score">The ability's score</param>
        public Charisma(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected override void SetDisplayOrder()
        {
            DisplayOrder = 6;
        }

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Charisma";
        }
    }
}
