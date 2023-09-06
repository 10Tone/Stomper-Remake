using System;
using Godot;

namespace Stomper
{
    public class FastTravel : Interactible
    {
        private enum TargetDirection {Horizontal, Vertical}

        [Export()] private TargetDirection _targetDirection;

        [Export()] private NodePath _otherPos2DNodePath;

        private Position2D _position2D;
        
        public override void _Ready()
        {
            base._Ready();
            _position2D = GetNode(_otherPos2DNodePath) as Position2D;
        }


        protected override void BodyEnteredAction(Player _player)
        {
            switch (_targetDirection)
            {
                case TargetDirection.Horizontal:
                    globalEvents.EmitSignal(nameof(GlobalEvents.PlayerFastTravelEvent), new Vector2(_position2D.GlobalPosition.x, _player.GlobalPosition.y));
                    GD.Print("horizontal");
                    break;
                case TargetDirection.Vertical:
                    globalEvents.EmitSignal(nameof(GlobalEvents.PlayerFastTravelEvent), _position2D.GlobalPosition);
                    GD.Print("Vertical");
                    break;
                default:
                    globalEvents.EmitSignal(nameof(GlobalEvents.PlayerFastTravelEvent), Vector2.Zero);
                    break;
            }
            
        }
    }
}
