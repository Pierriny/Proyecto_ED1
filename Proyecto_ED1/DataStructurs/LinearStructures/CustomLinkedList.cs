using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStructurs.ADT;

namespace DataStructurs.LinearStructures
{
    public delegate void SortMethod();

    public class CustomLinkedList<T> : ICustomLinkedList<T>, IEnumerable<T>, ISortable<T>
    {
        private int count;
        private Node<T> start;
        private Node<T> end;
        public int Count()
        {
            return count;
        }

        public void Delete(int index)
        {
            int indexDelete = 0;
            Node<T> auxDelete = start;
            if (index < count)
            {
                if (index == 0)
                {
                    start = start.next;
                    start.previous = end;
                    count--;
                }
                else if (index == count - 1)
                {
                    end = end.previous;
                    end.next = null;
                    count--;
                }
                else
                {
                    while (indexDelete < index && auxDelete != null)
                    {
                        indexDelete++;
                        auxDelete = auxDelete.next;
                    }
                    auxDelete.previous.next = auxDelete.next;
                    auxDelete.next.previous = auxDelete.previous;
                    count--;
                }
            }
        }

        public T Get(int index)
        {
            T dato;
            if (!isEmpty() && index >= 0)
            {

                int buscar = 0;
                Node<T> temp = start;
                while (buscar < index)
                {
                    buscar++;
                    temp = temp.next;
                }
                dato = temp.value;
                if(dato != null) return dato;
            }
            return default;
        }


        public void Insert(T value)
        {
            Node<T> nuevoNodo = new Node<T>(value);
            if (isEmpty())
            {
                start = nuevoNodo;
                end = nuevoNodo;
            }
            else 
            {
                Node<T> temp = end;
                start.previous = nuevoNodo;
                end.next = nuevoNodo;
                end = nuevoNodo;
                end.previous = temp;
                end.next = null;
            }
            count++;
        }

        public void Insert(T value, int index)
        {
            Node<T> nuevoNodo = new Node<T>(value);
            if (index <= count)
            {
                if (isEmpty())
                {
                    start = nuevoNodo;
                    end = nuevoNodo;
                }
                else
                {
                    int posicion = 0;
                    Node<T> AuxInsertar = start;
                    while (posicion < index)
                    {
                        posicion++;
                        AuxInsertar = AuxInsertar.next;
                    }
                    nuevoNodo.next = AuxInsertar.next;
                    AuxInsertar.next = nuevoNodo;
                    nuevoNodo.previous = AuxInsertar;
                }
                count++;
            }
        }

        public bool isEmpty()
        {
            return (count == 0);
        }

        public int Search(T data, IComparer<T> comparer)
        {
            Node<T> sData = new Node<T>();
            sData.value = data;
            Node<T> elementoA = start;
            int size = Count();
            for (int i = 0; i < size; i++)
            {
                if (sData.value != null && elementoA.value != null)
                {
                    if (comparer.Compare(sData.value, elementoA.value) == 0)
                    {
                        return i;
                    }
                }
                elementoA = elementoA.next;
            }
            return -1;
        }
        //
        //IEnumerable
        //
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            var node = start;
            while (node != null)
            {
                yield return node.value;
                node = node.next;
            }
        }
        //
        //ISortable
        //
        public void Sort(IComparer<T> comparer)
        {
            if (Count() <= 1) return;
            Node<T> first = start;
            Node<T> elementoA = start;
            Node<T> elementoB = start;
            for (int i = 0; i < Count() - 1; i++)
            {
                for (int j = 0; j < Count() - 1; j++)
                {
                    elementoB = elementoA.next;
                    if (comparer.Compare(elementoA.value, elementoB.value) > 0)
                    {
                        Swap(ref elementoA, ref elementoB);
                    }
                }
            }
        }

        public void Swap(ref Node<T> a, ref Node<T> b)
        {
            T aux = b.value;
            b.value = a.value;
            a.value = aux;
        }

        //

        public int CountB()
        {
            return count;
        }

        public bool IsEmptyB()
        {
            return (count == 0);
        }
        
        public void InsertAtStartB(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (IsEmptyB())
            {
                start = newNode;
                end = newNode;
            }
            else
            {
                newNode.next = start;
                start = newNode;
            }

            count++;
        }

        public void InsertAtEndB(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (IsEmptyB())
            {
                start = newNode;
                end = newNode;
            }
            else
            {
                end.next = newNode;
                end = newNode;
            }
            count++;
        }

        public void InsertB(T value, int index)
        {
            if (IsEmptyB()) //if the list is empty then insert at start
            {
                InsertAtStartB(value);
            }
            else
            {
                if (index >= CountB()) //if the index is equal or greater than count then insert at end
                {
                    InsertAtEndB(value);
                }
                else if (index == 0) //If the index to insert is 0 and the list is not empty
                {
                    InsertAtStartB(value);
                }
                else if ((index > 0) && (index < CountB())) //Index between 1 (second element) and Count() - 1 previous the last one
                {
                    Node<T> newNode = new Node<T>(value);
                    Node<T> pretemp = start;
                    Node<T> temp = start.next;
                    int i = 1;

                    //Search the position where the node will be inserted
                    while ((temp != null) && (i < index))
                    {
                        pretemp = temp;
                        temp = temp.next;
                        i++;
                    }

                    //doing the insertion
                    newNode.next = temp;
                    pretemp.next = newNode;
                    count++;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }

            }
        }

        public T DeleteB(int index)
        {

            if (index == 0)
            {
                return DeleteAtStartB();
            }
            else if (index == (CountB() - 1))
            {
                return DeleteAtEndB();
            }
            else if ((index > 0) && (index < (CountB() - 1)))
            {
                Node<T> pretemp = start;
                Node<T> temp = start.next;
                int i = 1;

                //Search the position where the node will be inserted
                while ((temp != null) && (i < (CountB() - 1)))
                {
                    pretemp = temp;
                    temp = temp.next;
                    i++;
                }

                //Delete the node
                pretemp.next = temp.next;
                count--;
                return temp.value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public T DeleteAtStartB()
        {
            if (!IsEmptyB())
            {
                Node<T> temp = start;
                start = start.next;
                count--;
                return temp.value;
            }

            return default;
        }

        public T DeleteAtEndB()
        {
            if (!IsEmptyB())
            {

                if (CountB() == 1) //Only one node then delete
                {
                    Node<T> temp = start;
                    start = null;
                    end = null;
                    count--;
                    return temp.value;
                }
                else
                {
                    Node<T> pretemp = start;
                    Node<T> temp = start.next;

                    //Search the position where the node will be inserted
                    while (temp != null)
                    {
                        pretemp = temp;
                        temp = temp.next;
                    }

                    //Delete the node
                    end = pretemp;
                    end.next = null;
                    count--;
                    return temp.value;
                }

            }

            return default;
        }

        public T GetB(int index)
        {
            if (!IsEmptyB())
            {
                if (index == 0)
                {
                    return start.value;
                }
                else if (index == (CountB() - 1))
                {
                    return end.value;
                }
                else if ((index > 0) && (index < (CountB() - 1)))
                {
                    Node<T> temp = start;
                    int i = 0;
                    while ((temp != null) && (i != index))
                    {
                        temp = temp.next;
                        i++;
                    }

                    if (temp != null)
                    {
                        return temp.value;
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            return default;
        }

        public void SortUsingMethods(SortMethod preferedSortMethod)
        {
            preferedSortMethod();
        }
 

    }
}
