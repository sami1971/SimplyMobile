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

        public IDependencyResolver RegisterService<T>(T service) where T : class
        {
            this.container = null;
            this.builder.RegisterInstance<T>(service);
            return this;
        }

        public IDependencyResolver RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            this.container = null;
            this.builder.RegisterType<TImpl>().As<T>();
            return this;
        }

        public IDependencyResolver RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            this.container = null;
            this.builder.Register<T>(a => func(this));
            return this;
        }
    }
}
