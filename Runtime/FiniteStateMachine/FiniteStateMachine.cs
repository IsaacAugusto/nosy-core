using System;
using System.Collections.Generic;

namespace NosyCore.FSM
{
    public class FiniteStateMachine
    {
        private StateNode _currentState;
        private Dictionary<Type, StateNode> _nodes = new ();
        private HashSet<ITransition> _anyTransitions = new ();

        public FiniteStateMachine(IState initialState)
        {
            SetState(GetOrAddNode(initialState).State);
        }

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.ToState);
            }
            
            _currentState.State.OnUpdate();
        }

        public void FixedUpdate()
        {
            _currentState.State.OnFixedUpdate();
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransitions(GetOrAddNode(to).State, condition);
        }
        
        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(to, condition));
        }

        public void SetState(IState state)
        {
            if (_currentState?.State == state) return;
            
            var previousState = _currentState;
            if (previousState != null)
            {
                previousState.State.OnExit();
            }
           
            _currentState = _nodes[state.GetType()];
            _currentState.State.OnEnter();
        }
        
        private ITransition GetTransition()
        {
            foreach (var anyTransition in _anyTransitions)
            {
                if (anyTransition.Condition.Evaluate())
                {
                    return anyTransition;
                }
            }
            
            foreach (var transition in _currentState.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            return null;
        }
                
        private StateNode GetOrAddNode(IState state)
        {
            if (_nodes.TryGetValue(state.GetType(), out var node))
            {
                return node;
            }

            node = new StateNode(state);
            _nodes.Add(state.GetType(), node);
            return node;
        }

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransitions(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}