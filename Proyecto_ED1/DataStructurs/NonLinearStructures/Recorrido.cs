using System;
using System.Collections.Generic;
using System.Text;
using DataStructurs.ADT;

namespace DataStructurs.NonLinearStructures
{
    public class Recorrido<T> : ITreeTraversal<T>
    {
        public List<T> arbolEnLista = new List<T>();
        public void Walk(T value)
        {
            arbolEnLista.Add(value);
        }
    }
}
