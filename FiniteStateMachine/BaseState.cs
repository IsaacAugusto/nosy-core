namespace NosyCore.FSM
{
    public abstract class BaseState : IState
    {
        public virtual void OnEnter()
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

        public virtual void OnExit()
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