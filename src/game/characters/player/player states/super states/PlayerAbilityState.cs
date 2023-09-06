using Godot;

namespace Stomper
{
    public class PlayerAbilityState: PlayerState
    {
        protected bool isAbilityDone;
        protected bool isGrounded;
        protected bool jumpInput;
        protected float verticalInput;
        
        public PlayerAbilityState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            isAbilityDone = false;
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

            isGrounded = player.IsGrounded;
            jumpInput = player.InputHandler.JumpInput;
            verticalInput = player.InputHandler.VerticalInput;
            
            if (isAbilityDone)
            {
                if(isGrounded) playerFSM.ChangeState(player.IdleState);
                else if(playerFSM.CurrentState != player.StompState)playerFSM.ChangeState(player.InAirState);
            }
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}