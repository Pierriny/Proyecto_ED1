using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructurs.ADT
{
    public interface ITreeTraversal<T>
    {
        void Walk(T value);
    }
}
