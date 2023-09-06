using Godot;

namespace Stomper
{
    public class GemsDisplay : HBoxContainer
    {
        [Export()] private NodePath _gemIconsNodePath;
        [Export()] private Texture _emptyGemIcon;
        [Export()] private Texture _collectedGemIcon;
        
        private GlobalEvents _globalEvents;
        private Control _gemIcons;
        private int _gemsCollected;

        public override void _EnterTree()
        {
            _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
            _globalEvents.Connect(nameof(GlobalEvents.LevelLoadedEvent), this, nameof(OnLevelLoaded));
            _globalEvents.Connect(nameof(GlobalEvents.GemCollectedEvent), this, nameof(OnGemCollected));
        }

        public override void _Ready()
        {
            _gemIcons = GetNode(_gemIconsNodePath) as Control;
        }

        private void OnLevelLoaded(LevelData levelData, LevelManager level)
        {
            if (level.LevelNumber == 1 || level.LevelNumber == 6)
            {
                Visible = false;
                return;
            }
            
            Visible = true;
            
            _gemsCollected = 0;
            if (_gemIcons.GetChildCount() > 0)
            {
                foreach (Node icon in _gemIcons.GetChildren())
                {
                   // RemoveChild(icon);
                   icon.CallDeferred("queue_free");
                }
            }
            for (int i = 0; i < levelData.gemsToCollect; i++)
            {
                var icon = new TextureRect();
                _gemIcons.AddChild(icon);
                icon.Texture = _emptyGemIcon;
            }
        }

        private void OnGemCollected()
        {
            _gemsCollected++;
            var icon = _gemIcons.GetChild(_gemsCollected - 1) as TextureRect;
            icon.Texture = _collectedGemIcon;
        }
    }
}
