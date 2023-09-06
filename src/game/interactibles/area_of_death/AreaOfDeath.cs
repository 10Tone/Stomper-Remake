namespace Stomper
{
    public class AreaOfDeath : Interactible
    {
        protected override void BodyEnteredAction(Player _player)
        {
            globalEvents.EmitSignal(nameof(GlobalEvents.PlayerDiedEvent));
        }
    }
}
