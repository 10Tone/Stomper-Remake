using System;
using Godot;
using Stomper.FSM;

namespace Stomper
{
	public class Player : KinematicBody2D
	{
		#region States
		
		public PlayerIdleState IdleState { get; private set; }
		public PlayerMoveState MoveState { get; private set; }
		public PlayerJumpState JumpState { get; private set; }
		public PlayerInAirState InAirState { get; private set; }
		public PlayerLandState LandState { get; private set; }
		public PlayerFallState FallState { get; private set; }
		public PlayerStompState StompState { get; private set; }
		public PlayerDuckState DuckState { get; private set; }
		public PlayerStompJumpLoadingState StompJumpLoadingState {get; private set; }
		public PlayerStompJumpState StompJumpState { get; private set; }

		#endregion

		#region Export Variables

		[Export()] private PlayerData _playerData;
		[Export()] private NodePath _inputHandlerPath;
		[Export()] private NodePath _animatedSpritePath;
		[Export()] private NodePath _dashFxPath;
		[Export()] private NodePath _dustFxPath;
		[Export()] private NodePath _rayPath;

		#endregion
		
		public PlayerStateMachine PlayerFSM { get; private set; }
		public PlayerInputHandler InputHandler { get; private set; }
		public AnimatedSprite Animator { get; private set; }
		
		public Vector2 motion;
		public int FacingDirection { get; private set; }
		public bool IsGrounded { get; private set; }
		public bool AnimationFinished { get; set; }
		public bool StompJumpEnabled { get; set; }
		public bool CollidingWithWall { get; private set; }
		public GlobalEvents GlobalEvents { get; private set; }

		private Globals _globals;
		private DashFX _dashFx;
		private DustFX _dustFx;
		private RayCast2D _ray;

		#region Callback Functions

		public override void _EnterTree()
		{
			PlayerFSM = new PlayerStateMachine();
			_playerData = (PlayerData)GD.Load("res://src/game/characters/player/data/PlayerData.tres");
			_globals = GetNode<Globals>("/root/Globals");
			GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
			
			InitializeStates();
		}
		
		public override void _Ready()
		{
			InputHandler = GetNode(_inputHandlerPath) as PlayerInputHandler;
			Animator = GetNode(_animatedSpritePath) as AnimatedSprite;
			PlayerFSM.Initialize(IdleState);
			FacingDirection = 1;
			_dashFx = GetNode(_dashFxPath) as DashFX;
			_dustFx = GetNode(_dustFxPath) as DustFX;
			_ray = GetNode(_rayPath) as RayCast2D;

			GlobalEvents.Connect(nameof(GlobalEvents.PlayerFastTravelEvent), this, nameof(OnPlayerFastTravel));
		}
	
		public override void _Process(float delta) 
		{
			PlayerFSM.CurrentState.LogicUpdate(delta);
		}

		public override void _PhysicsProcess(float delta)
		{
			PlayerFSM.CurrentState.PhysicsUpdate(delta);
			IsGrounded = IsOnFloor();
			motion.y += _playerData.gravity * delta;
		   
			motion = MoveAndSlide(motion, Vector2.Up, false, 4, 0f);
			
			CheckForWall();
		}
		#endregion 
		
		private void InitializeStates()
		{
			IdleState = new PlayerIdleState(this, PlayerFSM, _playerData, "idle");
			MoveState = new PlayerMoveState(this, PlayerFSM, _playerData, "move");
			JumpState = new PlayerJumpState(this, PlayerFSM, _playerData, "jump");
			InAirState = new PlayerInAirState(this, PlayerFSM, _playerData, "in_air");
			LandState = new PlayerLandState(this, PlayerFSM, _playerData, "land");
			FallState = new PlayerFallState(this, PlayerFSM, _playerData, "fall");
			StompState = new PlayerStompState(this, PlayerFSM, _playerData, "stomp");
			DuckState = new PlayerDuckState(this, PlayerFSM, _playerData, "duck");
			StompJumpLoadingState = new PlayerStompJumpLoadingState(this, PlayerFSM, _playerData, "stomp_jump_loading");
			StompJumpState = new PlayerStompJumpState(this, PlayerFSM, _playerData, "stomp");
		}

		private void CheckForWall()
		{
			_ray.CastTo = new Vector2(12 * FacingDirection, 0);
			if (_ray.IsColliding())
			{
				var col = _ray.GetCollider();
				if (col is TileMap)
				{
					CollidingWithWall = true;
				}
				else CollidingWithWall = false;
			}
		}

		public void CheckIfShouldFlip(float horizontalInput)
		{
			if (horizontalInput != 0 && Convert.ToInt32(horizontalInput) != FacingDirection)
			{
				Flip();
			}
		}

		public void StartDashFx() => _dashFx.StartDashFX(this, _globals);
		public void StopDashFx() => _dashFx.StopDashFX();
		public void ShowDustFx(string animName) => _dustFx.ShowDustFx(GlobalPosition, animName);

		private void Flip()
		{
			FacingDirection *= -1;
			Animator.FlipH = FacingDirection switch
			{
				1 => false,
				-1 => true,
				_ => Animator.FlipH
			};
		}

		private void _on_AnimatedSprite_animation_finished() => AnimationFinished = true;

		private void OnPlayerFastTravel(Vector2 targetPos)
		{
			GlobalPosition = targetPos;
		}

	}
}
