using Godot;

namespace Stomper
{
    public class ZoomCamera : Camera2D
    {
        [Export()] private NodePath _zoomTweenPath, _animatorPath, _transitionPath;

        private Tween _zoomTween;
        private AnimationPlayer _animationPlayer;
        // private Transition _transition;
        private Vector2 _startPos;
        private Vector2 _offset = new Vector2(160, 120);
        private Vector2 _targetZoom = new Vector2(0.85f, 0.85f);
        private Vector2 _targetPos;
        private float _zoomDuration = 1f;
        
        [Signal] public delegate void CameraZoomFinishedEvent();

        public override void _Ready()
        {
            _zoomTween = GetNode(_zoomTweenPath) as Tween;
            _animationPlayer = GetNode(_animatorPath) as AnimationPlayer;
            // _transition = GetNode(_transitionPath) as Transition;
            _startPos = GlobalPosition;
        }

        public void StartZoom(Vector2 targetPos)
        {
            _targetPos = targetPos - _offset;
            _animationPlayer.Play("zoom");
        }

        public void ZoomIn()
        {
            GD.Print("start zoom in");
            _zoomTween.InterpolateProperty(this, "zoom", Zoom, _targetZoom, _zoomDuration, Tween.TransitionType.Quad, Tween.EaseType.In);
            _zoomTween.InterpolateProperty(this, "global_position", _startPos, _targetPos , _zoomDuration,
                Tween.TransitionType.Quad, Tween.EaseType.In);
            _zoomTween.Start();
        }

        public void ZoomOut()
        {
            GD.Print("start zoom out");
            _zoomTween.InterpolateProperty(this, "zoom", _targetZoom, new Vector2(1,1), _zoomDuration, Tween.TransitionType.Quad, Tween.EaseType.Out);
            _zoomTween.InterpolateProperty(this, "global_position", _targetPos, _startPos , _zoomDuration,
                Tween.TransitionType.Quad, Tween.EaseType.Out);
            _zoomTween.Start();
        }

        private void _on_AnimationPlayer_animation_finished(string animName)
        {
            EmitSignal(nameof(CameraZoomFinishedEvent));
        }
    }
}
