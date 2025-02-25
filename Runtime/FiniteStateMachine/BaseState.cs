namespace NosyCore.FSM
{
    public abstract class BaseState : IState
    {
        public virtual void OnEnter(IState previousState)
        {
            // noop
        }

        public virtual void OnUpdate()
        {
            // noop
        }

        public virtual void OnFixedUpdate()
        {
            // noop
        }

        public virtual void OnExit(IState nextState)
        {
            // noop
        }
    }
        
    public abstract class BaseState<T> : BaseState where T : IStateContext
    {
        protected T _context;

        protected BaseState(T context)
        {
            _context = context;
        }
    }
}