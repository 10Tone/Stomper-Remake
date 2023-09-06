using Godot;
using Stomper.FSM;

namespace Stomper
{
    public class PlayerGroundedState: PlayerState
    {
        protected float horizontalInput;
        protected float verticalInput;
        private bool _jumpInput;
        private bool _isGrounded;
        public PlayerGroundedState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.JumpState.ResetAmountOfJumpsLeft();
        }
        
        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            _jumpInput = player.InputHandler.JumpInput;
            horizontalInput = player.InputHandler.HorizontalInput;
            verticalInput = player.InputHandler.VerticalInput;
            _isGrounded = player.IsGrounded;
            
            if(_jumpInput && player.JumpState.CanJump())
            {
                player.InputHandler.UseJumpInput();
                playerFSM.ChangeState(player.JumpState);
            }
            else if(!_isGrounded)
            {
                player.InAirState.StartCoyoteTime();
                playerFSM.ChangeState(player.InAirState);
            }
            
        }
        
    }
}