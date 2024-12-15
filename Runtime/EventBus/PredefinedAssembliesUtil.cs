using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NosyCore.EventBus
{
    public static class PredefinedAssembliesUtil
    {

        public static List<Type> GetTypes(Type interfaceType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var assembliesToRegister = Resources.Load<EventBusAssembliesAsset>("EventBusAssembliesAsset");

            List<Type> types = new List<Type>();
            for (int i = 0; i < assemblies.Length; i++)
            {
                
                if (assembliesToRegister.Assemblies.Contains(assemblies[i].GetName().Name))
                {
                    AddTypesFromAssembly(assemblies[i].GetTypes(), types, interfaceType);
                }
            }

            return types;
        }

        private static void AddTypesFromAssembly(Type[] assemblyTypes, ICollection<Type> resultTypes, Type interfaceType)
        {
            if (assemblyTypes == null) return;

            for (int i = 0; i < assemblyTypes.Length; i++)
            {
                var type = assemblyTypes[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    resultTypes.Add(type);
                }
            }
        }
        
    }
}