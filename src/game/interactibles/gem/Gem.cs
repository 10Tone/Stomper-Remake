using Godot;

namespace Stomper.gem
{
    public class Gem : Interactible
    {
        [Export()]private NodePath _animNodePath;
        private AnimationPlayer _anim;

        public override void _EnterTree()
        {
            base._EnterTree();
            globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        }

        public override void _Ready()
        {
            base._Ready();
            _anim = GetNode(_animNodePath) as AnimationPlayer;
            _anim?.Play("idle");
        }

        protected override void BodyEnteredAction(Player _player)
        {
            _anim.Play("picked_up");

        }

        private void _on_AnimationPlayer_animation_finished(string animName)
        {
            if (animName == "picked_up")
            {
                globalEvents.EmitSignal(nameof(GlobalEvents.GemCollectedEvent));
                QueueFree();
            }
            
        }
    }
}
