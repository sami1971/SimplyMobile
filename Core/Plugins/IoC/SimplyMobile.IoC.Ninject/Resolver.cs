using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.IoC.Ninject
{
    public class Resolver : DynamicResolver
    {
        private IKernel kernel;

        public Resolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override T GetService<T>()
        {
            return (this.kernel.GetService(typeof(T)) as T) ?? base.GetService<T>();
        }

        public override IEnumerable<T> GetServices<T>()
        {
            return this.kernel.GetAll<T>();
        }
    }
}
