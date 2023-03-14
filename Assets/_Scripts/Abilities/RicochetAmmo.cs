namespace _Scripts.Abilities
{
    public class RicochetAmmo : AbilityBehaviour
    {
        protected override void Init()
        {
            TargetAbility = AbilityManager.RicochetAmmo;
        }
    }
}
