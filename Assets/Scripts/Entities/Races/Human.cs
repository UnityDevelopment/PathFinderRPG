namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;

    public class Human : RaceBase 
    {
        /// <summary>
        /// Sets the friendly name for the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Human";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetRacialAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier> { };
        }
    }
}
