using Godot;

namespace Stomper
{
    public class TutorialLevelTransition : Interactible
    {
        
        protected override void BodyEnteredAction(Player _player)
        {
            base.BodyEnteredAction(_player);
            globalEvents.EmitSignal(nameof(GlobalEvents.LevelFinishedEvent));
        }
    }
}
