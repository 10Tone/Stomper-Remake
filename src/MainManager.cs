using Godot;

namespace Stomper
{
	public class MainManager : Node2D
	{
		[Export()] private PackedScene _gamePackedScene, _mainMenuPackScene;

		private Node _currentScene;
		private GlobalEvents _globalEvents;
		private Globals _globals;
		
		public override void _EnterTree()
		{
			_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
			_globals = GetNode<Globals>("/root/Globals");
			_globalEvents.Connect(nameof(GlobalEvents.MenuButtonActionEvent), this, nameof(OnMenuButtonAction));
		}

		public override void _Ready()
		{
			LoadScene(_mainMenuPackScene);
		}

		private void OnMenuButtonAction(MenuButtonActions menuButtonAction)
		{
			switch (menuButtonAction)
			{
				case MenuButtonActions.StartTutorial:
					GD.Print("start tutorial");
					_globals.SkipTutorial = false;
					LoadScene(_gamePackedScene);
					break;
				
				case MenuButtonActions.StartGame:
					GD.Print("start game");
					_globals.SkipTutorial = true;
					LoadScene(_gamePackedScene);
					break;
				
				case MenuButtonActions.PauseGame:
					GetTree().Paused = true;
					break;
				
				case MenuButtonActions.ResumeGame:
					GetTree().Paused = false;
					break;
				
				case MenuButtonActions.Exit:
					GD.Print("exit");
					GetTree().Quit();
					break;
				
				default:
					GD.Print("start game");
					break;
			}
		}

		private void LoadScene(PackedScene scene)
		{
			var sceneToLoad = scene.Instance();

			if (_currentScene == sceneToLoad)
			{
				sceneToLoad.QueueFree();
				return;
			}
			AddChild(sceneToLoad);
			_currentScene?.QueueFree();
			_currentScene = sceneToLoad;
		}
	}
	
	public enum MenuButtonActions {StartTutorial, StartGame, PauseGame, ResumeGame, Exit}
}
