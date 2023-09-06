using System.Runtime.InteropServices;
using Godot;

namespace Stomper
{
    public class DustFXAnim : Node2D
    {
        [Export()]private NodePath _animNodePath;
        [Export()] private NodePath _spritesPath;

        private AnimationPlayer _animPlayer;
        private Node2D _sprites;

        [Signal]
        private delegate void AnimationFinished(string animName, DustFX dustFx);

        public override void _Ready()
        {
            _animPlayer = GetNode(_animNodePath) as AnimationPlayer;
            _sprites = GetNode(_spritesPath) as Node2D;
            foreach (Sprite sprite in _sprites.GetChildren())
            {
                sprite.Visible = false;
            }
        }

        public void PlayAnimation(string animName)
        {
            _animPlayer.Play(animName);
        }

        private void _on_AnimationPlayer_animation_finished(string animName)
        {
            EmitSignal(nameof(AnimationFinished), animName, this);
           
        }
    }
}
