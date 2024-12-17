using System.Reflection;
using UnityEngine;

namespace NosyCore.Attributes
{
    public static class ReflectionUtils
    {
        public static void ValidateDependencies(this MonoBehaviour target)
        {
            var fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |BindingFlags.Public);
            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(typeof(DependencyAttributeSelf), false);
                foreach (var attribute in attributes)
                {
                    if (attribute is DependencyAttributeSelf dependencyAttribute) dependencyAttribute.ValidateDependency(target.gameObject, field);
                }
            }
        }
    }
}