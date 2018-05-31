namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Races.Languages;

    public class Elf : RaceBase 
    {
        /// <summary>
        /// Sets the display name of the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Elf";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier>
            {
                new AbilityModifier(typeof(Dexterity), 2),
                new AbilityModifier(typeof(Constitution), -2),
                new AbilityModifier(typeof(Intelligence), 2)
            };
        }

        /// <summary>
        /// Populates a list containing known languages for the specific character race
        /// </summary>
        protected override void SetKnownLanguages()
        {
            KnownLanguages = new List<Language>
            {
                new Languages.Common(),
                new Languages.Elven()
            };
        }

        /// <summary>
        /// Populates a list containing learnable languages for the specific character race
        /// </summary>
        protected override void SetLearnableLanguages()
        {
            LearnableLanguages = new List<Language>
            {
                new Languages.Celestial(),
                new Languages.Draconic(),
                new Languages.Gnoll(),
                new Languages.Gnome(),
                new Languages.Goblin(),
                new Languages.Orc(),
                new Languages.Sylvan()
            };
        }
    }
}
