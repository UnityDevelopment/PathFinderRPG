namespace PathfinderRPG.Entities.Classes
{
    public class Sorcerer : ClassBase
    {
        /// <summary>
        /// Sets the display name of the character class
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Sorcerer";
        }

        /// <summary>
        /// Sets the hit die of the character class
        /// </summary>
        protected override void SetHitDie()
        {
            HitDie = Dice.DieType.D6;
        }
    }
}
