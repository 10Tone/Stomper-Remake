using Godot;

namespace Stomper
{
    public class PlayerStompJumpLoadingState: PlayerAbilityState
    {

        public PlayerStompJumpLoadingState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.StompJumpEnabled = true;
        }

        public override void Exit()
        {
            base.Exit();
            if (player.CollidingWithWall)
            {
                GD.Print("is on wall");
                player.GlobalPosition = new Vector2(player.GlobalPosition.x + 1 * -player.FacingDirection,
                    player.GlobalPosition.y);
            }
        }

        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            
            jumpInput = player.InputHandler.JumpInput;

            if(jumpInput)
            {
                playerFSM.ChangeState(player.StompJumpState);
            }
            else if (verticalInput >= 0)
            {
                isAbilityDone = true;
            }
        }
    }
}