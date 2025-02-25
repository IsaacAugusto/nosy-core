using UnityEngine;

namespace NosyCore.ScriptableVariables
{
    [CreateAssetMenu(menuName = "NosyCore/ScriptableVariables/IntVariable")]
    public class IntVariableSO : VariableScriptableObject<int>
    {
        [SerializeField]
        private int _initialValue;
        [SerializeField]
        private int _value;
        
        public override int Value
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