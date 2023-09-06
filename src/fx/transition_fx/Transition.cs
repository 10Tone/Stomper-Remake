using Godot;

namespace Stomper
{
    public class Transition : ColorRect
    {
        [Export()] private NodePath _animPlayerPath;

        private AnimationPlayer _animPlayer;

        public override void _Ready()
        {
            _animPlayer = GetNode(_animPlayerPath) as AnimationPlayer;
            
        }

        public float PlayTransitionAnimation(string animName)
        {
            _animPlayer.Play(animName);
            return _animPlayer.CurrentAnimationLength;
        }
        
    }
}
