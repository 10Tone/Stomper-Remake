using Godot;

namespace Stomper
{
    public class Brick : BreakableBlock
    {
        private enum BrickType{Normal, Hidden}
        
        [Export()] private BrickType _brickType;
        [Export()] private Texture _normalTexture, _hiddenTexture;
        [Export()] private NodePath _spritePath, _collisionShapePath;
        [Export()] private NodePath _triggerPath;
        
        private Sprite _sprite;
        private CollisionShape2D _collisionShape;
        private Crate _trigger;

        public override void _Ready()
        {
            _sprite = GetNode(_spritePath) as Sprite;
            _collisionShape = GetNode(_collisionShapePath) as CollisionShape2D;

            if (_brickType == BrickType.Normal)
            {
                _sprite.Texture = _normalTexture;
                _collisionShape.SetDeferred("disabled", false);
            }
            else
            {
                _sprite.Texture = _hiddenTexture;
                _collisionShape.SetDeferred("disabled", true);
                _trigger = GetNode(_triggerPath) as Crate;
                _trigger.Connect(nameof(Crate.TriggerActivatedEvent), this, nameof(ShowBrick));
            }
        }

        private void ShowBrick()
        {
            _sprite.Texture = _normalTexture;
            _collisionShape.SetDeferred("disabled", false);
        }

        protected override void BodyEnteredAction(Player player)
        {
            base.BodyEnteredAction(player);
            FreeNode();
        }
    }
}
