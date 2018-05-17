using System;
using System.Collections.Generic;

// TODO: Move these to a more suitable location, potentially their own namespace

public enum CharacterAbility : int { Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma};

public enum CharacterRace : int { Dwarf, Elf, Gnome, Half_Elf, Half_Orc, Halfling, Human };
public enum CharacterClass : int { Barbarian, Bard, Cleric, Druid, Fighter, Monk, Paladin, Ranger, Rogue, Sorcerer, Wizard };

namespace PathFinderRPG
{
    public static class CharacterCreator
    {
        /// <summary>
        /// Used for character creation only at this time.  Di to be thrown, type, sort order, and number of di to ignore are hard coded
        /// Rolls 4 D6, ignores the lowest value, returns the sum of the remaining dice
        /// </summary>
        /// <returns>int</returns>
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
        /// Returns a list of ability bonuses for the specified character race
        /// </summary>
        /// <param name="characterRace">The CharacterRace</param>
        /// <returns>List</returns>
        public static List<AbilityBonus> GetAbilityBonuses(CharacterRace characterRace)
        {
            List<AbilityBonus> abilityBonuses = new List<AbilityBonus>();
            AbilityBonus abilityBonus;

            // TODO: Refactor
            switch (characterRace)
            {
                case CharacterRace.Dwarf:

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Constitution, 2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Wisdom, 2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Charisma, -2);
                    abilityBonuses.Add(abilityBonus);

                    break;

                case CharacterRace.Elf:

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Dexterity, 2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Constitution, -2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Intelligence, 2);
                    abilityBonuses.Add(abilityBonus);
                    
                    break;

                case CharacterRace.Gnome:

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Strength, -2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Constitution, 2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Charisma, 2);
                    abilityBonuses.Add(abilityBonus);

                    break;

                case CharacterRace.Halfling:

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Strength, -2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Dexterity, 2);
                    abilityBonuses.Add(abilityBonus);

                    abilityBonus = AbilityBonus.CreateInstance(CharacterAbility.Charisma, 2);
                    abilityBonuses.Add(abilityBonus);

                    break;

                case CharacterRace.Half_Elf:

                    // Note: Intentionally left empty, player can choose ability

                    break;

                case CharacterRace.Half_Orc:

                    // Note: Intentionally left empty, player can choose ability

                    break;

                case CharacterRace.Human:

                    // Note: Intentionally left empty, player can choose ability

                    break;
            }

            return abilityBonuses;
        }

        /// <summary>
        /// Returns a list of ability bonuses for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The CharacterAbility</param>
        /// <returns>List</returns>
        public static List<AbilityBonus> GetAbilityBonuses(CharacterAbility characterAbility)
        {
            List<AbilityBonus> abilityBonuses = new List<AbilityBonus>();
            AbilityBonus abilityBonus;

            // TODO: Remove hard coded value
            abilityBonus = AbilityBonus.CreateInstance(characterAbility, 2);
            abilityBonuses.Add(abilityBonus);

            return abilityBonuses;
        }


        /// <summary>
        /// Creates a character
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
        /// <param name="characterClass">The character's class</param>
        /// <param name="characterRace">The character's race</param>
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
            CharacterClass characterClass,
            CharacterRace characterRace
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
                characterClass,
                characterRace
            );

            return character;
        }

        /// <summary>
        /// Calculates the ability modifier for the specified base score
        /// </summary>
        /// <param name="abilityBaseScore">The ability's base score</param>
        public static int CalculateAbilityModifier(int abilityBaseScore, int abilityBonus)
        {
            double modifier = ((abilityBaseScore + abilityBonus) - 10) / 2;

            return (int)Math.Floor(modifier);
        }
    }
}