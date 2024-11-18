using UnityEngine;

namespace NosyCore.FSM
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnFixedUpdate();
        void OnExit();
    }

    public interface IStateContext {}
}
