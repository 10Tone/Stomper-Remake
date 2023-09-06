using System.Threading.Tasks;
using Godot;

namespace Stomper
{
    public class PlayerLandState: PlayerGroundedState
    {
        public PlayerLandState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // play land particles or land animation
            player.ShowDustFx(animName);

        }

        public override void Exit()
        {
            base.Exit();
            
        }

        public override void PhysicsUpdate(float delta)
        {
            if (!player.IsGrounded) return;
            if(horizontalInput != 0f) playerFSM.ChangeState(player.MoveState);
            else playerFSM.ChangeState(player.IdleState);
        }
        
    }
}