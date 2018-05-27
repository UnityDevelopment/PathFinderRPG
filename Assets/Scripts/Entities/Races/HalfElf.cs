namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;

    public class HalfElf : RaceBase 
    {
        /// <summary>
        /// Sets the friendly name for the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Half-Elf";
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
