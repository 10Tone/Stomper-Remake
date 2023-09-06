using Godot;

namespace Stomper
{
    public class MainMenu : CanvasLayer
    {
        private GlobalEvents _globalEvents;

        public override void _EnterTree()
        {
            _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        }

        private void _on_StartTutorialButton_button_down()
        {
            _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.StartTutorial);
        }

        private void _on_StartButton_button_down()
        {
            _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.StartGame);
        }

        private void _on_ExitButton_button_down()
        {
            _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.Exit);
        }
    }
}
