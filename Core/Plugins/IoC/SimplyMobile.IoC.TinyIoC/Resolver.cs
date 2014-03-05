using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TinyIoC;

namespace SimplyMobile.IoC.TinyIoC
{
    public class Resolver : IDependencyResolver
    {
        private TinyIoCContainer container;

        public Resolver()
        {
        }

        public Resolver(bool autoRegister)
        {
            if (autoRegister)
            {
                this.Container.AutoRegister();
            }
        }

        public Resolver(TinyIoCContainer container)
        {
            this.container = container;
        }

        public TinyIoCContainer Container
        {
            get
            {
                return this.container ?? (this.container = new TinyIoCContainer());
            }
        }

        public T GetService<T>() where T : class
        {
            T ret = default(T);

            try
            {
                ret = this.Container.Resolve<T>();
            }
            catch
            {
            }

            return ret;
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            try
            {
                return this.Container.ResolveAll<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public object RegisterService<T>(T service) where T : class
        {
            return this.Container.Register<T>(service);
        }


        public object RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            return this.Container.Register<T, TImpl>();
        }


        public object RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            return this.Container.Register<T>((c, p) => func(this));
        }
    }
}
