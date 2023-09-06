using Godot;

namespace Stomper
{
    public class PlayerIdleState: PlayerGroundedState
    {
        public PlayerIdleState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // player.Motion = Vector2.Zero;
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate(float delta)
        {
            base.LogicUpdate(delta);
            // GD.Print(Player.InputHandler.HorizontalInput);
 
        }

        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            
            if (verticalInput < 0)
            {
                playerFSM.ChangeState(player.DuckState);
            }
            else if (horizontalInput != 0f)
            {
                playerFSM.ChangeState(player.MoveState);
            }

            
            
            player.motion.x = Mathf.Lerp(player.motion.x, 0, playerData.friction);
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}