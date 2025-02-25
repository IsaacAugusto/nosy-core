using UnityEngine;

namespace NosyCore.ScriptableVariables
{
    [CreateAssetMenu(menuName = "NosyCore/ScriptableVariables/FloatVariable")]
    public class FloatVariableSO : VariableScriptableObject<float>
    {
        [SerializeField]
        private float _initialValue;
        [SerializeField]
        private float _value;
        
        public override float Value
        {
            get => _value;
            set
            {
                if (Mathf.Approximately(_value, value)) return;
                
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        protected override void OnReset()
        {
            _value = _initialValue;
            OnValueChanged?.Invoke(_value);
        }
    }
}