namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Races.Languages;

    public class Halfling : RaceBase
    {
        /// <summary>
        /// Sets the display name for the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Halfling";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier>
            {
                new AbilityModifier(typeof(Strength), -2),
                new AbilityModifier(typeof(Dexterity), 2),
                new AbilityModifier(typeof(Charisma), 2)
            };
        }

        /// <summary>
        /// Populates a list containing known languages for the specific character race
        /// </summary>
        protected override void SetKnownLanguages()
        {
            KnownLanguages = new List<Language>
            {
                new Common(),
                new Languages.Halfling()
            };
        }

        /// <summary>
        /// Populates a list containing learnable languages for the specific character race
        /// </summary>
        protected override void SetLearnableLanguages()
        {
            LearnableLanguages = new List<Language>
            {
                new Languages.Dwarven(),
                new Languages.Elven(),
                new Languages.Gnome(),
                new Languages.Goblin()
            };
        }
    }
}
