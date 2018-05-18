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

        [Space(10f)]

        public Dropdown _abilities;

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

        [Space(10f)]

        public Text _level;
        public Text _experience;
        public Text _health;
        public Text _hitDie;
        


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

            if (SelectedOptionIsValid(typeof(CharacterRace), characterRace))
            {
                // TODO: Refactor this with enum -> class changes
                if (characterRace == CharacterRace.Half_Elf || characterRace == CharacterRace.Half_Orc || characterRace == CharacterRace.Human)
                {
                    ClearAbilityBonuses();

                    EnableRacialBonusSelection();
                }
                else
                {
                    DisableRacialBonusSelection();

                    UpdateAbilityBonuses(characterRace);
                }

                UpdateAbilityModifiers();
            }
        }

        /// <summary>
        /// Event handler for class selection
        /// </summary>
        public void SelectedClassChanged()
        {
            CharacterClass characterClass = GetCharacterClass();

            if (SelectedOptionIsValid(typeof(CharacterClass), characterClass))
            {
                SetClassSpecificAttributes(characterClass);
            }

            PopulateLevel1Attributes();
        }

        /// <summary>
        /// Event handler for ability selection
        /// </summary>
        public void SelectedAbilityChanged()
        {
            if (_abilities.isActiveAndEnabled)
            {
                CharacterAbility characterAbility = GetCharacterAbility();

                if (SelectedOptionIsValid(typeof(CharacterAbility), characterAbility))
                {
                    UpdateAbilityBonuses(characterAbility);
                    UpdateAbilityModifiers();
                }
                else
                {
                    ClearAbilityBonuses();
                }
            }
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

            CharacterRace characterRace = GetCharacterRace();
            CharacterClass characterClass = GetCharacterClass();

            int level = ParseAttribute(CharacterAttribute.Level);
            int experience = ParseAttribute(CharacterAttribute.Experience);
            Dice.DieType hitDie = (Dice.DieType)ParseAttribute(CharacterAttribute.HitDie);
            int health = ParseAttribute(CharacterAttribute.Health);

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
                    characterRace,
                    characterClass,
                    level,
                    experience,
                    hitDie,
                    health
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
            PopulateCharacterRaces();
            PopulateCharacterClasses();
            PopulateCharacterAbilities();
        }


        /// <summary>
        /// Populate the Character Races dropdown
        /// </summary>
        private void PopulateCharacterRaces()
        {
            string[] characterRaces = Enum.GetNames(typeof(CharacterRace));

            PopulateDropdown(_characterRace, "Race", characterRaces);
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
        /// Populate the Character Abilities dropdown
        /// </summary>
        private void PopulateCharacterAbilities()
        {
            string[] characterAbilities = Enum.GetNames(typeof(CharacterAbility));

            PopulateDropdown(_abilities, "Ability", characterAbilities);
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
        /// Enables the racial bonus abilities dropdown
        /// </summary>
        private void EnableRacialBonusSelection()
        {
            _abilities.value = 0;
            _abilities.gameObject.SetActive(true);
        }

        /// <summary>
        /// Disables the racial bonus abilities dropdown
        /// </summary>
        private void DisableRacialBonusSelection()
        {
            _abilities.gameObject.SetActive(false);
            _abilities.value = 0;
        }

        /// <summary>
        /// Indicates whether a selected option is valid
        /// </summary>
        /// <param name="enumType">The enum type</param>
        /// <param name="option">The selected option</param>
        /// <returns>bool</returns>
        private bool SelectedOptionIsValid(Type enumType, object option)
        {
            return Enum.IsDefined(enumType, option);
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
        /// Updates the ability bonus for the selected ability
        /// </summary>
        private void UpdateAbilityBonuses(CharacterAbility characterAbility)
        {
            ClearAbilityBonuses();

            List<AbilityBonus> abilityBonuses = CharacterCreator.GetAbilityBonuses(characterAbility);

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
            Text abilityModifier = GetAbilityModifierGameObject(characterAbility);

            int score = ParseAbilityScore(characterAbility);
            int bonus = ParseAbilityBonus(characterAbility);
            int modifier = CharacterCreator.CalculateAbilityModifier(score, bonus);

            abilityModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Updates the attribute for the specified character attribute
        /// </summary>
        /// <param name="characterAttribute">The character attribute</param>
        /// <param name="value">The attribute value</param>
        private void UpdateAttribute(CharacterAttribute characterAttribute, int value)
        {
            Text attribute = GetAttributeGameObject(characterAttribute);

            attribute.text = value.ToString();
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
        /// Returns the attribute's Text GameObject for the specified character attribute
        /// </summary>
        /// <param name="characterAttribute">The character attribute</param>
        /// <returns>Text</returns>
        private Text GetAttributeGameObject(CharacterAttribute characterAttribute)
        {
            Text attribute = null;

            switch (characterAttribute)
            {
                case CharacterAttribute.Level:
                    {
                        attribute = _level;

                        break;
                    }
                case CharacterAttribute.Experience:
                    {
                        attribute = _experience;

                        break;
                    }
                case CharacterAttribute.HitDie:
                    {
                        attribute = _hitDie;

                        break;
                    }
                case CharacterAttribute.Health:
                    {
                        attribute = _health;

                        break;
                    }
            }

            return attribute;
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
        /// Parses attribute
        /// </summary>
        /// <param name="characterAttribute">The character attribute</param>
        /// <returns>int</returns>
        private int ParseAttribute(CharacterAttribute characterAttribute)
        {
            Text attribute = GetAttributeGameObject(characterAttribute);

            return ParseInput(attribute.text);
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

        /// <summary>
        /// Returns the selected character ability
        /// </summary>
        /// <returns>CharacterAbility</returns>
        private CharacterAbility GetCharacterAbility()
        {
            return (CharacterAbility)_abilities.value + _dropdownHeadingOffset;
        }

        /// <summary>
        /// Sets class specific attributes
        /// </summary>
        /// <param name="characterClass">The character class</param>
        private void SetClassSpecificAttributes(CharacterClass characterClass)
        {
            Dice.DieType hitDie = CharacterCreator.GetHitDie(characterClass);

            UpdateAttribute(CharacterAttribute.HitDie, (int)hitDie);
            UpdateAttribute(CharacterAttribute.Health, CharacterCreator.GetHealth(hitDie));

            // TODO: Apply modifiers
        }

        /// <summary>
        /// Populate level 1 character attributes
        /// </summary>
        private void PopulateLevel1Attributes()
        {
            // NOTE: Potentially temporary as level may become selectable
            _level.text = 1.ToString();
            _experience.text = 0.ToString();
        }
    }
}