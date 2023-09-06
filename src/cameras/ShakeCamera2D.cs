using System;
using Godot;

namespace Stomper
{
    public class ShakeCamera2D : Camera2D
    {
        [Export()] private float _decay = 0.8f;
        [Export()] private Vector2 _maxOffset = new Vector2(100, 75);
        [Export()] private float _maxRoll = 0.1f;
        [Export()] private NodePath _targetNodepath;

        private bool _shakeEnded;

        private Node2D _target;
        private float _trauma;
        private float _traumaPower = 2f;
        private OpenSimplexNoise _noise;
        private float _noiseY;
        private RandomNumberGenerator _rndm;
        // private GlobalEvents _globalEvents;

        [Signal] public delegate void ShakeFinishedEvent();

        public override void _Ready()
        {
            SetProcess(false);
            // _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
            // _globalEvents.Connect(nameof(GlobalEvents.StompEvent), this, nameof(OnStompEvent));
            _noise = new OpenSimplexNoise();
            _rndm = new RandomNumberGenerator();
            _rndm.Randomize();

            _noise.Seed = (int)_rndm.Randi();
            _noise.Period = 2;
            _noise.Octaves = 1;
            // if (_targetNodepath != null) _target = GetNode(_targetNodepath) as Node2D;
            
        }

        public override void _Process(float delta)
        {
            // if (_target != null) GlobalPosition = _target.GlobalPosition;

            _trauma = Mathf.Max(_trauma - _decay * delta, 0);

            Shake();
            
        }

        public void AddTrauma(float amount)
        {
            SetProcess(true);
            _shakeEnded = false;
            _trauma = Mathf.Min(_trauma + amount, 1.0f);
        }

        private void Shake()
        {
            var amount = Mathf.Pow(_trauma, _traumaPower);
            _noiseY += 1;
    
            Rotation = _maxRoll * amount * _noise.GetNoise2d(_noise.Seed, _noiseY);
            Set("offset", new Vector2(_maxOffset.x * amount * _noise.GetNoise2d(_noise.Seed * 2, _noiseY), _maxOffset.y * amount * _noise.GetNoise2d(_noise.Seed * 3, _noiseY)));
            _shakeEnded = _trauma == 0;
            
            if (!_shakeEnded) return;
            EmitSignal(nameof(ShakeFinishedEvent));
            SetProcess(false);
            
        }

        // private void OnStompEvent(float amount)
        // {
        //     AddTrauma(amount);
        // }
    }
}
