using Godot;

namespace Stomper
{
    public class Crate : BreakableBlock
    {
        
        [Signal]
        public delegate void TriggerActivatedEvent();
        
        protected override void BodyEnteredAction(Player player)
        {
            base.BodyEnteredAction(player);
            EmitSignal(nameof(TriggerActivatedEvent));
            FreeNode();
        }
    }
}
