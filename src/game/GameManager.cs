using System;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace Stomper
{
    public class GameManager : Node2D
    {
        [Export()] private NodePath _transitionFxPath;
        
        private Globals _globals;
        private GlobalEvents _globalEvents;
        private Dictionary<int, PackedScene> _levels = new Dictionary<int, PackedScene>();
        private Transition _transitionFx;

        public override void _EnterTree()
        {
            base._EnterTree();
            _globals = GetNode<Globals>("/root/Globals");
            _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
            _globalEvents.Connect(nameof(GlobalEvents.LevelFinishedEvent), this, nameof(OnLevelFinished));
            _globalEvents.Connect(nameof(GlobalEvents.PlayerDiedEvent), this, nameof(OnPlayerDied));
        }

        public override void _Ready()
        {
            base._Ready();
            _transitionFx = GetNode(_transitionFxPath) as Transition;
            PreLoadLevels();

            if (_globals.SkipTutorial)
            {
                var firstLevel = _levels[2].Instance() as LevelManager;
                AddChild(firstLevel);
            }
            else
            {
                var firstLevel = _levels[1].Instance() as LevelManager;
                AddChild(firstLevel);
            }
            // _globals.CurrentLevel = level01;
            _globals.PlayerInputEnabled = true;

        }

        public override void _Process(float delta)
        {
            // TEMP for testing
            if (Input.IsActionJustPressed("next_level"))
            {
                CallDeferred(nameof(LoadLevel));
            }
        }

        private void LoadLevel()
        {
            var currentLevel = _globals.CurrentLevel;
            int nextLevelIndex = currentLevel.LevelNumber + 1;
            var nextLevelToLoad = _levels[nextLevelIndex].Instance() as LevelManager;
            currentLevel.CallDeferred("queue_free");
            AddChild(nextLevelToLoad);
            // _globals.CurrentLevel = nextLevelToLoad;
        }

        private void ReloadCurrentLevel()
        {
            var currentLevel = _globals.CurrentLevel;
            var nextLevelToLoad = _levels[currentLevel.LevelNumber].Instance() as LevelManager;
            currentLevel.CallDeferred("queue_free");
            AddChild(nextLevelToLoad);
            // _globals.CurrentLevel = nextLevelToLoad;
        }

        private void OnLevelFinished()
        {
            // GD.Print("level finished");
            CallDeferred(nameof(LoadLevel));
        }

        private void OnPlayerDied()
        {
            // death animation and routine?
            GD.Print("player died");
            LevelTransitionRoutine(ReloadCurrentLevel);
        }

        private async void LevelTransitionRoutine(Action loadLevelAction)
        {
            _globals.PlayerInputEnabled = false;
            var delay = _transitionFx.PlayTransitionAnimation("fade_in") * 1000;
            await Task.Delay((int) delay);
            loadLevelAction();
            _transitionFx.PlayTransitionAnimation("fade_out");
            _globals.PlayerInputEnabled = true;

        }

        private void PreLoadLevels()
        {
            var path = "res://src/game/levels/level_scenes/";
            var dir = new Directory();
            dir.Open(path);
            if (!dir.DirExists(path))
            {
                GD.PushWarning("GAMEMANAGER: Directory Not Found!");
                return;
            }

            dir.ListDirBegin(true, true);
            string filename = dir.GetNext();
            int id = 1;

            while (dir.FileExists(filename))
            {
                if (!dir.CurrentIsDir())
                {
                    PackedScene level = (PackedScene) GD.Load(path + filename);
                    var preview = level.Instance() as LevelManager;
                    _levels.Add(preview.LevelNumber, level);
                    preview.CallDeferred("queue_free");
                    // GD.Print("level " + preview.LevelNumber + " preloaded");
                    id++;
                    
                }

                filename = dir.GetNext();
            }
        }
    }
}