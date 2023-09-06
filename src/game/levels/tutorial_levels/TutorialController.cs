using System.Threading.Tasks;
using Godot;
namespace Stomper
{
	public class TutorialController : Node2D
	{
		[Export()] private NodePath _timerPath, _textPath, _animPath, _triggerPath;
		[Export(PropertyHint.MultilineText)] private string _line01, _line02, _line03, _line04, _line05, _line06, _line07, _line08; 

		private Timer _timer;
		private RichTextLabel _text;
		private AnimationPlayer _anim;
		private GlobalEvents _globalEvents;
		private Globals _globals;
		private Crate _trigger;

		private bool _stompJumpDone;

		public override void _EnterTree()
		{
			_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
			_globalEvents.Connect(nameof(GlobalEvents.StompEvent), this, nameof(OnStompEvent));
			_globals = GetNode<Globals>("/root/Globals");
		}

		public override void _Ready()
		{
			_timer = GetNode(_timerPath) as Timer;
			_text = GetNode(_textPath) as RichTextLabel;
			_anim = GetNode(_animPath) as AnimationPlayer;
			_trigger = GetNode(_triggerPath) as Crate;
			_trigger?.Connect(nameof(Crate.TriggerActivatedEvent), this, nameof(OnTriggerActivated));
			_trigger.Visible = false;
			// _timer.Connect("timeout", this, nameof(TutorialRoutine));
			_text.PercentVisible = 0f;
			StartTutorial();
		}

		private void StartTutorial()
		{
			TutorialRoutine();
			_timer.Start(1f);
		}

		private async void TutorialRoutine()
		{
			await ToSignal(_timer, "timeout");
			ShowText(_line01);
			_timer.Start(4f);
			
			await ToSignal(_timer, "timeout");
			ShowText(_line02);
			_timer.Start(5f);
			
			await ToSignal(_timer, "timeout");
			ShowText(_line03);
			_timer.Start(7f);
			
			await ToSignal(_timer, "timeout");
			ShowText(_line04);
			_timer.Start(5f);
			
			// await ToSignal(_timer, "timeout");
			// ShowText(_line05);
	  
		}

		private async void OnStompEvent(float amount)
		{
			if (_stompJumpDone) return;
			ShowText(_line05);
			_stompJumpDone = true;
			
			_timer.Start(5f);
			
			await ToSignal(_timer, "timeout");
			ShowText(_line06);
			_trigger.Visible = true;
		}

		private async void OnTriggerActivated()
		{
			ShowText(_line07);
			_timer.Start(2f);
			await ToSignal(_timer, "timeout");
			ShowText(_line08);
		}
		

		private void ShowText(string line)
		{
			_text.PercentVisible = 0f;
			_anim.Play("show_text");
			_text.BbcodeText = line;
		}
	}
}
