using System;
using System.Reflection;

namespace SimplyMobile
{
    public class AssemblyService : IAssemblyService
    {
        #region IAssemblyService implementation
        public string GetCodeBase(Assembly assembly)
        {
            return assembly.CodeBase;
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public Assembly LoadFrom(string assemblyPath)
        {
            return Assembly.LoadFrom (assemblyPath);
        }
        #endregion
    }
}

