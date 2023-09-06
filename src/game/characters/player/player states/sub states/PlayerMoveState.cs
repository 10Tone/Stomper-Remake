using System;
using Godot;

namespace Stomper
{
    public class PlayerMoveState: PlayerGroundedState
    {
        public PlayerMoveState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
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
            
            if (horizontalInput == 0f)
            {
                playerFSM.ChangeState(player.IdleState);
            }
            player.CheckIfShouldFlip(horizontalInput);
            
            Move(delta);
        }

        private void Move(float delta)
        {
            player.motion.x += player.InputHandler.HorizontalInput * playerData.acceleration * delta;
            player.motion.x = Mathf.Clamp(player.motion.x, -playerData.maxSpeed, playerData.maxSpeed);
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}