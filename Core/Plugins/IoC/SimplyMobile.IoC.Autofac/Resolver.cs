using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.IoC.Autofac
{
    public class Resolver : IDependencyResolver
    {
        private readonly ContainerBuilder builder;
        private IContainer container;

        private IContainer Container
        {
            get
            {
                return container ?? (container = builder.Build());
            }
        }

		public Resolver() : this(new ContainerBuilder()){}

        public Resolver(ContainerBuilder builder)
        {
            this.builder = builder;
        }

        public T GetService<T>() where T : class
        {
            return this.Container.ResolveOptional<T>();
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            return new[] { this.GetService<T>() };
        }

        public object RegisterService<T>(T service) where T : class
        {
            this.container = null;
            return this.builder.RegisterInstance<T>(service);
        }

        public object RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            this.container = null;
            return this.builder.RegisterType<TImpl>().As<T>();
        }

        public object RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            this.container = null;
            return this.builder.Register<T>(a => func(this));
        }
    }
}
