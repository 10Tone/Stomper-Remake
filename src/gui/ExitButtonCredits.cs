using Godot;

namespace Stomper
{
	public class ExitButtonCredits : Button
	{
		private GlobalEvents _globalEvents;

		public override void _EnterTree()
		{
			_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		}
		
		private void _on_ExitButton_button_down()
		{
			_globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.Exit);
		}
	}
}
