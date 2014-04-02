using System;
using System.Collections.Generic;

namespace TextSerializationTests
{
    public class E
    {
        public object zero { get; set; }
        public int one { get; set; }
        public int two { get; set; }
        public List<int> three { get; set; }
        public List<int> four { get; set; }
    }

    public class F
    {
        public object g { get; set; }
    }

    public class E2
    {
        public F f { get; set; }
    }

    public class D
    {
        public E2 e { get; set; }
    }

    public class C
    {
        public D d { get; set; }
    }

    public class B
    {
        public C c { get; set; }
    }

    public class A
    {
        public B b { get; set; }
    }

    public class H
    {
        public A a { get; set; }
    }

    public class HighlyNested
    {
        public string a { get; set; }
        public bool b { get; set; }
        public int c { get; set; }
        public List<object> d { get; set; }
        public E e { get; set; }
        public object f { get; set; }
        public H h { get; set; }
        public List<List<List<List<List<List<List<object>>>>>>> i { get; set; }
    }
}

