namespace Game.Scripts.BaseStateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Exit();
        
        public virtual void Tick(float deltaTime) {}
        public virtual void TickLate(float deltaTime) {}
        public virtual void TickFixed(float deltaTime) {}
        
        public virtual void CheckState() {}
    }
}
