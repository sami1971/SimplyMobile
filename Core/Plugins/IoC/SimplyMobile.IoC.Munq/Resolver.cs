using Munq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.IoC.Munq
{
    public class Resolver : IDependencyResolver
    {
        private IocContainer container;

        private IocContainer Container
        {
            get
            {
                return container ?? (container = new IocContainer());
            }
        }

        public Resolver()
        {
        }

        public Resolver(IocContainer container)
        {
            this.container = container;
        }

        public T GetService<T>() where T : class
        {
            return this.Container.CanResolve<T>() ?
                this.Container.Resolve<T>() :
                null;
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            return this.Container.ResolveAll<T>();
        }

        public object RegisterService<T>(T service) where T : class
        {
            return this.Container.RegisterInstance<T>(service);
        }

        public object RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            return this.Container.Register<T, TImpl>();
        }

        public object RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            return this.Container.Register<T>(c => func(this));
        }
    }
}
