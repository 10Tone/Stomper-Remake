using Godot;

namespace Stomper
{
    public class DustFX : Node2D
    {
        [Export()]private PackedScene _dustFxPackedScene;
        

        private Globals _globals;
        // private bool _canPlayAnim = true;


        public override void _Ready()
        {
            _globals = GetNode<Globals>("/root/Globals");
        }

        public void ShowDustFx(Vector2 position, string animName)
        {
            // if (!_canPlayAnim) return;
            var dust = _dustFxPackedScene.Instance() as DustFXAnim;
            _globals.CurrentLevel.AddChild(dust);
            dust.Connect("AnimationFinished", this, nameof(OnAnimationFinished));
            dust.GlobalPosition = position;
            // dust.CallDeferred("PlayAnimation", animName);
            // GD.Print("playing: " + animName + " animation");
            dust.PlayAnimation(animName);
            // _canPlayAnim = false;
        }

        private void OnAnimationFinished(string animName, DustFX dust)
        {
            dust.QueueFree();
            // _canPlayAnim = true;
        }
    }
}
