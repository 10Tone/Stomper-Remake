using Godot;
using Object = Godot.Object;

namespace Stomper
{
    public class DashTrace : Sprite
    {
        private Tween _tween;

        [Signal] private delegate void DashTraceTweenCompleted();
    
        public override void _Ready()
        {
            _tween = GetNode("Tween") as Tween;
            _tween?.InterpolateProperty(this, "modulate:a", 0.75f, 0.0f, 0.65f, Tween.TransitionType.Expo,
                Tween.EaseType.Out);
            _tween?.Start();
        }

        private void _on_Tween_tween_completed(Object @object, NodePath key)
        {
            EmitSignal(nameof(DashTraceTweenCompleted));
            QueueFree();
        }
    
    }
}
