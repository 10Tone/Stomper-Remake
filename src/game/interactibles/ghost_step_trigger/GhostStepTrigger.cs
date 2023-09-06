using Godot;

namespace Stomper
{
    public class GhostStepTrigger : Interactible
    {

        [Signal]
        public delegate void TriggerActivatedEvent();

        protected override void BodyEnteredAction(Player _player)
        {
            base.BodyEnteredAction(_player);
            GD.Print("player entered");
            EmitSignal(nameof(TriggerActivatedEvent));
            
        }
    }
}
