namespace AISystem
{
    public abstract class BaseState
    {
        protected StateMachine stateMachine;

        public BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void Enter();

        public virtual void Update()
        {
            CheckTransaction();
        }

        public abstract void Exit();

        public virtual void FixedUpdate()
        {
        }

        public abstract void CheckTransaction();

        public virtual void AnimationEventHandler()
        {
        }
    }
}