namespace Stomper
{
    public class PlayerFallState: PlayerInAirState
    {
        public PlayerFallState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            isFalling = true;
        }
    }
}