namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;

    public class Elf : RaceBase 
    {
        /// <summary>
        /// Sets the friendly name of the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Elf";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetRacialAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier>
            {
                new AbilityModifier(typeof(Dexterity), 2),
                new AbilityModifier(typeof(Constitution), -2),
                new AbilityModifier(typeof(Intelligence), 2)
            };
        }
    }
}
