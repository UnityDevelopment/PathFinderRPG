namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;

    public class Gnome : RaceBase 
    {
        /// <summary>
        /// Sets the friendly name for the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Gnome";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetRacialAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier>
            {
                new AbilityModifier(typeof(Strength), -2),
                new AbilityModifier(typeof(Constitution), 2),
                new AbilityModifier(typeof(Charisma), 2)
            };
        }
    }
}
