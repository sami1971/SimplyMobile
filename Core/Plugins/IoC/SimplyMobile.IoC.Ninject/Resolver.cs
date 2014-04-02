using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.IoC.Ninject
{
    public class Resolver : IDependencyResolver
    {
        private IKernel kernel;

        public Resolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public T GetService<T>() where T : class
        {
            return this.kernel.GetService(typeof(T)) as T;
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            return this.kernel.GetAll<T>();
        }

        public IDependencyResolver RegisterService<T>(T service) where T : class
        {
            this.kernel.Bind<T>().ToConstant<T>(service);
            return this;
        }

        public IDependencyResolver RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            this.kernel.Bind<T, TImpl>();
            return this;
        }

        public IDependencyResolver RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            this.kernel.Bind<T>().ToMethod<T>(t => func(this));
            return this;
        }
    }
}
