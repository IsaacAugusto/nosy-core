using UnityEngine.Events;

namespace NosyCore.ScriptableVariables
{
    public abstract class VariableScriptableObject<T> : RuntimeScriptableObject
    {
        public abstract T Value { get; set; }
        public UnityAction<T> OnValueChanged { get; set; }
    }
}