using Godot;

namespace Stomper
{
    public class DashFX: Node2D
    {
        [Export()] private PackedScene _dashTracePackedScene;
        [Export()] private NodePath _timerPath;

        private Player _player;
        private Globals _globals;
        private Timer _timer;
        private bool _canShowDash;

        public override void _Ready()
        {
            base._Ready();
            _timer = GetNode(_timerPath) as Timer;
        }

        public void StartDashFX(Player player, Globals globals)
        {
            _player = player;
            _globals = globals;
            _canShowDash = true;
            InstanceDashTrace();
        }

        public void StopDashFX()
        {
            _canShowDash = false;
            _timer.Stop();
        } 

        private void InstanceDashTrace()
        {
            if (!_canShowDash) return;
            InstanceDashTraceRoutine();
        }
        private async void InstanceDashTraceRoutine()
        {
            var dashTrace = _dashTracePackedScene.Instance() as Sprite;
            _globals.CurrentLevel.AddChild(dashTrace);
            dashTrace.GlobalPosition = _player.GlobalPosition;
            _timer.Start();
            await ToSignal(_timer, "timeout");
            InstanceDashTrace();
        }
        
    }
}