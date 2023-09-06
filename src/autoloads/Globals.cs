using Godot;

namespace Stomper
{
    public class Globals : Node
    {
        public LevelManager CurrentLevel { get; set; }
        public bool PlayerInputEnabled { get; set; }
        public bool SkipTutorial { get; set; }
    }
}