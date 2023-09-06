using Godot;

namespace Stomper
{
    public class GlobalEvents : Node
    {
        [Signal] public delegate void StompEvent(float amount);

        [Signal] public delegate void CameraZoomEvent(Vector2 targetPos);

        [Signal] public delegate void LevelClearedEvent(LevelManager level);

        [Signal] public delegate void GemCollectedEvent();

        [Signal] public delegate void LevelFinishedEvent();

        [Signal] public delegate void LevelLoadedEvent(LevelData levelData, LevelManager level);

        [Signal] public delegate void PlayerDiedEvent();

        [Signal] public delegate void PlayerFastTravelEvent(Vector2 targetPos);

        [Signal]
        public delegate void MenuButtonActionEvent();
    }
}