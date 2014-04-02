using System;

namespace SimplyMobile
{
    public class JavaObject<T> : Java.Lang.Object
    {
        public JavaObject (T obj)
        {
            this.Value = obj;
        }

        public T Value { get; private set; }
    }
}

