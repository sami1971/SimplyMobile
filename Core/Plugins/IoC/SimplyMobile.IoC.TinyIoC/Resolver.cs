using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TinyIoC;

namespace SimplyMobile.IoC.TinyIoC
{
    public class Resolver : DynamicResolver
    {
        private TinyIoCContainer container;

        public TinyIoCContainer Container
        {
            get
            {
                return this.container ?? (this.container = new TinyIoCContainer());
            }
        }

        public override T GetService<T>()
        {
            T ret = default(T);

            try
            {
                ret = this.Container.Resolve<T>();
            }
            catch
            {
            }

            return ret ?? base.GetService<T>();
        }

        public override IEnumerable<T> GetServices<T>()
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
    }
}
