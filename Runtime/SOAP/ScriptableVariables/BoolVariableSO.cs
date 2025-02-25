using UnityEngine;

namespace NosyCore.ScriptableVariables
{
    [CreateAssetMenu(menuName = "NosyCore/ScriptableVariables/BoolVariable")]
    public class BoolVariableSO : VariableScriptableObject<bool>
    {
        [SerializeField]
        private bool _initialValue;
        [SerializeField]
        private bool _value;
        
        public override bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                
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