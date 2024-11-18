using System;
using System.Collections.Generic;
using System.Reflection;

namespace NosyCore.EventBus
{
    public static class PredefinedAssembliesUtil
    {
        enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpFirstPass,
            AssemblyCSharpEditorFirstPass,
        }

        static AssemblyType? GetAssemblyType(string assemblyName)
        {
            switch (assemblyName)
            {
                case "Assembly-CSharp":
                    return AssemblyType.AssemblyCSharp;
                case "Assembly-CSharp-Editor":
                    return AssemblyType.AssemblyCSharpEditor;
                case "Assembly-CSharp-FirstPass":
                    return AssemblyType.AssemblyCSharpFirstPass;
                case "Assembly-CSharp-Editor-FirstPass":
                    return AssemblyType.AssemblyCSharpEditorFirstPass;
                default:
                    return null;
            }
        }

        public static List<Type> GetTypes(Type interfaceType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();
            List<Type> types = new List<Type>();
            for (int i = 0; i < assemblies.Length; i++)
            {
                AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
                if (assemblyType.HasValue)
                {
                    assemblyTypes.Add(assemblyType.Value, assemblies[i].GetTypes());
                }
            }

            if (assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharp, out var csharpTypes))
            {
                AddTypesFromAssembly(csharpTypes, types, interfaceType);
            }
            if (assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpEditor, out var csharpETypes))
            {
                AddTypesFromAssembly(csharpETypes, types, interfaceType);
            }
            if (assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpFirstPass, out var csharpFPTypes))
            {
                AddTypesFromAssembly(csharpFPTypes, types, interfaceType);
            }
            if (assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpEditorFirstPass, out var csharpEFPTypes))
            {
                AddTypesFromAssembly(csharpEFPTypes, types, interfaceType);
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