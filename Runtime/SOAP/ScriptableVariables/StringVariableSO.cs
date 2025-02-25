using UnityEngine;

namespace NosyCore.ScriptableVariables
{
    [CreateAssetMenu(menuName = "NosyCore/ScriptableVariables/StringVariable")]
    public class StringVariableSO : VariableScriptableObject<string>
    {
        [SerializeField]
        private string _initialValue;
        [SerializeField]
        private string _value;

        public override string Value
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