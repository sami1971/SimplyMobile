using SimpleInjector;
using System;
using System.Collections.Generic;

namespace SimplyMobile.IoC.SimpleInjector
{
    public class Resolver : IDependencyResolver
    {
        private Container container;

        private Container Container
        {
            get
            {
                return container ?? (container = new Container());
            }
        }

        public T GetService<T>() where T : class
        {
            return this.Container.GetInstance<T>();
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            return this.Container.GetAllInstances<T>();
        }

        public IDependencyResolver RegisterService<T>(T service) where T : class
        {
            this.Container.RegisterSingle<T>(service);
            return this;
        }

        public IDependencyResolver RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            this.Container.Register<T, TImpl>();
            return this;
        }

        public IDependencyResolver RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            this.Container.Register<T> (() => func (this));
            return this;
        }
    }
}
