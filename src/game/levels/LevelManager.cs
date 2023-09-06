using Godot;

namespace Stomper
{
	public class LevelManager : Node2D
	{
		[Export()] private int _levelNumber;

		public int LevelNumber => _levelNumber;

		[Export()] private LevelData _levelData;

		private int _gemsCollected;
		private GlobalEvents _globalEvents;
		private Globals _globals;

		public override void _EnterTree()
		{
			// automate the resource loading for all levels
			if(_levelNumber == 0)
			{
				GD.PushWarning("LEVELMANAGER: level number missing");
				return;
			}
			var levelNumString = "0" + _levelNumber;
			_levelData = (LevelData) GD.Load("res://src/game/levels/data/leveldata_resources/LevelData" + levelNumString + ".tres");
			_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
			_globalEvents.Connect(nameof(GlobalEvents.GemCollectedEvent), this, nameof(OnGemCollected));
			_globals = GetNode<Globals>("/root/Globals");
		}

		public override void _Ready()
		{
			_globals.CurrentLevel = this;
			// GD.Print(_gemsCollected + " at start level");
			_globalEvents.EmitSignal(nameof(GlobalEvents.LevelLoadedEvent), _levelData, this);
		}

		private void OnGemCollected()
		{
			_gemsCollected++;
			// GD.Print("update gems collected: " + _gemsCollected);
			if (_gemsCollected >= _levelData.gemsToCollect)
			{
				_globalEvents.EmitSignal(nameof(GlobalEvents.LevelClearedEvent), this);
				_gemsCollected = 0;
			}
		}
	}
}
