namespace PathfinderRPG
{
    using System;
    using System.Collections.Generic;

    using PathfinderRPG.Entities;
    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Classes;
    using PathfinderRPG.Entities.Races;
    using PathfinderRPG.Entities.Races.Languages;    

    /// <summary>
    /// Provides functionality to create a character
    /// </summary>
    public static class CharacterCreator
    {
        /// <summary>
        /// Rolls 4 D6, ignores the lowest value, returns the sum of the remaining dice
        /// </summary>
        /// <remarks>Used for character creation only</remarks>
        /// <returns>A random ability score based on a 4D6 dice roll, ignoring the lowest value</returns>
        public static int RollForAbilityScore()
        {
            // TODO: Remove hard coded value
            int[] results = Dice.Roll(Dice.DieType.D6, 4, Dice.SortOrder.Descending);

            int sumOfDiceRolls = 0;

            // TODO: Remove hard coded value
            for (int i = 0; i < 3; i++)
            {
                sumOfDiceRolls += results[i];
            }

            return sumOfDiceRolls;
        }

        /// <summary>
        /// Returns the display names for the character races
        /// </summary>
        /// <returns>A list of race display names</returns>
        public static List<string> GetRaceDisplayNames()
        {
            List<string> characterRaces = RaceCollection.GetDisplayNames();

            return characterRaces;
        }

        /// <summary>
        /// Returns the display names for the character classes
        /// </summary>
        /// <returns>A list of class display names</returns>
        public static List<string> GetClassDisplayNames()
        {
            List<string> characterClasses = ClassCollection.GetDisplayNames();

            return characterClasses;
        }

        /// <summary>
        /// Returns the display names for the character ability
        /// </summary>
        /// <returns>A list of ability display names</returns>
        public static List<string> GetAbilityDisplayNames()
        {
            List<string> characterAbilities = AbilityCollection.GetDisplayNames();

            return characterAbilities;
        }

        /// <summary>
        /// Searches for a race within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name</param>
        /// <returns>A character race corresponding to the specified display name.  Returns null if a specific race is not found.</returns>
        public static RaceBase FindCharacterRace(string displayName)
        {
            return RaceCollection.FindRace(displayName);
        }

        /// <summary>
        /// Searches for a class within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name</param>
        /// <param name="level">The level of the character</param>
        /// <returns>A character class corresponding to the specified display name.  Returns null if a specific class is not found.</returns>
        public static ClassBase GetCharacterClass(string displayName)
        {
            Type characterClassType = ClassCollection.FindClass(displayName).GetType();

            ClassBase characterClass = (ClassBase)Activator.CreateInstance(Type.GetType(characterClassType.ToString()));

            return characterClass;
        }

        /// <summary>
        /// Searches for an ability within the collection with the specified <paramref name="displayName"/>
        /// </summary>
        /// <param name="displayName">The display name</param>
        /// <returns>A character ability corresponding to the specified display name.  Returns null if a specific ability is not found.</returns>
        public static AbilityBase FindCharacterAbility(string displayName)
        {
            return AbilityCollection.FindAbility(displayName);
        }

        /// <summary>
        /// Returns an ability modifier to support "Your Choice" modifiers
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>An ability modifier for the specified character ability</returns>
        public static AbilityModifier GetRacialAbilityModifier(Type characterAbilityType)
        {
            AbilityModifier abilityModifier = new AbilityModifier(characterAbilityType, 2);

            return abilityModifier;
        }

        /// <summary>
        /// Returns the ability modifier for the specified score and racial modifier
        /// </summary>
        /// <param name="abilityScore">The ability's score</param>
        /// <param name="raceAbilityModifier">The ability's racial modifier</param>
        /// <returns>The calculated ability modifier based upon the specified ability score and racial ability modifier</returns>
        public static int CalculateAbilityModifier(int abilityScore, int raceAbilityModifier)
        {
            return AbilityBase.CalculateAbilityModifier(abilityScore, raceAbilityModifier);
        }

        /// <summary>
        /// Attempts to parse a string value as an integer
        /// </summary>
        /// <param name="input">The string</param>
        /// <returns>The result of the parse</returns>
        public static int ParseInput(string input)
        {
            int result = 0;

            int.TryParse(input, out result);

            return result;
        }

        /// <summary>
        /// Calculates the base value for an ability based upon the specified ability score and racial ability modifier
        /// </summary>
        /// <param name="abilityScore">The ability score</param>
        /// <param name="racialAbilityModifier">The racial ability modifier</param>
        /// <returns>An integer representing the base value for the ability</returns>
        public static int CalculateBaseAbility(int abilityScore, int racialAbilityModifier)
        {
            return abilityScore + racialAbilityModifier;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Character" /> class
        /// </summary>
        /// <param name="strength">The character's base strength ability</param>
        /// <param name="dexterity">The character's base dexterity ability</param>
        /// <param name="constitution">The character's base constitution ability</param>
        /// <param name="intelligence">The character's base intelligence ability</param>
        /// <param name="wisdom">The character's base wisdom ability</param>
        /// <param name="charisma">The character's base charisma ability</param>
        /// <param name="strengthModifier">The character's strength modifier</param>
        /// <param name="dexterityModifier">The character's dexterity modifier</param>
        /// <param name="constitutionModifier">The character's constitution modifier</param>
        /// <param name="intelligenceModifier">The character's intelligence modifier</param>
        /// <param name="wisdomModifier">The character's wisdom modifier</param>
        /// <param name="charismaModifier">The character's charisma modifier</param> 
        /// <param name="characterRace">The character's race</param>
        /// <param name="bonusLanguages">The character's chosen bonus languages</param>
        /// <param name="characterClass">The character's class</param>
        /// <param name="experience">The character's experience</param>
        /// <returns>Character</returns>
        public static Character Create
        (
           int strength,
           int dexterity,
           int constitution,
           int intelligence,
           int wisdom,
           int charisma,
           int strengthModifier,
           int dexterityModifier,
           int constitutionModifier,
           int intelligenceModifier,
           int wisdomModifier,
           int charismaModifier,
           RaceBase characterRace,
           List<Language> bonusLanguages,
           ClassBase characterClass,
           int experience
        )
        {
            Character character = Character.CreateInstance
            (
                strength,
                dexterity,
                constitution,
                intelligence,
                wisdom,
                charisma,
                strengthModifier,
                dexterityModifier,
                constitutionModifier,
                intelligenceModifier,
                wisdomModifier,
                charismaModifier,
                characterRace,
                bonusLanguages,
                characterClass,
                experience
            );

            return character;
        }
    }
}