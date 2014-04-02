using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile
{
    public class AssemblyService : IAssemblyService
    {
        public string GetCodeBase(Assembly assembly)
        {
            return assembly.GetName().CodeBase;
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public Assembly LoadFrom(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
