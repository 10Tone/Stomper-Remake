using Godot;

namespace Stomper
{
    public class BreakableBlock: StaticBody2D
    {
        [Export()] protected PackedScene rubblePackedScene;
        [Export()] protected Texture rubbleTexture;
        
        protected Globals globals;
        protected RandomNumberGenerator rndm;

        public override void _EnterTree()
        {
            base._EnterTree();
            globals = GetNode<Globals>("/root/Globals");
            rndm = new RandomNumberGenerator();
        }
        
        public void _on_Area2D_body_entered(Node body)
        {
            if (body is KinematicBody2D && body.IsInGroup("Player"))
            {
                var player = body as Player;
                // GD.Print(player?.PlayerFSM.CurrentState);
                if (player?.PlayerFSM.CurrentState == player?.StompState || player?.PlayerFSM.CurrentState == player?.StompJumpState)
                {
                    BodyEnteredAction(player);
                }
            }
        }

        protected virtual void BodyEnteredAction(Player player)
        {
            CallDeferred(nameof(BreakIntoRubble));
        }

        protected void FreeNode()
        {
            CallDeferred("queue_free");
        }
        
        private void BreakIntoRubble()
        {
            rndm.Randomize();
            for (int i = 0; i < 2; i++)
            {
                var rubble = rubblePackedScene.Instance() as BrickRubble;
                globals.AddChild(rubble);
                rubble.GlobalPosition = GlobalPosition;
                rubble.rotation = 1f;
                rubble.horizontalDirection = rndm.RandfRange(50 * rubble.rotation, 00);
                rubble.ApplyImpulse(rubbleTexture);
            }
            
            for (int i = 0; i < 2; i++)
            {
                var rubble = rubblePackedScene.Instance() as BrickRubble;
                globals.AddChild(rubble);
                rubble.GlobalPosition = GlobalPosition;
                rubble.rotation = -1f;
                rubble.horizontalDirection = rndm.RandfRange(50 * rubble.rotation, 0);
                rubble.ApplyImpulse(rubbleTexture);
            }
            
            
        }
    }
}