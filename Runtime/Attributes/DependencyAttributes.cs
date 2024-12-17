using System;
using System.Reflection;
using UnityEngine;

namespace NosyCore.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class DependencyAttributeSelf : PropertyAttribute
    {
        public virtual void ValidateDependency(GameObject gameObject, FieldInfo fieldInfo)
        {
            if (gameObject.TryGetComponent(fieldInfo.FieldType, out var component))
            {
                fieldInfo.SetValue(gameObject.GetComponent(fieldInfo.DeclaringType), component);
            }
            else
            {
                LogError("Self", gameObject, fieldInfo.FieldType);
            }
        }
        
        protected void LogError(string attributeType, GameObject gameObject, Type typeSearched)
        {
            Debug.LogError($"Dependency ({attributeType}) of type {typeSearched} not found on {gameObject.name} hierarchy.");
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class DependencyAttributeChild : DependencyAttributeSelf
    {
        public override void ValidateDependency(GameObject gameObject, FieldInfo fieldInfo)
        {
            var comp = gameObject.GetComponentInChildren(fieldInfo.FieldType);
            if (comp)
            {
                fieldInfo.SetValue(gameObject.GetComponent(fieldInfo.DeclaringType), comp);
            }
            else
            {
                LogError("Child", gameObject, fieldInfo.FieldType);
            }
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class DependencyAttributeParent : DependencyAttributeSelf
    {
        public override void ValidateDependency(GameObject gameObject, FieldInfo fieldInfo)
        {
            var comp = gameObject.GetComponentInParent(fieldInfo.FieldType);
            if (comp)
            {
                fieldInfo.SetValue(gameObject.GetComponent(fieldInfo.DeclaringType), comp);
            }
            else
            {
                LogError("Parent", gameObject, fieldInfo.FieldType);
            }
        }
    }
}
