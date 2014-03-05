using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public object RegisterService<T>(T service) where T : class
        {
            this.Container.RegisterSingle<T>(service);
            return this;
        }


        public object RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            this.Container.Register<T, TImpl>();
            return this;
        }
    }
}
