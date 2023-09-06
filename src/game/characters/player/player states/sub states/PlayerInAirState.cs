using Godot;

namespace Stomper
{
    public class PlayerInAirState: PlayerState
    {
        private float _horizontalInput;
        private float _verticalInput;
        private bool _jumpInput;
        private bool _jumpInputStop;
        private bool _isGrounded;
        private bool _coyoteTimeStarted;
        protected bool isFalling;

        public PlayerInAirState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            isFalling = false;
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate(float delta)
        {
            base.LogicUpdate(delta);
            
        }

        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            
            CheckCoyoteTime();
            
            _horizontalInput = player.InputHandler.HorizontalInput;
            _verticalInput = player.InputHandler.VerticalInput;
            _jumpInput = player.InputHandler.JumpInput;
            _jumpInputStop = player.InputHandler.JumpInputStop;
            _isGrounded = player.IsGrounded;
            
            if(_verticalInput < 0f && player.JumpState.AmountOfJumpsLeft == 0) playerFSM.ChangeState(player.StompState);
            else if (player.motion.y >= 0 && !isFalling) playerFSM.ChangeState(player.FallState);
            
            if(_isGrounded && player.motion.y < 0.01f) playerFSM.ChangeState(player.LandState);
            else if (_jumpInput && player.JumpState.CanJump())
            {
                playerFSM.ChangeState(player.JumpState);
            }
            else
            {
                player.CheckIfShouldFlip(_horizontalInput);
                player.motion.x += _horizontalInput * playerData.acceleration * delta;
                player.motion.x = Mathf.Clamp(player.motion.x, -playerData.maxSpeed, playerData.maxSpeed);
            }
        }
        

        public override void DoChecks()
        {
            base.DoChecks();
        }

        private void CheckCoyoteTime()
        {
            if (_coyoteTimeStarted && OS.GetTicksUsec() > startTime + playerData.coyoteTime)
            {
                _coyoteTimeStarted = false;
                player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        public void StartCoyoteTime() => _coyoteTimeStarted = true;
        
    }
}