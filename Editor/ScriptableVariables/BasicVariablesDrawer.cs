using System;
using NosyCore.ScriptableVariables;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NosyCore.Editor
{
    internal static class SOVariableEditorUtils
    {
        internal static Label CreateBaseLabel()
        {
            var valueLabel = new Label();
            valueLabel.style.paddingLeft = 20;
            valueLabel.style.color = Color.white;
            valueLabel.style.backgroundColor = new Color(0, 1, 0, .314f);
            return valueLabel;
        }
    }
    
    [CustomPropertyDrawer(typeof(IntVariableSO))]
    public class IntVariableSODrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var objectField = new ObjectField(property.displayName)
            {
                objectType = typeof(IntVariableSO)
            };
            objectField.BindProperty(property);
            
            var valueLabel = SOVariableEditorUtils.CreateBaseLabel();
            
            container.Add(objectField);
            container.Add(valueLabel);

            objectField.RegisterValueChangedCallback(evt =>
            {
                var variable = evt.newValue as IntVariableSO;
                if (variable != null)
                {
                    valueLabel.text = $"Current value: {variable.Value}";
                    variable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
                }
                else
                {
                    valueLabel.text = String.Empty;
                }
            });
            
            var currentVariable = property.objectReferenceValue as IntVariableSO;
            if (currentVariable != null)
            {
                valueLabel.text = $"Current value: {currentVariable.Value}";
                currentVariable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
            }

            return container;
        }
    }
    
    [CustomPropertyDrawer(typeof(FloatVariableSO))]
    public class FloatVariableSODrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var objectField = new ObjectField(property.displayName)
            {
                objectType = typeof(FloatVariableSO)
            };
            objectField.BindProperty(property);
            
            var valueLabel = SOVariableEditorUtils.CreateBaseLabel();
            
            container.Add(objectField);
            container.Add(valueLabel);

            objectField.RegisterValueChangedCallback(evt =>
            {
                var variable = evt.newValue as FloatVariableSO;
                if (variable != null)
                {
                    valueLabel.text = $"Current value: {variable.Value}";
                    variable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
                }
                else
                {
                    valueLabel.text = String.Empty;
                }
            });
            
            var currentVariable = property.objectReferenceValue as FloatVariableSO;
            if (currentVariable != null)
            {
                valueLabel.text = $"Current value: {currentVariable.Value}";
                currentVariable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
            }

            return container;
        }
    }
    
    [CustomPropertyDrawer(typeof(BoolVariableSO))]
    public class BoolVariableSODrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var objectField = new ObjectField(property.displayName)
            {
                objectType = typeof(BoolVariableSO)
            };
            objectField.BindProperty(property);
            
            var valueLabel = SOVariableEditorUtils.CreateBaseLabel();
            
            container.Add(objectField);
            container.Add(valueLabel);

            objectField.RegisterValueChangedCallback(evt =>
            {
                var variable = evt.newValue as BoolVariableSO;
                if (variable != null)
                {
                    valueLabel.text = $"Current value: {variable.Value}";
                    variable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
                }
                else
                {
                    valueLabel.text = String.Empty;
                }
            });
            
            var currentVariable = property.objectReferenceValue as BoolVariableSO;
            if (currentVariable != null)
            {
                valueLabel.text = $"Current value: {currentVariable.Value}";
                currentVariable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
            }

            return container;
        }
    }
    
    [CustomPropertyDrawer(typeof(StringVariableSO))]
    public class StringVariableSODrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var objectField = new ObjectField(property.displayName)
            {
                objectType = typeof(StringVariableSO)
            };
            objectField.BindProperty(property);
            
            var valueLabel = SOVariableEditorUtils.CreateBaseLabel();

            container.Add(objectField);
            container.Add(valueLabel);

            objectField.RegisterValueChangedCallback(evt =>
            {
                var variable = evt.newValue as StringVariableSO;
                if (variable != null)
                {
                    valueLabel.text = $"Current value: {variable.Value}";
                    variable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
                }
                else
                {
                    valueLabel.text = String.Empty;
                }
            });
            
            var currentVariable = property.objectReferenceValue as StringVariableSO;
            if (currentVariable != null)
            {
                valueLabel.text = $"Current value: {currentVariable.Value}";
                currentVariable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
            }

            return container;
        }
    }
}