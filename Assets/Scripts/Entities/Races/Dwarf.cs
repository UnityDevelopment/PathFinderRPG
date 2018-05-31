namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Races.Languages;

    public class Dwarf : RaceBase
    {
        /// <summary>
        /// Sets the display name for the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Dwarf";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier>
            {
                new AbilityModifier(typeof(Constitution), 2),
                new AbilityModifier(typeof(Wisdom), 2),
                new AbilityModifier(typeof(Charisma), -2)
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
                new Languages.Dwarven()
            };
        }

        /// <summary>
        /// Populates a list containing learnable languages for the specific character race
        /// </summary>
        protected override void SetLearnableLanguages()
        {
            LearnableLanguages = new List<Language>
            {
                new Languages.Giant(),
                new Languages.Gnome(),
                new Languages.Goblin(),
                new Languages.Orc(),
                new Languages.Terran(),
                new Languages.Undercommon()
            };
        }
    }
}
