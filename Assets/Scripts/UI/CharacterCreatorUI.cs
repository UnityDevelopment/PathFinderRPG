using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathFinderRPG.Extensions;

namespace PathFinderRPG
{
    public class CharacterCreatorUI : MonoBehaviour
    {
        // UI objects
        [Header("Abilities")]
        public Text _strength;
        public Text _dexterity;
        public Text _constitution;
        public Text _intelligence;
        public Text _wisdom;
        public Text _charisma;

        [Header("Ability Bonuses")]
        public Text _strengthBonus;
        public Text _dexterityBonus;
        public Text _constitutionBonus;
        public Text _intelligenceBonus;
        public Text _wisdomBonus;
        public Text _charismaBonus;

        [Header("Ability Modifiers")]
        public Text _strengthModifier;
        public Text _dexterityModifier;
        public Text _constitutionModifier;
        public Text _intelligenceModifier;
        public Text _wisdomModifier;
        public Text _charismaModifier;

        [Header("Race")]
        public Dropdown _characterRace;

        [Header("Class")]
        public Dropdown _characterClass;


        // dropdown heading index offset
        private int _dropdownHeadingOffset = -1;


        /// <summary>
        /// Event handler for strength ability
        /// </summary>
        public void RollStrength()
        {
            UpdateAbilityScore(CharacterAbility.Strength);
            UpdateAbilityModifier(CharacterAbility.Strength);
        }

        /// <summary>
        /// Event handler for dexterity ability
        /// </summary>
        public void RollDexterity()
        {
            UpdateAbilityScore(CharacterAbility.Dexterity);
            UpdateAbilityModifier(CharacterAbility.Dexterity);
        }

        /// <summary>
        /// Event handler for constitution ability
        /// </summary>
        public void RollConstitution()
        {
            UpdateAbilityScore(CharacterAbility.Constitution);
            UpdateAbilityModifier(CharacterAbility.Constitution);
        }

        /// <summary>
        /// Event handler for intelligence ability
        /// </summary>
        public void RollIntelligence()
        {
            UpdateAbilityScore(CharacterAbility.Intelligence);
            UpdateAbilityModifier(CharacterAbility.Intelligence);
        }

        /// <summary>
        /// Event handler for wisdom ability
        /// </summary>
        public void RollWisdom()
        {
            UpdateAbilityScore(CharacterAbility.Wisdom);
            UpdateAbilityModifier(CharacterAbility.Wisdom);
        }

        /// <summary>
        /// Event handler for charisma ability
        /// </summary>
        public void RollCharisma()
        {
            UpdateAbilityScore(CharacterAbility.Charisma);
            UpdateAbilityModifier(CharacterAbility.Charisma);
        }
        
        /// <summary>
        /// Event handler for race selection
        /// </summary>
        public void SelectedRaceChanged()
        {
            CharacterRace characterRace = GetCharacterRace();

            UpdateAbilityBonuses(characterRace);
            UpdateAbilityModifiers();
        }

        /// <summary>
        /// Event handler for character creation
        /// </summary>
        public void CreateCharacter()
        {
            // TODO: Validate dropdown selections ( > -1 )
            // TODO: Validate abilities are valid ( > 0 )

            int baseStrength = CalculateBaseAbility(CharacterAbility.Strength);
            int baseDexterity = CalculateBaseAbility(CharacterAbility.Dexterity);
            int baseConstitution = CalculateBaseAbility(CharacterAbility.Constitution);
            int baseIntelligence = CalculateBaseAbility(CharacterAbility.Intelligence);
            int baseWisdom = CalculateBaseAbility(CharacterAbility.Wisdom);
            int baseCharisma = CalculateBaseAbility(CharacterAbility.Charisma);
            int strengthModifier = ParseAbilityModifier(CharacterAbility.Strength);
            int dexterityModifier = ParseAbilityModifier(CharacterAbility.Dexterity);
            int constitutionModifier = ParseAbilityModifier(CharacterAbility.Constitution);
            int intelligenceModifier = ParseAbilityModifier(CharacterAbility.Intelligence);
            int wisdomModifier = ParseAbilityModifier(CharacterAbility.Wisdom);
            int charismaModifier = ParseAbilityModifier(CharacterAbility.Charisma);

            CharacterClass characterClass = GetCharacterClass();
            CharacterRace characterRace = GetCharacterRace();

            // returns a Character class
            Character character = CharacterCreator.Create
                (
                    baseStrength,
                    baseDexterity,
                    baseConstitution,
                    baseIntelligence,
                    baseWisdom,
                    baseCharisma,
                    strengthModifier,
                    dexterityModifier,
                    constitutionModifier,
                    intelligenceModifier,
                    wisdomModifier,
                    charismaModifier,
                    characterClass,
                    characterRace
                );

            // TODO: Temporary
            Player player = GameObject.FindObjectOfType<Player>();
            player.character = character;
        }


        /// <summary>
        /// Initialisation
        /// </summary>
        private void Start()
        {
            PopulateCharacterClasses();
            PopulateCharacterRaces();
        }


        /// <summary>
        /// Populate the Character Classes dropdown
        /// </summary>
        private void PopulateCharacterClasses()
        {
            string[] characterClasses = Enum.GetNames(typeof(CharacterClass));

            PopulateDropdown(_characterClass, "Class", characterClasses);
        }

        /// <summary>
        /// Populate the Character Races dropdown
        /// </summary>
        private void PopulateCharacterRaces()
        {
            string[] charaterRaces = Enum.GetNames(typeof(CharacterRace));

            PopulateDropdown(_characterRace, "Race", charaterRaces);
        }

        /// <summary>
        /// Populate a dropdown menu with option
        /// </summary>
        /// <param name="dropdown">The dropdown menu to populate</param>
        /// <param name="headingOption">The heading option for the menu</param>
        /// <param name="options">The options</param>
        private void PopulateDropdown(Dropdown dropdown, string headingOption, string[] options)
        {
            dropdown.ClearOptions();

            List<string> optionItems = new List<string>(options);

            optionItems.Insert(0, headingOption);

            dropdown.AddOptions(optionItems);
        }


        /// <summary>
        /// Recalculates and updates the ability score for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        private void UpdateAbilityScore(CharacterAbility characterAbility)
        {
            Text ability = GetAbilityGameObject(characterAbility);
            
            int abilityScore = CharacterCreator.RollForAbilityScore();

            ability.text = abilityScore.ToString();
        }

        /// <summary>
        /// Updates the ability bonuses for the selected race
        /// </summary>
        private void UpdateAbilityBonuses(CharacterRace characterRace)
        {
            ClearAbilityBonuses();

            List<AbilityBonus> abilityBonuses = CharacterCreator.GetAbilityBonuses(characterRace);

            foreach (AbilityBonus abilityBonus in abilityBonuses)
            {
                UpdateAbilityBonus(abilityBonus.Ability, abilityBonus.Value);
            }
        }

        /// <summary>
        /// Updates the ability bonus for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <param name="bonus">The ability bonus</param>
        private void UpdateAbilityBonus(CharacterAbility characterAbility, int bonus)
        {
            Text abilityBonus = GetAbilityBonusGameObject(characterAbility);

            abilityBonus.text = bonus.ToString(true);
        }

        /// <summary>
        /// Recalculates and updates all ability modifiers
        /// </summary>
        private void UpdateAbilityModifiers()
        {
            UpdateAbilityModifier(CharacterAbility.Strength);
            UpdateAbilityModifier(CharacterAbility.Dexterity);
            UpdateAbilityModifier(CharacterAbility.Constitution);
            UpdateAbilityModifier(CharacterAbility.Intelligence);
            UpdateAbilityModifier(CharacterAbility.Wisdom);
            UpdateAbilityModifier(CharacterAbility.Charisma);
        }

        /// <summary>
        /// Recalculates and updates the ability's modifier for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        private void UpdateAbilityModifier(CharacterAbility characterAbility)
        {
            Text ability = GetAbilityGameObject(characterAbility);
            Text abilityBonus = GetAbilityBonusGameObject(characterAbility);
            Text abilityModifier = GetAbilityModifierGameObject(characterAbility);

            int score = ParseAbilityScore(characterAbility);
            int bonus = ParseAbilityBonus(characterAbility);
            int modifier = CharacterCreator.CalculateAbilityModifier(score, bonus);

            abilityModifier.text = modifier.ToString(true);
        }


        /// <summary>
        /// Clears the currently display ability bonuses
        /// </summary>
        private void ClearAbilityBonuses()
        {
            ClearAbilityBonus(CharacterAbility.Strength);
            ClearAbilityBonus(CharacterAbility.Dexterity);
            ClearAbilityBonus(CharacterAbility.Constitution);
            ClearAbilityBonus(CharacterAbility.Intelligence);
            ClearAbilityBonus(CharacterAbility.Wisdom);
            ClearAbilityBonus(CharacterAbility.Charisma);
        }

        /// <summary>
        /// Clears the ability's Text GameObject for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        private void ClearAbilityBonus(CharacterAbility characterAbility)
        {
            Text abilityBonus = GetAbilityBonusGameObject(characterAbility);

            abilityBonus.text = String.Empty;
        }


        /// <summary>
        /// Returns the ability's Text GameObject for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>Text</returns>
        private Text GetAbilityGameObject(CharacterAbility characterAbility)
        {
            Text ability = null;

            switch (characterAbility)
            {
                case CharacterAbility.Strength:

                    ability = _strength;

                    break;

                case CharacterAbility.Dexterity:

                    ability = _dexterity;

                    break;

                case CharacterAbility.Constitution:

                    ability = _constitution;

                    break;

                case CharacterAbility.Intelligence:

                    ability = _intelligence;

                    break;

                case CharacterAbility.Wisdom:

                    ability = _wisdom;

                    break;

                case CharacterAbility.Charisma:

                    ability = _charisma;

                    break;
            }

            return ability;
        }

        /// <summary>
        /// Returns the ability's bonus Text GameObject for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>Text</returns>
        private Text GetAbilityBonusGameObject(CharacterAbility characterAbility)
        {
            Text abilityBonus = null;

            switch (characterAbility)
            {
                case CharacterAbility.Strength:

                    abilityBonus = _strengthBonus;

                    break;

                case CharacterAbility.Dexterity:

                    abilityBonus = _dexterityBonus;

                    break;

                case CharacterAbility.Constitution:

                    abilityBonus = _constitutionBonus;

                    break;

                case CharacterAbility.Intelligence:

                    abilityBonus = _intelligenceBonus;

                    break;

                case CharacterAbility.Wisdom:

                    abilityBonus = _wisdomBonus;

                    break;

                case CharacterAbility.Charisma:

                    abilityBonus = _charismaBonus;

                    break;
            }

            return abilityBonus;
        }

        /// <summary>
        /// Returns the ability's modifier Text GameObject for the specified character ability
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>Text</returns>
        private Text GetAbilityModifierGameObject(CharacterAbility characterAbility)
        {
            Text abilityModifier = null;

            switch (characterAbility)
            {
                case CharacterAbility.Strength:

                    abilityModifier = _strengthModifier;

                    break;

                case CharacterAbility.Dexterity:

                    abilityModifier = _dexterityModifier;

                    break;

                case CharacterAbility.Constitution:

                    abilityModifier = _constitutionModifier;

                    break;

                case CharacterAbility.Intelligence:

                    abilityModifier = _intelligenceModifier;

                    break;

                case CharacterAbility.Wisdom:

                    abilityModifier = _wisdomModifier;

                    break;

                case CharacterAbility.Charisma:

                    abilityModifier = _charismaModifier;

                    break;
            }

            return abilityModifier;
        }


        /// <summary>
        /// Parses ability score
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>int</returns>
        private int ParseAbilityScore(CharacterAbility characterAbility)
        {
            Text ability = GetAbilityGameObject(characterAbility);

            return ParseInput(ability.text);
        }

        /// <summary>
        /// Parses ability bonus
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>int</returns>
        private int ParseAbilityBonus(CharacterAbility characterAbility)
        {
            Text abilityBonus = GetAbilityBonusGameObject(characterAbility);

            return ParseInput(abilityBonus.text);
        }

        /// <summary>
        /// Parses ability modifier
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>int</returns>
        private int ParseAbilityModifier(CharacterAbility characterAbility)
        {
            Text abilityModifier = GetAbilityModifierGameObject(characterAbility);

            return ParseInput(abilityModifier.text);
        }

        /// <summary>
        /// Attempts to parse string as int
        /// </summary>
        /// <param name="input">The string</param>
        /// <returns>int</returns>
        private int ParseInput(string input)
        {
            int result = 0;

            int.TryParse(input, out result);

            return result;
        }

        /// <summary>
        /// Returns the sum of the character's ability score and ability bonus
        /// </summary>
        /// <param name="characterAbility">The character ability</param>
        /// <returns>int</returns>
        private int CalculateBaseAbility(CharacterAbility characterAbility)
        {
            int baseAbility;

            baseAbility = ParseAbilityScore(characterAbility) + ParseAbilityBonus(characterAbility);

            return baseAbility;
        }

        /// <summary>
        /// Returns the selected character race
        /// </summary>
        /// <returns>CharacterRace</returns>
        private CharacterRace GetCharacterRace()
        {
            return (CharacterRace)_characterRace.value + _dropdownHeadingOffset;
        }

        /// <summary>
        /// Returns the selected character class
        /// </summary>
        /// <returns>CharacterClass</returns>
        private CharacterClass GetCharacterClass()
        {
            return (CharacterClass)_characterClass.value + _dropdownHeadingOffset;
        }
    }
}