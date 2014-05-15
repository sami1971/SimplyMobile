using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimplyMobile
{
    public interface IAssemblyService
    {
        string GetCodeBase(Assembly assembly);

        Assembly[] GetAssemblies();

        Assembly LoadFrom(string assemblyPath);
    }

    public static class AssemblyServiceExtensions
    {
        public static Type FindType(this IAssemblyService assemblyService, string typeName)
        {
            return assemblyService.GetAssemblies()
                .Select(a => a.GetType(typeName))
                .FirstOrDefault(a => a != null);
        }

        public static Type FindType(this IAssemblyService assemblyService, string typeName, string assemblyName)
        {
            var assembly = assemblyService.LoadFrom(assemblyName);
            return (assembly != null) ? assembly.GetType(typeName) : null;
        }
    }
}
