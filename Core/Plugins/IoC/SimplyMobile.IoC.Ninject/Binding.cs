using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.IoC.Ninject
{
    public class Binding : IBinding
    {
        public IBindingConfiguration BindingConfiguration
        {
            get { throw new NotImplementedException(); }
        }

        public Type Service
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<Action<global::Ninject.Activation.IContext, object>> ActivationActions
        {
            get { throw new NotImplementedException(); }
        }

        public Func<global::Ninject.Activation.IRequest, bool> Condition
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<Action<global::Ninject.Activation.IContext, object>> DeactivationActions
        {
            get { throw new NotImplementedException(); }
        }

        public global::Ninject.Activation.IProvider GetProvider(global::Ninject.Activation.IContext context)
        {
            throw new NotImplementedException();
        }

        public object GetScope(global::Ninject.Activation.IContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsConditional
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsImplicit
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Matches(global::Ninject.Activation.IRequest request)
        {
            throw new NotImplementedException();
        }

        public IBindingMetadata Metadata
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<global::Ninject.Parameters.IParameter> Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public Func<global::Ninject.Activation.IContext, global::Ninject.Activation.IProvider> ProviderCallback
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Func<global::Ninject.Activation.IContext, object> ScopeCallback
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public BindingTarget Target
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
