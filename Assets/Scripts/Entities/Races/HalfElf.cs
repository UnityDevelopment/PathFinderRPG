namespace PathfinderRPG.Entities.Races
{
    using System.Collections.Generic;

    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Races.Languages;

    public class HalfElf : RaceBase 
    {
        /// <summary>
        /// Sets the display name for the specific character race
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Half-Elf";
        }

        /// <summary>
        /// Populates a list containing ability modifiers for the specific character race
        /// </summary>
        protected override void SetAbilityModifiers()
        {
            AbilityModifiers = new List<AbilityModifier> { };
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
            List<Language> learnableLanguages = new List<Language>(LanguageCollection.GetLanguages());

            foreach (Language knownLanguage in KnownLanguages)
            {
                learnableLanguages.Remove(knownLanguage);
            }

            LearnableLanguages = learnableLanguages;
        }
    }
}
