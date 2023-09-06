using Godot;

namespace Stomper.FSM
{
    public class State
    {
        protected float startTime;

        public virtual void Enter()
        {
            DoChecks();
            startTime = OS.GetTicksUsec();
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate(float delta)
        {
            
        }

        public virtual void PhysicsUpdate(float delta)
        {
            DoChecks();
        }

        public virtual void DoChecks()
        {
            
        }
    }
}