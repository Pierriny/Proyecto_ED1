using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructurs.ADT
{
    interface ISortable<T>
    {
        /// <summary>
        /// Este metodo ordena una lista de elementos
        /// </summary>
        /// <param name="comparer">Necesita una forma de comparar los datos</param>
        void Sort(IComparer<T> comparer);
    }
}
