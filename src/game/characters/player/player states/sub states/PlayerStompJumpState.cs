using Godot;

namespace Stomper
{
    public class PlayerStompJumpState: PlayerAbilityState
    {
        public PlayerStompJumpState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.motion.y = -playerData.stompSpeed;
            player.ShowDustFx("jump");
            player.StartDashFx();
            
        }

        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            // if(isGrounded && player.motion.y < 0.01f) playerFSM.ChangeState(player.LandState);
            for (int i = 0; i < player.GetSlideCount(); i++)
            {
                var collision = player.GetSlideCollision(i);

                if (!player.IsGrounded && collision.Collider != null)
                {
                    player.GlobalEvents.EmitSignal(nameof(GlobalEvents.StompEvent), 0.7f);
                    playerFSM.ChangeState(player.FallState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            player.StopDashFx();
            player.ShowDustFx("stomp_jump");
        }
    }
}