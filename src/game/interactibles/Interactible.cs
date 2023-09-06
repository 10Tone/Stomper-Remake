using Godot;

namespace Stomper
{
    public class Interactible : Area2D
    {
        protected GlobalEvents globalEvents;

        public override void _EnterTree()
        {
            globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        }

        private void _on_Interactible_body_entered(Node _body)
        {
            if (_body is KinematicBody2D && _body.IsInGroup("Player"))
            {
                var player = _body as Player;
                BodyEnteredAction(player);
                // GD.Print("INTERACTIBLE BASE CLASS: entered");
            }
        }

        protected virtual void BodyEnteredAction(Player _player)
        {
            
        }
    }
}