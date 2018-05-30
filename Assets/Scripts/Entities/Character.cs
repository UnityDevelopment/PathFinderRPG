namespace PathfinderRPG.Entities
{
    using PathfinderRPG.Entities.Races;
    using PathfinderRPG.Entities.Classes;

    using UnityEngine;

    public class Character : EntityBase
    {
        private int _baseStrength;
        private int _baseDexterity;
        private int _baseConstitution;
        private int _baseIntelligence;
        private int _baseWisdom;
        private int _baseCharisma;

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

        private RaceBase _characterRace;
        private ClassBase _characterClass;

        private int _baseHealth;
        private int _health;

        private int _level;
        private int _experience;

        /// <summary>
        /// Prevents a default instance of the <see cref="Character" /> class from being created
        /// </summary>
        private Character()
        {

        }

        /// <summary>
        /// Gets the health of the character
        /// </summary>
        public int Health
        {
            get { return _health; }
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
        /// <param name="characterClass">The character's class</param>
        /// <param name="level">The character's level</param>
        /// <param name="experience">The character's experience</param>
        /// <param name="hitDie">The character's hit die</param>
        /// <param name="health">The character's base health</param>
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
               RaceBase characterRace,
               ClassBase characterClass,
               int level,
               int experience
            )
        {
            Character character = ScriptableObject.CreateInstance<Character>();

            character.Initialise
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
                    characterClass,
                    level,
                    experience
                );

            return character;
        }

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
        /// <param name="characterRace">The character's race</param>
        /// <param name="characterClass">The character's class</param>
        /// <param name="level">The character's level</param>
        /// <param name="experience">The character's experience</param>
        private void Initialise
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
            ClassBase characterClass,
            int level,
            int experience
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

            _characterRace = characterRace;
            _characterClass = characterClass;

            _level = level;
            _experience = experience;

            CalculateHealth();
        }

        /// <summary>
        /// Calculates the health of the character
        /// </summary>
        private void CalculateHealth()
        {
            // NOTE: Assumes level 1 "new" character creation
            _baseHealth = (int)_characterClass.HitDie;
            _health = _baseHealth;
        }
    }
}
