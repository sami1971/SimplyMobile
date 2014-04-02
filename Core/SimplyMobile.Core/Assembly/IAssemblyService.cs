using System.Reflection;

namespace SimplyMobile
{
    public interface IAssemblyService
    {
        string GetCodeBase(Assembly assembly);

        Assembly[] GetAssemblies();

        Assembly LoadFrom(string assemblyPath);
    }
}
