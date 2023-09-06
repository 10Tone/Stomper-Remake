using Godot;

namespace Stomper
{
    public class BrickRubble : RigidBody2D
    {
        [Export()] private NodePath _spriteNodepath;

        private Sprite _sprite;

        public float horizontalDirection;
        public float rotation;
        // public Texture rubbleTexture;

        public override void _Ready()
        {
            _sprite = GetNode(_spriteNodepath) as Sprite;
            
        }

        public override void _Process(float delta)
        {
            _sprite.Rotation += 0.2f * rotation;
            
        }

        public void ApplyImpulse(Texture rubbleTexture)
        {
            _sprite.Texture = rubbleTexture;
            ApplyCentralImpulse(new Vector2(horizontalDirection, -200));
            // GD.Print(horizontalDirection);
        }

        public void _on_VisibilityNotifier2D_screen_exited()
        {
            // GD.Print(GlobalPosition);
            QueueFree();
        }
    }
}