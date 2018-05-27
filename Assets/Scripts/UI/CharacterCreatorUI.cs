namespace PathfinderRPG.UI
{
    using System;
    using System.Collections.Generic;

    using PathfinderRPG.Entities;
    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Attributes;
    using PathfinderRPG.Entities.Classes;
    using PathfinderRPG.Entities.Races;
    using PathfinderRPG.Extensions;

    using UnityEngine;
    using UnityEngine.UI;

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

        [Header("Racial Ability Modifiers")]
        public Text _strengthRacialModifier;
        public Text _dexterityRacialModifier;
        public Text _constitutionRacialModifier;
        public Text _intelligenceRacialModifier;
        public Text _wisdomRacialModifier;
        public Text _charismaRacialModifier;

        [Space(10f)]

        public Dropdown _characterAbility;

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

        /// <summary>
        /// Event handler for strength ability
        /// </summary>
        public void RollStrength()
        {
            UpdateAbilityScore(typeof(Strength));
            UpdateAbilityModifier(typeof(Strength));
        }

        /// <summary>
        /// Event handler for dexterity ability
        /// </summary>
        public void RollDexterity()
        {
            UpdateAbilityScore(typeof(Dexterity));
            UpdateAbilityModifier(typeof(Dexterity));
        }

        /// <summary>
        /// Event handler for constitution ability
        /// </summary>
        public void RollConstitution()
        {
            UpdateAbilityScore(typeof(Constitution));
            UpdateAbilityModifier(typeof(Constitution));
        }

        /// <summary>
        /// Event handler for intelligence ability
        /// </summary>
        public void RollIntelligence()
        {
            UpdateAbilityScore(typeof(Intelligence));
            UpdateAbilityModifier(typeof(Intelligence));
        }

        /// <summary>
        /// Event handler for wisdom ability
        /// </summary>
        public void RollWisdom()
        {
            UpdateAbilityScore(typeof(Wisdom));
            UpdateAbilityModifier(typeof(Wisdom));
        }

        /// <summary>
        /// Event handler for charisma ability
        /// </summary>
        public void RollCharisma()
        {
            UpdateAbilityScore(typeof(Charisma));
            UpdateAbilityModifier(typeof(Charisma));
        }

        /// <summary>
        /// Event handler for character race selection
        /// </summary>
        public void SelectedRaceChanged()
        {
            RaceBase characterRace = GetCharacterRace();

            UpdateRacialAbilityModifiers(characterRace);
            UpdateAbilityModifiers();
        }

        /// <summary>
        /// Event handler for character class selection
        /// </summary>
        public void SelectedClassChanged()
        {
            ClassBase characterClass = GetCharacterClass();

            UpdateAttributes(characterClass);
        }

        /// <summary>
        /// Event handler for character ability selection
        /// </summary>
        public void SelectedAbilityChanged()
        {
            if (_characterAbility.isActiveAndEnabled)
            {
                AbilityBase characterAbility = GetCharacterAbility();

                UpdateRacialAbilityModifiers(characterAbility.GetType());
                UpdateAbilityModifiers();
            }
        }

        /// <summary>
        /// Event handler for character creation
        /// </summary>
        public void CreateCharacter()
        {
            int baseStrength = CalculateBaseAbility(typeof(Strength));
            int baseDexterity = CalculateBaseAbility(typeof(Dexterity));
            int baseConstitution = CalculateBaseAbility(typeof(Constitution));
            int baseIntelligence = CalculateBaseAbility(typeof(Intelligence));
            int baseWisdom = CalculateBaseAbility(typeof(Wisdom));
            int baseCharisma = CalculateBaseAbility(typeof(Charisma));
            int strengthModifier = ParseAbilityModifier(typeof(Strength));
            int dexterityModifier = ParseAbilityModifier(typeof(Dexterity));
            int constitutionModifier = ParseAbilityModifier(typeof(Constitution));
            int intelligenceModifier = ParseAbilityModifier(typeof(Intelligence));
            int wisdomModifier = ParseAbilityModifier(typeof(Wisdom));
            int charismaModifier = ParseAbilityModifier(typeof(Charisma));
            RaceBase characterRace = GetCharacterRace();
            ClassBase characterClass = GetCharacterClass();
            int level = ParseAttribute(typeof(Level));
            int experience = ParseAttribute(typeof(Experience));

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
                    experience
                );

            // TODO: Temporary
            Player player = GameObject.FindObjectOfType<Player>();
            player.character = character;
        }

        /// <summary>
        /// Initialises the instance of the <see cref="CharacterCreatorUI" /> class
        /// </summary>
        private void Start()
        {
            PopulateCharacterRaces();
            PopulateCharacterClasses();
            PopulateCharacterAbilities();
        }

        /// <summary>
        /// Populate the character races dropdown
        /// </summary>
        private void PopulateCharacterRaces()
        {
            List<string> characterRaces = CharacterCreator.GetRaceDisplayNames();

            PopulateDropdown(_characterRace, characterRaces);
        }

        /// <summary>
        /// Populate the character classes dropdown
        /// </summary>
        private void PopulateCharacterClasses()
        {
            List<string> characterClasses = CharacterCreator.GetClassDisplayNames();

            PopulateDropdown(_characterClass, characterClasses);
        }

        /// <summary>
        /// Populate the character abilities dropdown
        /// </summary>
        private void PopulateCharacterAbilities()
        {
            List<string> characterAbilities = CharacterCreator.GetAbilityDisplayNames();

            PopulateDropdown(_characterAbility, characterAbilities);
        }

        /// <summary>
        /// Populates <paramref name="dropdown"/> with <paramref name="optionItems"/>
        /// </summary>
        /// <param name="dropdown">The dropdown menu to populate</param>
        /// <param name="optionItems">The option items</param>
        private void PopulateDropdown(Dropdown dropdown, List<string> optionItems)
        {
            dropdown.AddOptions(optionItems);
        }

        /// <summary>
        /// Enables the racial modifiers ability dropdown
        /// </summary>
        private void EnableRacialModifierSelection()
        {
            DropdownSetActive(_characterAbility, true);
        }

        /// <summary>
        /// Disables the racial modifiers ability dropdown
        /// </summary>
        private void DisableRacialModifierSelection()
        {
            DropdownSetActive(_characterAbility, false);
        }

        /// <summary>
        /// Enables or disables <paramref name="dropdown"/> based upon the value of <paramref name="active"/>
        /// </summary>
        /// <param name="dropdown">The dropdown menu to enable/disable</param>
        /// <param name="active">The state to set</param>
        private void DropdownSetActive(Dropdown dropdown, bool active)
        {
            dropdown.value = 0;
            dropdown.gameObject.SetActive(active);
        }

        /// <summary>
        /// Updates the ability score for the specified character ability type
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        private void UpdateAbilityScore(Type characterAbilityType)
        {
            Text ability = GetAbilityGameObject(characterAbilityType);

            int abilityScore = CharacterCreator.RollForAbilityScore();

            ability.text = abilityScore.ToString();
        }

        /// <summary>
        /// Updates the racial ability modifiers for the selected race
        /// </summary>
        /// <param name="characterRace">The character race</param>
        private void UpdateRacialAbilityModifiers(RaceBase characterRace)
        {
            ClearRacialAbilityModifiers();

            if (characterRace.AbilityModifiers.Count == 0)
            {
                EnableRacialModifierSelection();
            }
            else
            {
                DisableRacialModifierSelection();

                UpdateRacialAbilityModifier(_strengthRacialModifier, characterRace.GetModifier<Strength>());
                UpdateRacialAbilityModifier(_dexterityRacialModifier, characterRace.GetModifier<Dexterity>());
                UpdateRacialAbilityModifier(_constitutionRacialModifier, characterRace.GetModifier<Constitution>());
                UpdateRacialAbilityModifier(_intelligenceRacialModifier, characterRace.GetModifier<Intelligence>());
                UpdateRacialAbilityModifier(_wisdomRacialModifier, characterRace.GetModifier<Wisdom>());
                UpdateRacialAbilityModifier(_charismaRacialModifier, characterRace.GetModifier<Charisma>());
            }
        }

        /// <summary>
        /// Updates the racial ability modifiers for the selected ability
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        private void UpdateRacialAbilityModifiers(Type characterAbilityType)
        {
            AbilityModifier racialAbilityModifier = CharacterCreator.GetRacialAbilityModifier(characterAbilityType);
            Text characterAbilityRacialModifier = GetAbilityRacialModifierGameObject(characterAbilityType);

            ClearRacialAbilityModifiers();
            UpdateRacialAbilityModifier(characterAbilityRacialModifier, racialAbilityModifier.Modifier);
        }

        /// <summary>
        /// Updates the character ability's <paramref name="characterAbilityRacialModifier"/> with the value of <paramref name="modifier"/>
        /// </summary>
        /// <param name="characterAbilityRacialModifier">The character ability</param>
        /// <param name="modifier">The racial ability modifier</param>
        private void UpdateRacialAbilityModifier(Text characterAbilityRacialModifier, int modifier)
        {
            characterAbilityRacialModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Recalculates and updates all ability modifiers
        /// </summary>
        private void UpdateAbilityModifiers()
        {
            UpdateAbilityModifier(typeof(Strength));
            UpdateAbilityModifier(typeof(Dexterity));
            UpdateAbilityModifier(typeof(Constitution));
            UpdateAbilityModifier(typeof(Intelligence));
            UpdateAbilityModifier(typeof(Wisdom));
            UpdateAbilityModifier(typeof(Charisma));
        }

        /// <summary>
        /// Recalculates and updates the ability's modifier for the specified character ability type
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        private void UpdateAbilityModifier(Type characterAbilityType)
        {
            Text abilityModifier = GetAbilityModifierGameObject(characterAbilityType);

            int score = ParseAbilityScore(characterAbilityType);
            int racialModifier = ParseRacialAbilityModifier(characterAbilityType);
            int modifier = CharacterCreator.CalculateAbilityModifier(score, racialModifier);

            abilityModifier.text = modifier.ToString(true);
        }

        /// <summary>
        /// Updates class specific attributes for the specific character class
        /// </summary>
        /// <param name="characterClass">The character class</param>
        private void UpdateAttributes(ClassBase characterClass)
        {
            // TODO: Displayed health will need to be calculated differently if level selection is available later
            UpdateAttribute(typeof(Health), (int)characterClass.HitDie);

            // TODO: Character level may be/may need to be selectable in a furture version
            PopulateLevel1Attributes();
        }

        /// <summary>
        /// Updates the attribute for the specified character attribute type
        /// </summary>
        /// <param name="characterAttributeType">The character attribute type</param>
        /// <param name="value">The attribute value</param>
        private void UpdateAttribute(Type characterAttributeType, int value)
        {
            Text attribute = GetAttributeGameObject(characterAttributeType);

            attribute.text = value.ToString();
        }

        /// <summary>
        /// Returns the ability's Text GameObject for the specified character ability type
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>The corresponding UI Text GameObject for the character ability</returns>
        private Text GetAbilityGameObject(Type characterAbilityType)
        {
            Text abilityGameObject = null;

            // TODO: Improve this.  Could we use an IDictionary to map TextUI objects to types?
            if (characterAbilityType == typeof(Strength))
            {
                abilityGameObject = _strength;
            }
            else if (characterAbilityType == typeof(Dexterity))
            {
                abilityGameObject = _dexterity;
            }
            else if (characterAbilityType == typeof(Constitution))
            {
                abilityGameObject = _constitution;
            }
            else if (characterAbilityType == typeof(Intelligence))
            {
                abilityGameObject = _intelligence;
            }
            else if (characterAbilityType == typeof(Wisdom))
            {
                abilityGameObject = _wisdom;
            }
            else if (characterAbilityType == typeof(Charisma))
            {
                abilityGameObject = _charisma;
            }

            return abilityGameObject;
        }

        /// <summary>
        /// Returns the character ability racial modifier's UI Text GameObject
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>The corresponding UI Text GameObject for the character ability racial modifier</returns>
        private Text GetAbilityRacialModifierGameObject(Type characterAbilityType)
        {
            Text characterAbilityRacialModifier = null;

            // TODO: Improve this.  Could we use an IDictionary to map TextUI objects to types?
            if (characterAbilityType == typeof(Strength))
            {
                characterAbilityRacialModifier = _strengthRacialModifier;
            }
            else if (characterAbilityType == typeof(Dexterity))
            {
                characterAbilityRacialModifier = _dexterityRacialModifier;
            }
            else if (characterAbilityType == typeof(Constitution))
            {
                characterAbilityRacialModifier = _constitutionRacialModifier;
            }
            else if (characterAbilityType == typeof(Intelligence))
            {
                characterAbilityRacialModifier = _intelligenceRacialModifier;
            }
            else if (characterAbilityType == typeof(Wisdom))
            {
                characterAbilityRacialModifier = _wisdomRacialModifier;
            }
            else if (characterAbilityType == typeof(Charisma))
            {
                characterAbilityRacialModifier = _charismaRacialModifier;
            }

            return characterAbilityRacialModifier;
        }

        /// <summary>
        /// Returns the ability's modifier Text GameObject for the specified character ability type
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>The corresponding UI Text GameObject for the character ability's modifier</returns>
        private Text GetAbilityModifierGameObject(Type characterAbilityType)
        {
            Text abilityModifierGameObject = null;

            // TODO: Improve this.  Could we use an IDictionary to map TextUI objects to types?
            if (characterAbilityType == typeof(Strength))
            {
                abilityModifierGameObject = _strengthModifier;
            }
            else if (characterAbilityType == typeof(Dexterity))
            {
                abilityModifierGameObject = _dexterityModifier;
            }
            else if (characterAbilityType == typeof(Constitution))
            {
                abilityModifierGameObject = _constitutionModifier;
            }
            else if (characterAbilityType == typeof(Intelligence))
            {
                abilityModifierGameObject = _intelligenceModifier;
            }
            else if (characterAbilityType == typeof(Wisdom))
            {
                abilityModifierGameObject = _wisdomModifier;
            }
            else if (characterAbilityType == typeof(Charisma))
            {
                abilityModifierGameObject = _charismaModifier;
            }

            return abilityModifierGameObject;
        }

        /// <summary>
        /// Returns the attribute's Text GameObject for the specified character attribute type
        /// </summary>
        /// <param name="characterAttributeType">The character attribute type</param>
        /// <returns>The corresponding UI Text GameObject for the attribute</returns>
        private Text GetAttributeGameObject(Type characterAttributeType)
        {
            Text attributerGameObject = null;

            // TODO: Improve this.  Could we use an IDictionary to map TextUI objects to types?
            if (characterAttributeType == typeof(Level))
            {
                attributerGameObject = _level;
            }
            else if (characterAttributeType == typeof(Experience))
            {
                attributerGameObject = _experience;
            }
            else if (characterAttributeType == typeof(HitDie))
            {
                attributerGameObject = _hitDie;
            }
            else if (characterAttributeType == typeof(Health))
            {
                attributerGameObject = _health;
            }

            return attributerGameObject;
        }

        /// <summary>
        /// Clears the racial ability modifiers
        /// </summary>
        private void ClearRacialAbilityModifiers()
        {
            ClearRacialAbilityModifier(_strengthRacialModifier);
            ClearRacialAbilityModifier(_dexterityRacialModifier);
            ClearRacialAbilityModifier(_constitutionRacialModifier);
            ClearRacialAbilityModifier(_intelligenceRacialModifier);
            ClearRacialAbilityModifier(_wisdomRacialModifier);
            ClearRacialAbilityModifier(_charismaRacialModifier);
        }

        /// <summary>
        /// Clears the ability's racial modifier
        /// </summary>
        /// <param name="racialAbilityModifier">The Text GameObject representing the racial ability modifier</param>
        private void ClearRacialAbilityModifier(Text racialAbilityModifier)
        {
            racialAbilityModifier.text = "0";
        }

        /// <summary>
        /// Returns the selected character race
        /// </summary>
        /// <returns>A character race corresponding to the race selection</returns>
        private RaceBase GetCharacterRace()
        {
            Dropdown.OptionData optionData = GetSelectedDropdownOption(_characterRace);

            RaceBase characterRace = CharacterCreator.FindCharacterRace(optionData.text);

            return characterRace;
        }

        /// <summary>
        /// Returns the selected character class
        /// </summary>
        /// <returns>A character class corresponding to the class selection</returns>
        private ClassBase GetCharacterClass()
        {
            Dropdown.OptionData optionData = GetSelectedDropdownOption(_characterClass);

            ClassBase characterClass = CharacterCreator.GetCharacterClass(optionData.text);

            return characterClass;
        }

        /// <summary>
        /// Returns the selected character ability
        /// </summary>
        /// <returns>A character ability corresponding to the ability selection</returns>
        private AbilityBase GetCharacterAbility()
        {
            Dropdown.OptionData optionData = GetSelectedDropdownOption(_characterAbility);

            AbilityBase characterAbility = CharacterCreator.FindCharacterAbility(optionData.text);

            return characterAbility;
        }

        /// <summary>
        /// Returns the currently selected option from the specified dropdown menu
        /// </summary>
        /// <param name="dropdown">The dropdown menu</param>
        /// <returns>OptionData for the selected option in the specified dropdown menu</returns>
        private Dropdown.OptionData GetSelectedDropdownOption(Dropdown dropdown)
        {
            Dropdown.OptionData selectedOption = dropdown.options[dropdown.value];

            return selectedOption;
        }

        /// <summary>
        /// Parses ability score
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>An integer representing the ability score value</returns>
        private int ParseAbilityScore(Type characterAbilityType)
        {
            Text ability = GetAbilityGameObject(characterAbilityType);

            return CharacterCreator.ParseInput(ability.text);
        }

        /// <summary>
        /// Parses racial ability modifier
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>An integer representing the racial ability modifier value</returns>
        private int ParseRacialAbilityModifier(Type characterAbilityType)
        {
            Text racialAbilityModifier = GetAbilityRacialModifierGameObject(characterAbilityType);

            return CharacterCreator.ParseInput(racialAbilityModifier.text);
        }

        /// <summary>
        /// Parses ability modifier
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>An integer representing the ability modifier value</returns>
        private int ParseAbilityModifier(Type characterAbilityType)
        {
            Text abilityModifier = GetAbilityModifierGameObject(characterAbilityType);

            return CharacterCreator.ParseInput(abilityModifier.text);
        }

        /// <summary>
        /// Parses attribute
        /// </summary>
        /// <param name="characterAttributeType">The character attribute type</param>
        /// <returns>An integer representing the attribute value</returns>
        private int ParseAttribute(Type characterAttributeType)
        {
            Text attribute = GetAttributeGameObject(characterAttributeType);

            return CharacterCreator.ParseInput(attribute.text);
        }

        /// <summary>
        /// Returns the sum of the character's ability score and racial ability modifier
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>The base ability score for the specified character ability</returns>
        private int CalculateBaseAbility(Type characterAbilityType)
        {
            int abilityScore = ParseAbilityScore(characterAbilityType);
            int racialAbilityModifier = ParseRacialAbilityModifier(characterAbilityType);

            return CharacterCreator.CalculateBaseAbility(abilityScore, racialAbilityModifier);
        }

        /// <summary>
        /// Populate level 1 character attributes
        /// </summary>
        private void PopulateLevel1Attributes()
        {
            _level.text = 1.ToString();
            _experience.text = 0.ToString();
        }
    }
}