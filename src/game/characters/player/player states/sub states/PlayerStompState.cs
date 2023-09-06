namespace Stomper
{
    public class PlayerStompState: PlayerAbilityState
    {
        private bool _isGrounded;

        public PlayerStompState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.motion.y = playerData.stompSpeed;
            player.StartDashFx();
        }

        public override void PhysicsUpdate(float delta)
        {
            // base.PhysicsUpdate(delta);
            _isGrounded = player.IsGrounded;
            if(_isGrounded && player.motion.y < 0.01f) playerFSM.ChangeState(player.LandState);
        }

        public override void Exit()
        {
            base.Exit();
            isAbilityDone = true;
            player.StopDashFx();
            player.ShowDustFx(animName);
            player.GlobalEvents.EmitSignal(nameof(GlobalEvents.StompEvent), 0.7f);
        }

        public override void LogicUpdate(float delta)
        {
            base.LogicUpdate(delta);
        }
    }
}