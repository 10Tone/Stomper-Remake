using Godot;

namespace Stomper
{
    public class Portal : Interactible
    {
        [Export()] private NodePath _animNodePath, _lightPath;
            

        private AnimatedSprite _anim;
        private Light2D _light;
        private bool _levelCleared;
        private LevelManager _currentLevel;
        

        public override void _EnterTree()
        {
            base._EnterTree();
            globalEvents.Connect(nameof(GlobalEvents.LevelClearedEvent), this, nameof(OnLevelCleared));
        }

        public override void _Ready()
        {
            base._Ready();
            _levelCleared = false;
            _anim = GetNode(_animNodePath) as AnimatedSprite;
            _light = GetNode(_lightPath) as Light2D;
            _anim?.Play("idle");
            _light.Enabled = false;
        }

        protected override void BodyEnteredAction(Player _player)
        {
            base.BodyEnteredAction(_player);
            if(_levelCleared && _currentLevel != null) globalEvents.EmitSignal(nameof(GlobalEvents.LevelFinishedEvent));
        }

        private void OnLevelCleared(LevelManager level)
        {
            _anim.Play("active");
            _levelCleared = true;
            _currentLevel = level;
            _light.Enabled = true;
            // globalEvents.EmitSignal(nameof(GlobalEvents.CameraZoomEvent), GlobalPosition);
        }
        
        
    }
}
