using Godot;

namespace Stomper
{
    public class PlayerInputHandler : Node
    {
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool JumpInputStop { get; private set; }

        [Export()] private float _inputHoldTime = 0.1f;
        private float _jumpInputStartTime;
        private Globals _globals;

        public override void _EnterTree()
        {
            base._EnterTree();
            _globals = _globals = GetNode<Globals>("/root/Globals");
        }

        public override void _PhysicsProcess (float delta)
        {
            base._PhysicsProcess(delta);
            if (!_globals.PlayerInputEnabled) return;
            CheckForJumpInput();
            CheckForHorizontalInput();
            CheckForVerticalInput();
        }
        
        private void CheckForHorizontalInput() =>
            HorizontalInput = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        private void CheckForVerticalInput() =>
            VerticalInput = Input.GetActionStrength("up") - Input.GetActionStrength("down");
        
        private void CheckForJumpInput()
        {
            CheckJumpInputHoldTime();
            if (Input.IsActionJustPressed("jump"))
            {
                JumpInput = true;
                JumpInputStop = false;
                _jumpInputStartTime = OS.GetTicksUsec();
            }

            if (Input.IsActionJustReleased("jump"))
            {
                JumpInputStop = true;
            }
        }

        public void UseJumpInput() => JumpInput = false;

        private void CheckJumpInputHoldTime()
        {
            if (OS.GetTicksUsec() >= _jumpInputStartTime + _inputHoldTime)
            {
                JumpInput = false;
            }
        }
        
    }
}