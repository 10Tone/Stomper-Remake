namespace Stomper
{
    public class PlayerJumpState: PlayerAbilityState
    {
        private int _amountOfJumpsLeft;
        private float jumpforce;
        public int AmountOfJumpsLeft
        {
            get => _amountOfJumpsLeft;
            private set => _amountOfJumpsLeft = value;
        }
        
        public PlayerJumpState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName) : base(player, playerFSM, playerData, animName)
        {
            _amountOfJumpsLeft = playerData.amountOfJumps;
            jumpforce = playerData.jumpForce;
        }

        public override void Enter()
        {
            base.Enter();
            // if (player.StompJumpEnabled)
            // {
            //     player.motion.y = -jumpforce * 5;
            //     _amountOfJumpsLeft--;
            //     player.ShowDustFx(animName);
            //     playerFSM.ChangeState(player.StompJumpState);
            // }
            player.motion.y = -jumpforce;
            
            _amountOfJumpsLeft--;
            if (_amountOfJumpsLeft == playerData.amountOfJumps - 1)
            {
                player.ShowDustFx(animName);
            }
            
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void PhysicsUpdate(float delta)
        {
            base.PhysicsUpdate(delta);
            if(!isGrounded) isAbilityDone = true;
        }

        public bool CanJump() => _amountOfJumpsLeft > 0;

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = playerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

    }
}