namespace PathfinderRPG.Entities.Abilities
{
    public class Dexterity : AbilityBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Dexterity" /> class
        /// </summary>
        /// <param name="score">The ability's score</param>
        public Dexterity(int score)
        {
            Initialise(score);
        }

        /// <summary>
        /// Sets the display order of the character ability
        /// </summary>
        protected override void SetDisplayOrder()
        {
            DisplayOrder = 2;
        }

        /// <summary>
        /// Sets the display name of the character ability
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Dexterity";
        }
    }
}
