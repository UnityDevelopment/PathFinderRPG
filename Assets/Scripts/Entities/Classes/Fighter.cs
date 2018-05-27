namespace PathfinderRPG.Entities.Classes
{
    public class Fighter : ClassBase
    {
        /// <summary>
        /// Sets the display name of the character class
        /// </summary>
        protected override void SetDisplayName()
        {
            DisplayName = "Fighter";
        }

        /// <summary>
        /// Sets the hit die of the character class
        /// </summary>
        protected override void SetHitDie()
        {
            HitDie = Dice.DieType.D10;
        }
    }
}
