using UnityEngine;

namespace NosyCore.FSM
{
    public interface IState
    {
        void OnEnter(IState previousState);
        void OnUpdate();
        void OnFixedUpdate();
        void OnExit(IState nextState);
    }

    public interface IStateContext {}
}
