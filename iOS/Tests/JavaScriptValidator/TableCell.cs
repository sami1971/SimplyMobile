using System;

namespace JavaScriptValidator
{
    public interface ITableCell<T> 
    {
        void Bind(T node);
        void Unbind();
    }
}

