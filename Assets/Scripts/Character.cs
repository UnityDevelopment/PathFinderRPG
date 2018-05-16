using UnityEngine;

namespace PathFinderRPG
{
    public class Character : ScriptableObject
    {
        private int _baseStrength;
        private int _baseDexterity;
        private int _baseConstitution;
        private int _baseIntelligence;
        private int _baseWisdom;
        private int _baseCharisma;

        private int _baseHealth;

        private int _modifiedStrength;
        private int _modifiedDexterity;
        private int _modifiedConstitution;
        private int _modifiedIntelligence;
        private int _modifiedWisdom;
        private int _modifiedCharisma;

        private int _strengthModifier;
        private int _dexterityModifier;
        private int _constitutionModifier;
        private int _intelligenceModifier;
        private int _wisdomModifier;
        private int _charismaModifier;

        private CharacterClass _characterClass;
        private CharacterRace _characterRace;

        private int _health;

        private int _level;
        private int _experience;

        private Dice.DieType _hitDie;


        /// <summary>
        /// Initialises the character
        /// </summary>
        /// <param name="strength">The character's strength ability's base score</param>
        /// <param name="dexterity">The character's dexterity ability's base score</param>
        /// <param name="constitution">The character's constitution ability's base score</param>
        /// <param name="intelligence">The character's intelligence ability's base score</param>
        /// <param name="wisdom">The character's wisdom ability's base score</param>
        /// <param name="charisma">The character's charisma ability's base score</param>
        /// <param name="strengthModifier">The character's strength modifier</param>
        /// <param name="dexterityModifier">The character's dexterity modifier</param>
        /// <param name="constitutionModifier">The character's constitution modifier</param>
        /// <param name="intelligenceModifier">The character's intelligence modifier</param>
        /// <param name="wisdomModifier">The character's wisdom modifier</param>
        /// <param name="charismaModifier">The character's charisma modifier</param>
        /// <param name="characterClass">The character's class</param>
        /// <param name="characterRace">The character's race</param>
        private void Init
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
            _baseStrength = strength;
            _baseDexterity = dexterity;
            _baseConstitution = constitution;
            _baseIntelligence = intelligence;
            _baseWisdom = wisdom;
            _baseCharisma = charisma;

            _modifiedStrength = _baseStrength;
            _modifiedDexterity = _baseDexterity;
            _modifiedConstitution = _baseConstitution;
            _modifiedIntelligence = _baseIntelligence;
            _modifiedWisdom = _baseWisdom;
            _modifiedCharisma = _baseCharisma;

            _strengthModifier = strengthModifier;
            _dexterityModifier = dexterityModifier;
            _constitutionModifier = constitutionModifier;
            _intelligenceModifier = intelligenceModifier;
            _wisdomModifier = wisdomModifier;
            _charismaModifier = charismaModifier;

            _characterClass = characterClass;
            _characterRace = characterRace;

            _level = 1;
            _experience = 0;

            SetClassSpecificAttributes();
        }

        /// <summary>
        /// Sets additional character attributes specific to its class
        /// </summary>
        private void SetClassSpecificAttributes()
        {
            SetHitDie();
            SetHealth();
        }

        /// <summary>
        /// Sets the character's hit die based on its class
        /// </summary>
        private void SetHitDie()
        {
            switch (_characterClass)
            {
                case CharacterClass.Barbarian:

                    _hitDie = Dice.DieType.D12;

                    break;

                case CharacterClass.Bard:

                    _hitDie = Dice.DieType.D8;

                    break;

                case CharacterClass.Cleric:

                    _hitDie = Dice.DieType.D8;

                    break;

                case CharacterClass.Druid:

                    _hitDie = Dice.DieType.D8;

                    break;

                case CharacterClass.Fighter:

                    _hitDie = Dice.DieType.D10;

                    break;

                case CharacterClass.Monk:

                    _hitDie = Dice.DieType.D8;

                    break;

                case CharacterClass.Paladin:

                    _hitDie = Dice.DieType.D10;

                    break;

                case CharacterClass.Ranger:

                    _hitDie = Dice.DieType.D10;

                    break;

                case CharacterClass.Rogue:

                    _hitDie = Dice.DieType.D8;

                    break;

                case CharacterClass.Sorcerer:

                    _hitDie = Dice.DieType.D6;

                    break;

                case CharacterClass.Wizard:

                    _hitDie = Dice.DieType.D6;

                    break;
            }
        }

        /// <summary>
        /// Sets the character's health based on its hit die
        /// </summary>
        private void SetHealth()
        {
            switch (_characterClass)
            {
                case CharacterClass.Barbarian:

                    _baseHealth = (int)Dice.DieType.D12;

                    break;

                case CharacterClass.Bard:

                    _baseHealth = (int)Dice.DieType.D8;

                    break;

                case CharacterClass.Cleric:

                    _baseHealth = (int)Dice.DieType.D8;

                    break;

                case CharacterClass.Druid:

                    _baseHealth = (int)Dice.DieType.D8;

                    break;

                case CharacterClass.Fighter:

                    _baseHealth = (int)Dice.DieType.D10;

                    break;

                case CharacterClass.Monk:

                    _baseHealth = (int)Dice.DieType.D8;

                    break;

                case CharacterClass.Paladin:

                    _baseHealth = (int)Dice.DieType.D10;

                    break;

                case CharacterClass.Ranger:

                    _baseHealth = (int)Dice.DieType.D10;

                    break;

                case CharacterClass.Rogue:

                    _baseHealth = (int)Dice.DieType.D8;

                    break;

                case CharacterClass.Sorcerer:

                    _baseHealth = (int)Dice.DieType.D6;

                    break;

                case CharacterClass.Wizard:

                    _baseHealth = (int)Dice.DieType.D6;

                    break;
            }

            _health = _baseHealth;
        }

        /// <summary>
        /// Creates an instance of the Character
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
        public static Character CreateInstance
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
            Character character = ScriptableObject.CreateInstance<Character>();

            character.Init
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
    }
}