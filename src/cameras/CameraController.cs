using Godot;

namespace Stomper
{
    public class CameraController : Node2D
    {
        [Export()] private NodePath _normalCamPath, _shakeCamPath, _zoomCamPath;

        private Camera2D _normalCam;
        private ShakeCamera2D _shakeCam;
        private ZoomCamera _zoomCam;
        private GlobalEvents _globalEvents;
        private bool _cameraZoomRunning;

        public override void _EnterTree()
        {
            _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
            _globalEvents.Connect(nameof(GlobalEvents.StompEvent), this, nameof(OnStomp));
            _globalEvents.Connect(nameof(GlobalEvents.CameraZoomEvent), this, nameof(OnCameraZoom));
        }

        public override void _Ready()
        {
            _normalCam = GetNode(_normalCamPath) as Camera2D;
            _shakeCam = GetNode(_shakeCamPath) as ShakeCamera2D;
            _zoomCam = GetNode(_zoomCamPath) as ZoomCamera;
            _shakeCam?.Connect(nameof(ShakeCamera2D.ShakeFinishedEvent), this, nameof(OnShakeFinished));
            _zoomCam?.Connect(nameof(ZoomCamera.CameraZoomFinishedEvent), this, nameof(OnCameraZoomFinished));
            
            _normalCam.Current = true;
            
        }

        private void OnStomp(float amount)
        {
            if (_cameraZoomRunning) return;
            _shakeCam.Current = true;
            _shakeCam.AddTrauma(amount);
        }

        private void OnShakeFinished()
        {
            _normalCam.Current = true;
        }

        private void OnCameraZoom(Vector2 targetPos)
        {
            _cameraZoomRunning = true;
            _zoomCam.Current = true;
            _zoomCam.StartZoom(targetPos);
            
        }

        private void OnCameraZoomFinished()
        {
            _normalCam.Current = true;
            _cameraZoomRunning = false;
        }
        
    }
}
