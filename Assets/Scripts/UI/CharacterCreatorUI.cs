﻿namespace PathfinderRPG.UI
{
    using System;
    using System.Collections.Generic;

    using PathfinderRPG.Entities;
    using PathfinderRPG.Entities.Abilities;
    using PathfinderRPG.Entities.Attributes;
    using PathfinderRPG.Entities.Classes;
    using PathfinderRPG.Entities.Races;
    using PathfinderRPG.Entities.Races.Languages;
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

        [Space(10f)]

        [Header("Roll for Ability Buttons")]
        public GameObject _rollForStrength;
        public GameObject _rollForDexterity;
        public GameObject _rollForConstitution;
        public GameObject _rollForIntelligence;
        public GameObject _rollForWisdom;
        public GameObject _rollForCharisma;

        [Space(10f)]

        [Header("Race")]
        public Dropdown _characterRace;

        [Space(10f)]

        public GameObject _knownLanguagesContainer;
        public GameObject _knownLanguages;

        [Space(10f)]
        public GameObject _learnableLanguagesContainer;
        public GameObject _learnableLanguages;

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
            DisableButton(typeof(Strength));
            UpdateAbilityScore(typeof(Strength));
            UpdateAbilityModifier(typeof(Strength));
        }

        /// <summary>
        /// Event handler for dexterity ability
        /// </summary>
        public void RollDexterity()
        {
            DisableButton(typeof(Dexterity));
            UpdateAbilityScore(typeof(Dexterity));
            UpdateAbilityModifier(typeof(Dexterity));
        }

        /// <summary>
        /// Event handler for constitution ability
        /// </summary>
        public void RollConstitution()
        {
            DisableButton(typeof(Constitution));
            UpdateAbilityScore(typeof(Constitution));
            UpdateAbilityModifier(typeof(Constitution));
        }

        /// <summary>
        /// Event handler for intelligence ability
        /// </summary>
        public void RollIntelligence()
        {
            DisableButton(typeof(Intelligence));
            UpdateAbilityScore(typeof(Intelligence));
            UpdateAbilityModifier(typeof(Intelligence));

            ToggleLearnableLanguages();
        }

        /// <summary>
        /// Event handler for wisdom ability
        /// </summary>
        public void RollWisdom()
        {
            DisableButton(typeof(Wisdom));
            UpdateAbilityScore(typeof(Wisdom));
            UpdateAbilityModifier(typeof(Wisdom));
        }

        /// <summary>
        /// Event handler for charisma ability
        /// </summary>
        public void RollCharisma()
        {
            DisableButton(typeof(Charisma));
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

            UpdateKnownLanguages();
            UpdateLearnableLanguages();
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
            List<Language> bonusLanguages = GetBonusLanguages();
            ClassBase characterClass = GetCharacterClass();
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
                    bonusLanguages,
                    characterClass,
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
        /// Disables the roll for ability button for the specified ability type
        /// </summary>
        /// <param name="abilityType">The character ability type</param>
        public void DisableButton(Type abilityType)
        {
            GameObject buttonGameobject = GetAbilityButtonGameObject(abilityType);

            Button button = buttonGameobject.GetComponent<Button>();

            button.interactable = false;
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

            ToggleLearnableLanguages();
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

        // TODO: CharacterRace is being used at least twice now, it would be more efficient to store the 
        //       selection in a local variable or start to build an object to hold the values than retrieve
        //       them each time they are required.

        /// <summary>
        /// Updates the content of the known languages listbox for the currently selected character race
        /// </summary>
        private void UpdateKnownLanguages()
        {
            RaceBase characterRace = GetCharacterRace();
            List<Dropdown.OptionData> options = GetOptionData(characterRace.KnownLanguages);

            ListBox knownLanguages = _knownLanguages.GetComponent<ListBox>();

            knownLanguages.ClearOptions();
            knownLanguages.AddOptions(options, false);
        }

        /// <summary>
        /// Updates the content of the learnable languages listbox for the currently selected character race
        /// </summary>
        private void UpdateLearnableLanguages()
        {
            RaceBase characterRace = GetCharacterRace();
            List<Dropdown.OptionData> options = GetOptionData(characterRace.LearnableLanguages);

            ListBox learnableLanguages = _learnableLanguages.GetComponent<ListBox>();

            learnableLanguages.ClearOptions();
            learnableLanguages.AddOptions(options, true);
        }

        /// <summary>
        /// Enables or disables the learnable languages listbox
        /// </summary>
        private void ToggleLearnableLanguages()
        {
            RaceBase characterRace = GetCharacterRace();
            int modifier = ParseAbilityModifier(typeof(Intelligence));

            if (modifier > 0 && characterRace)
            {
                _learnableLanguagesContainer.SetActive(true);
            }
            else
            {
                _learnableLanguagesContainer.SetActive(false);
            }
        }

        /// <summary>
        /// Adds the selected language from the learnable languages ListBox to the known languages ListBox
        /// </summary>
        /// <param name="index">The index value of the language</param>
        public void AddKnownLanguage(int index)
        {
            ListBox learnableLanguages = _learnableLanguages.GetComponent<ListBox>();
            ListBox knownLanguages = _knownLanguages.GetComponent<ListBox>();
            ListItem listItem = learnableLanguages.GetListItem(index);

            if (listItem.IsRemoveable)
            {
                ListBox.MoveItem(index, learnableLanguages, knownLanguages, true);
            }

            // TODO: The following code prevents more bonus languages been added than should be, needs moving/refactoring
            RaceBase characterRace = GetCharacterRace();

            int initialLanguages = characterRace.KnownLanguages.Count;
            int addedLanguages = knownLanguages.Options.Count - initialLanguages;
            int intelligenceModifier = ParseAbilityModifier(typeof(Intelligence));

            // all bonus languages have been added
            if (addedLanguages == intelligenceModifier)
            {
                // set all learnable languages to IsRemoveable = false
                foreach(Transform option in learnableLanguages.content.transform)
                {
                    listItem = option.GetComponent<ListItem>();
                    listItem.IsRemoveable = false;
                }
            }
        }

        /// <summary>
        /// Removes the selected language from the known languages ListBox, replacing back into the learnable languages ListBox, if the language is removeable
        /// </summary>
        /// <param name="index">The index value of the language</param>
        public void RemoveKnownLanguage(int index)
        {
            ListBox knownLanguages = _knownLanguages.GetComponent<ListBox>();
            ListBox learnableLanguages = _learnableLanguages.GetComponent<ListBox>();
            ListItem listItem = knownLanguages.GetListItem(index);

            if (listItem.IsRemoveable)
            {
                ListBox.MoveItem(index, knownLanguages, learnableLanguages, true);
            }

            // TODO: The following code prevents more bonus languages been added than should be, needs moving/refactoring
            RaceBase characterRace = GetCharacterRace();

            int initialLanguages = characterRace.KnownLanguages.Count;
            int addedLanguages = knownLanguages.Options.Count - initialLanguages;
            int intelligenceModifier = ParseAbilityModifier(typeof(Intelligence));

            // all bonus languages have been added
            if (addedLanguages < intelligenceModifier)
            {
                // set all learnable languages to IsRemoveable = true
                foreach (Transform option in learnableLanguages.content.transform)
                {
                    listItem = option.GetComponent<ListItem>();
                    listItem.IsRemoveable = true;
                }
            }
        }

        /// <summary>
        /// Updates class specific attributes for the specific character class
        /// </summary>
        /// <param name="characterClass">The character class</param>
        private void UpdateAttributes(ClassBase characterClass)
        {
            // TODO: Displayed health will need to be calculated differently if level selection is available later
            UpdateAttribute(typeof(Health), (int)characterClass.HitDie);
            UpdateAttribute(typeof(Level), characterClass.Level);

            // TODO: Hard coded value of 0 
            UpdateAttribute(typeof(Experience), 0);
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
        /// Returns the ability's button GameObject for the specified character ability type
        /// </summary>
        /// <param name="characterAbilityType">The character ability type</param>
        /// <returns>The corresponding button GameObject for the character ability's</returns>
        private GameObject GetAbilityButtonGameObject(Type characterAbilityType)
        {
            GameObject abilityButtonGameObject = null;

            // TODO: Improve this.  Could we use an IDictionary to map TextUI objects to types?
            if (characterAbilityType == typeof(Strength))
            {
                abilityButtonGameObject = _rollForStrength;
            }
            else if (characterAbilityType == typeof(Dexterity))
            {
                abilityButtonGameObject = _rollForDexterity;
            }
            else if (characterAbilityType == typeof(Constitution))
            {
                abilityButtonGameObject = _rollForConstitution;
            }
            else if (characterAbilityType == typeof(Intelligence))
            {
                abilityButtonGameObject = _rollForIntelligence;
            }
            else if (characterAbilityType == typeof(Wisdom))
            {
                abilityButtonGameObject = _rollForWisdom;
            }
            else if (characterAbilityType == typeof(Charisma))
            {
                abilityButtonGameObject = _rollForCharisma;
            }

            return abilityButtonGameObject;
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
        /// Returns a list of OptionData objects for the specified languages
        /// </summary>
        /// <param name="languages">The list of languages</param>
        /// <returns>A list of languages as OptionData objects</returns>
        private List<Dropdown.OptionData> GetOptionData(List<Language> languages)
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

            foreach (Language language in languages)
            {
                Dropdown.OptionData option = new Dropdown.OptionData(language.DisplayName);

                options.Add(option);
            }

            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Language> GetBonusLanguages()
        {
            List<Language> bonusLanguages = new List<Language>();

            // grab known languages
            ListBox knownLanguages = _knownLanguages.GetComponent<ListBox>();

            // iterate
            foreach(Dropdown.OptionData option in knownLanguages.Options)
            {
                // use name to Find actual language in LanguagesCollection
                Language language = LanguageCollection.FindLanguage(option.text);

                // add language to bonus languages list
                bonusLanguages.Add(language);
            }


            //for(int i = 0; i < knownLanguages.Options.Count; i++)
            //{
            //    //// ignore "isRemoveable = false"
            //    //ListItem listItem = knownLanguages.GetListItem(i);

            //    //if(listItem.IsRemoveable == true)
            //    //{
            //        // use name to Find actual language in LanguagesCollection
            //        Language language = LanguageCollection.FindLanguage(knownLanguages.Options[i].text);
            //        // add language to bonus languages list
            //        bonusLanguages.Add(language);
            //    //}
            //}

            // return list
            return bonusLanguages;
        }
    }
}