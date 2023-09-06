using Godot;

namespace Stomper
{
    public class PauseMenu : CanvasLayer
    {
        [Export()] private NodePath _popupPath;

        private Control _popup;
        private GlobalEvents _globalEvents;

        public override void _EnterTree()
        {
            _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        }

        public override void _Ready()
        {
            _popup = GetNode(_popupPath) as Container;
            // _popup.DialogText = "Exit Game";
            _popup.Visible = false;

        }

        public override void _Process(float delta)
        {
            if (Input.IsActionJustPressed("pause"))
            {
                if (!_popup.Visible)
                {
                    _popup.Visible = true;
                    _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.PauseGame);
                }
                else
                {
                    _popup.Visible = false;
                    _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.ResumeGame);
                }
                
            }
        }

        private void _on_ResumeButton_button_down()
        {
            // GD.Print("resume clicked");
            _popup.Visible = false;
            _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.ResumeGame);
        }

        private void _on_ExitButton_button_down()
        {
            // GD.Print("exit clicked");
            _globalEvents.EmitSignal(nameof(GlobalEvents.MenuButtonActionEvent), MenuButtonActions.Exit);
        }
    }
}
