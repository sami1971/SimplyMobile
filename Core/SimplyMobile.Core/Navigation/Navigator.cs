using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    using Core;

    public abstract class Navigator : INavigationController
    {
        public virtual bool NavigateTo<T>(object sender, T model) where T : ViewModel
        {
            var type = typeof(T);
            object t;

            Func<T, bool> target;

            if (this.Delegates.TryGetValue(type, out t))
            {
                target = t as Func<T, bool>;
                if (target == null)
                {
                    this.Delegates.Remove(type);
                } 
                else
                {
                    return target (model);
                }
            }

            if (this.WeakDelegates.ContainsKey(type))
            {
                var funcDelegate = this.WeakDelegates[type];

                if (!funcDelegate.IsAlive || (target = funcDelegate.Target as Func<ViewModel, bool>) == null)
                {
                    this.WeakDelegates.Remove(type);
                    return false;
                }

                return target(model);
            }

            return false;
        }

        public void SetDelegate<T>(Func<T, bool> func) where T : ViewModel
        {
            var type = typeof(T);
            this.Delegates.Remove(type);
            this.WeakDelegates.Remove(type);
            this.Delegates.Add(type, func);
        }

        public void SetWeakDelegate<T>(Func<T, bool> func) where T : ViewModel
        {
            var type = typeof(T);
            this.WeakDelegates.Remove(type);
            this.WeakDelegates.Add(type, new WeakReference(func));
        }

        public virtual bool RemoveDelegates<T>() where T : ViewModel
        {
            return this.Delegates.Remove(typeof(T)) || this.WeakDelegates.Remove(typeof(T));
        }

        //public virtual bool RemoveWeakDelegate<T>() where T : ViewModel
        //{
        //    return this.WeakDelegates.Remove(typeof(T));
        //}

        //public virtual bool RemoveWeakDelegate<T>(Func<T, bool> func) where T : ViewModel
        //{
        //    var type = typeof(T);
        //    WeakReference reference;

        //    if (!this.WeakDelegates.TryGetValue(type, out reference))
        //    {
        //        return false;
        //    }

        //    Func<T, bool> target;

        //    if (!reference.IsAlive || (target = reference.Target as Func<T, bool>) == null
        //        || target.Equals(func))
        //    {
        //        return this.WeakDelegates.Remove(type);
        //    }

        //    return false;
        //}

        protected Navigator()
        {
            this.Delegates = new Dictionary<Type, object>();
            this.WeakDelegates = new Dictionary<Type, WeakReference>();
        }

        protected Dictionary<Type, object> Delegates;

        protected Dictionary<Type, WeakReference> WeakDelegates;
    }
}
