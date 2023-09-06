using System;
using Godot;

namespace Stomper
{
    public class PlayerData : Resource
    {
        [Export()] public float acceleration = 512;
        [Export()] public float maxSpeed = 64;
        [Export()] public float friction = 0.25f;
        [Export()] public float airResistance = 0.02f;
        [Export()] public float gravity = 200;
        [Export()] public float jumpForce = 128;
        [Export()] public int amountOfJumps = 2;
        [Export()] public float coyoteTime = 0.2f;
        [Export()] public float variableJumpHeightMultiplier = 0.1f;
        [Export()] public float stompSpeed = 200;
        
    }
}
