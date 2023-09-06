using Godot;

namespace Stomper
{
    public class GhostStep : StaticBody2D
    {
        [Export()] private NodePath _collisionShapePath;
        [Export()] private NodePath _animPlayerPath;
        [Export()] private NodePath _timerPath;
        [Export()] private NodePath _triggerPath;
        [Export()] private float _timerDelay = 5f;

        private CollisionShape2D _collisionShape;
        private AnimationPlayer _animPlayer;
        private Timer _timer;
        private GhostStepTrigger _trigger;

        public override void _Ready()
        {
            _collisionShape = GetNode(_collisionShapePath) as CollisionShape2D;
            _animPlayer = GetNode(_animPlayerPath) as AnimationPlayer;
            _timer = GetNode(_timerPath) as Timer;
            _trigger = GetNode(_triggerPath) as GhostStepTrigger;
            _trigger?.Connect(nameof(GhostStepTrigger.TriggerActivatedEvent), this, nameof(OnTriggerActivated));
            
            _collisionShape.SetDeferred("disabled", true);
            
        }

        private void OnTriggerActivated()
        {
            GD.Print("trigger activated");
            _animPlayer.Play("fade_in");
            _timer.Start(_timerDelay);
            // enable collision
            // start show animation
            // start timer
        }

        private void DeActivate()
        {
            _animPlayer.Play("fade_out");
        }

        private void _on_Timer_timeout()
        {
            DeActivate();
        }
    }
}
