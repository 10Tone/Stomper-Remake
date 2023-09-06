using Godot;

namespace Stomper
{
    public class PlayerDuckState: PlayerGroundedState
    {
        private float _downInputStartTime;
        private float _stompJumpTreshold;
        private const float StompJumpTime = 100f;
        
        public PlayerDuckState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _downInputStartTime = OS.GetTicksMsec();
            _stompJumpTreshold = StompJumpTime + _downInputStartTime;
        }

        public override void Exit()
        {
            base.Exit();
      
        }

        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            
            if (verticalInput >= 0)
            {
                playerFSM.ChangeState(player.IdleState);
            }
            player.motion.x = Mathf.Lerp(player.motion.x, 0, playerData.friction);

            if (OS.GetTicksMsec() >= _stompJumpTreshold)
            {
                playerFSM.ChangeState(player.StompJumpLoadingState);
            }
        }
    }
}