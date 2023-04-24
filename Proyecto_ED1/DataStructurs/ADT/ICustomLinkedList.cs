using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructurs
{
    interface ICustomLinkedList<T>
    {
        /// <summary>
        /// Inserta un elemento en la lista
        /// </summary>
        /// <param name="value">Dato</param>
        void Insert(T value);
        /// <summary>
        /// Inserta un elemento en la lista en la posicion deseada
        /// </summary>
        /// <param name="value">Dato</param>
        /// <param name="index">Posicion del dato</param>
        void Insert(T value, int index);
        /// <summary>
        /// Elimina un elemento en la posicion deseada
        /// </summary>
        /// <param name="index">posicion del elemento</param>
        void Delete(int index);
        /// <summary>
        /// Regresa un elemento de la posiicion deseada
        /// </summary>
        /// <param name="index">posicion del elemento</param>
        /// <returns></returns>
        T Get(int index);
       /// <summary>
       /// Verifica si la lista esta vacia
       /// </summary>
       /// <returns>true: esta vacia </returns>
        bool isEmpty();
       /// <summary>
       /// retorna el tamano de la lista
       /// </summary>
       /// <returns>tamano de la lista</returns>
        int Count();
        /// <summary>
        /// Busca un dato y retorna su posicion en la custom list
        /// </summary>
        /// <param name="data">Dato que se desea buscar</param>
        ///<param name="comparer">método utilizado para comparar</param>
        /// <returns>posiscion en la lista</returns>
        int Search(T data, IComparer<T> comparer);

        

        //Arbol Binario de Busqueda

        /// <summary>
        /// Insert an element at the start of the list
        /// </summary>
        /// <param name="value">Data that will be added</param>
        void InsertAtStartB(T value);

        /// <summary>
        /// Insert an element at the end of the list
        /// </summary>
        /// <param name="value">The data</param>
        void InsertAtEndB(T value);

        /// <summary>
        /// Insert an element into the list in a specific position
        /// </summary>
        /// <param name="value">The data</param>
        /// <param name="index">Position in the list</param>
        void InsertB(T value, int index);

        /// <summary>
        /// Removes an element from the list
        /// </summary>
        /// <param name="index">Position of the element</param>
        T DeleteB(int index);

        /// <summary>
        /// Delete an element at the start of the list
        /// </summary>
        /// <returns>The deleted element</returns>
        T DeleteAtStartB();

        /// <summary>
        /// Delete an element ath the end of the list
        /// </summary>
        /// <returns>The deleted element</returns>
        T DeleteAtEndB();

        /// <summary>
        /// Returns an element from the list without remove
        /// </summary>
        /// <param name="index">Position of the element</param>
        /// <returns>The element value</returns>
        T GetB(int index);

        /// <summary>
        /// Verify if the list is empty
        /// </summary>
        /// <returns>true if the list is empty and false otherwise</returns>
        bool IsEmptyB();

        /// <summary>
        /// Returns the number of elements into the list
        /// </summary>
        /// <returns></returns>
        int CountB();

        

    }
}
