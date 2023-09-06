using Godot;

namespace Stomper
{
    public class PlayerAudioHandler : Node2D
    {
        [Export()] private NodePath _jumpAudioPath;

        private AudioStreamPlayer _jumpAudio;

        public override void _Ready()
        {
            _jumpAudio = GetNode(_jumpAudioPath) as AudioStreamPlayer;
        }
    }
}
