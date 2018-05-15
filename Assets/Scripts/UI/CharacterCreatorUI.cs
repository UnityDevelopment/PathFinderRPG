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
            int baseScore = CharacterCreator.RollForBaseStrengthAbility();
            int modifier = CharacterCreator.CalculateAbilityModifier(baseScore);

            _strength.text = baseScore.ToString();
            _strengthModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Event handler for dexterity ability
        /// </summary>
        public void RollDexterity()
        {
            int baseScore = CharacterCreator.RollForBaseDexterityAbility();
            int abilityModifier = CharacterCreator.CalculateAbilityModifier(baseScore);

            _dexterity.text = baseScore.ToString();
            _dexterityModifier.text = abilityModifier.ToString(true);
        }

        /// <summary>
        /// Event handler for constitution ability
        /// </summary>
        public void RollConstitution()
        {
            int baseScore = CharacterCreator.RollForBaseConstitutionAbility();
            int modifier = CharacterCreator.CalculateAbilityModifier(baseScore);

            _constitution.text = baseScore.ToString();
            _constitutionModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Event handler for intelligence ability
        /// </summary>
        public void RollIntelligence()
        {
            int baseScore = CharacterCreator.RollForBaseIntelligenceAbility();
            int modifier = CharacterCreator.CalculateAbilityModifier(baseScore);

            _intelligence.text = baseScore.ToString();
            _intelligenceModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Event handler for wisdom ability
        /// </summary>
        public void RollWisdom()
        {
            int baseScore = CharacterCreator.RollForBaseWisdomAbility();
            int modifier = CharacterCreator.CalculateAbilityModifier(baseScore);

            _wisdom.text = baseScore.ToString();
            _wisdomModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Event handler for charisma ability
        /// </summary>
        public void RollCharisma()
        {
            int baseScore = CharacterCreator.RollForBaseCharismaAbility();
            int modifier = CharacterCreator.CalculateAbilityModifier(baseScore);

            _charisma.text = baseScore.ToString();
            _charismaModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Event handler for race selection
        /// </summary>
        public void SelectedRaceChanged()
        {
            CharacterRace characterRace = (CharacterRace)_characterRace.value + _dropdownHeadingOffset;

            UpdateAbilityBonuses(characterRace);
        }

        /// <summary>
        /// Event handler for character creation
        /// </summary>
        public void CreateCharacter()
        {
            // TODO: Validate dropdown selections ( > -1 )
            // TODO: Validate abilities are valid ( > 0 )

            //try
            //{
            int baseStrength = int.Parse(_strength.text);
            int baseDexterity = int.Parse(_dexterity.text);
            int baseConstitution = int.Parse(_constitution.text);
            int baseIntelligence = int.Parse(_intelligence.text);
            int baseWisdom = int.Parse(_wisdom.text);
            int baseCharisma = int.Parse(_charisma.text);
            CharacterClass characterClass = (CharacterClass)(_characterClass.value + _dropdownHeadingOffset);
            CharacterRace characterRace = (CharacterRace)(_characterRace.value + _dropdownHeadingOffset);

            // returns a Character class
            Character character = CharacterCreator.Create
                (
                    baseStrength,
                    baseDexterity,
                    baseConstitution,
                    baseIntelligence,
                    baseWisdom,
                    baseCharisma,
                    characterClass,
                    characterRace
                );

            // TODO: Temporary
            Player player = GameObject.FindObjectOfType<Player>();

            player.character = character;
            //}
            //catch
            //{
            //    throw new System.InvalidCastException("Base ability failed to parse.");
            //}
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
        /// 
        /// </summary>
        private void UpdateAbilityBonuses(CharacterRace characterRace)
        {
            ClearAbilityBonuses();

            List<AbilityBonus> abilityBonuses = CharacterCreator.GetAbilityBonuses(characterRace);

            foreach (AbilityBonus abilityBonus in abilityBonuses)
            {
                // TODO: Refactor, helper method to return corresponding UI Text GameObject
                switch (abilityBonus.Ability)
                {
                    case CharacterAbility.Strength:

                        _strengthBonus.text = abilityBonus.Value.ToString(true);

                        break;

                    case CharacterAbility.Dexterity:

                        _dexterityBonus.text = abilityBonus.Value.ToString(true);

                        break;

                    case CharacterAbility.Constitution:

                        _constitutionBonus.text = abilityBonus.Value.ToString(true);

                        break;

                    case CharacterAbility.Intelligence:

                        _intelligenceBonus.text = abilityBonus.Value.ToString(true);

                        break;

                    case CharacterAbility.Wisdom:

                        _wisdomBonus.text = abilityBonus.Value.ToString(true);

                        break;

                    case CharacterAbility.Charisma:

                        _charismaBonus.text = abilityBonus.Value.ToString(true);

                        break;
                }
            }
        }

        /// <summary>
        /// Clears the currently display ability bonuses
        /// </summary>
        private void ClearAbilityBonuses()
        {
            _strengthBonus.text = string.Empty;
            _dexterityBonus.text = string.Empty;
            _constitutionBonus.text = string.Empty;
            _intelligenceBonus.text = string.Empty;
            _wisdomBonus.text = string.Empty;
            _charismaBonus.text = string.Empty;
        }
    }
}