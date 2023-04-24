using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStructurs.ADT;

namespace DataStructurs.NonLinearStructures
{
    public class AVLTree<T> 
    {
        private NodeAVL<T> Raiz;
        private int Comparaciones { get; set; }
        private int H { get; set; }
        private int rotaciones { get; set; }
        private int nodes { get; set; }
        public virtual bool isEmpty
        {
            get { return this.Raiz == null; }
        }
        public AVLTree()
        {
            this.Raiz = null;
        }
        public AVLTree(T item = default(T), AVLTree<T> ArbolIzquierdo = null, AVLTree<T> ArbolDerecho = null)
        {
            this.Raiz = new NodeAVL<T>(item);
            if (ArbolIzquierdo != null)
            {
                this.Raiz.Izquierda = ArbolIzquierdo.Raiz;
            }
            if (ArbolDerecho != null)
            {
                this.Raiz.Derecha = ArbolDerecho.Raiz;
            }
        }
        public int getComparaciones()
        {
            return Comparaciones;
        }

        public int getAltura()
        {
            int n = 0;
            if (this.Raiz != null)
            {
                if (this.Raiz.Izquierda == null && this.Raiz.Derecha == null)
                    H = 1;
                else
                {
                    H = calcularH(this.Raiz, ref n);
                }
            }
            return H;
        }


        public int getRotaciones()
        {
            return rotaciones;
        }

        public int getNodes()
        {
            return nodes;
        }
        public int calcularH(NodeAVL<T> raiz, ref int n)
        {
            n++;
            if (raiz.Izquierda != null)
            {
                n = calcularH(raiz.Izquierda, ref n);
            }
            else
            {
                if (raiz.Derecha != null)
                {
                    n = calcularH(raiz.Derecha, ref n);
                }
            }
            return n;
        }

        public virtual void InOrder(ITreeTraversal<T> traversal)
        {
            InOrder(this.Raiz, traversal);
        }
        public virtual void InOrder(NodeAVL<T> raiz, ITreeTraversal<T> traversal)
        {
            if (raiz != null)
            {
                InOrder(raiz.Izquierda, traversal);
                traversal.Walk(raiz.value);
                InOrder(raiz.Derecha, traversal);
            }
        }
        public virtual Boolean Contains(T item, IComparer<T> comparer)
        {
            return Contains(this.Raiz, item, comparer);
        }
        public virtual Boolean Contains(NodeAVL<T> raiz, T item, IComparer<T> comparer)
        {
            if (raiz == null)
            {
                return false;
            }
            if (comparer.Compare(item, raiz.value) < 0)
            {
                return Contains(raiz.Izquierda, item, comparer);
            }
            else
            {
                if (comparer.Compare(item, raiz.value) > 0)
                {
                    return Contains(raiz.Derecha, item, comparer);
                }
            }
            return true;
        }
        public virtual T Find(T item, IComparer<T> comparer)
        {
            Comparaciones = 0;
            return Find(this.Raiz, item, comparer);
        }
        public virtual T Find(NodeAVL<T> raiz, T item, IComparer<T> comparer)
        {
            if (raiz == null) 
            {
                return default(T);
            }
            Comparaciones++;
            if (comparer.Compare(item, raiz.value) < 0)
            {
                return Find(raiz.Izquierda, item, comparer);
            }
            else
            {
                if (comparer.Compare(item, raiz.value) > 0) 
                {
                    return Find(raiz.Derecha, item, comparer);
                }
            }
            return raiz.value;
        }
        public virtual void Add(T item, IComparer<T> comparer)
        {
            bool flag = false;
            rotaciones = 0;
            this.Raiz = AddAVL(this.Raiz, item, ref flag, comparer);
        }
        public virtual NodeAVL<T> AddAVL(NodeAVL<T> raiz, T item, ref bool flag, IComparer<T> comparer)
        {
            NodeAVL<T> n1;
            if (raiz == null)
            {
                raiz = new NodeAVL<T>(item);
                flag = true;
                nodes++;
            }
            else
            {
                if (comparer.Compare(item, raiz.value) < 0)
                {
                    raiz.Izquierda = AddAVL(raiz.Izquierda, item, ref flag, comparer);
                    if (flag)
                    {
                        switch (raiz.balance)
                        {
                            case -1:
                                raiz.balance = 0;
                                flag = false;
                                break;
                            case 0:
                                raiz.balance = 1;
                                break;
                            case 1:
                                n1 = raiz.Izquierda;
                                if (n1.balance == 1)
                                {
                                    raiz = LeftLeftRotation(raiz, n1);

                                }
                                else
                                {
                                    raiz = LeftRightRotation(raiz, n1);

                                }
                                flag = false;
                                break;
                        }
                    }
                }
                else 
                {
                    if (comparer.Compare(item, raiz.value) > 0)
                    {
                        raiz.Derecha = AddAVL(raiz.Derecha, item, ref flag, comparer);
                        if (flag)
                        {
                            switch (raiz.balance)
                            {
                                case -1:
                                    n1 = raiz.Derecha;
                                    if (n1.balance == -1)
                                    {
                                        raiz = RightRightRotation(raiz, n1);

                                    }
                                    else
                                    {
                                        raiz = RightLeftRotation(raiz, n1);
                                    }
                                    flag = false;
                                    break;
                                case 0:
                                    raiz.balance = -1;                                  
                                    break;
                                case 1:
                                    raiz.balance = 0;
                                    flag = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //error
                    }
                }
            }
            return raiz;
        }

        public virtual void Remove(T item, IComparer<T> comparer)
        {
            bool flag = false;
            this.Raiz = RemoveAVL(this.Raiz, item, ref flag, comparer);
        }
        public virtual NodeAVL<T> RemoveAVL(NodeAVL<T> raiz, T item, ref bool flag, IComparer<T> comparer)
        {
            if (raiz == null)
            {
                //error
            }
            if (comparer.Compare(item, raiz.value) < 0)
            {
                raiz.Izquierda = RemoveAVL(raiz.Izquierda, item, ref flag, comparer);
                if (flag)
                {
                    raiz = LeftBalance(raiz, ref flag);
                }

            }
            else
            {
                if (comparer.Compare(item, raiz.value) > 0)
                {
                    raiz.Derecha = RemoveAVL(raiz.Derecha, item, ref flag, comparer);
                    if (flag)
                    {
                        raiz = RightBalance(raiz, ref flag);
                    }
                }
                else
                {
                    NodeAVL<T> q;
                    q = raiz;
                    if (q.Izquierda == null)
                    {
                        raiz = q.Derecha;
                        flag = true;
                    }
                    else
                    {
                        if (q.Derecha == null)
                        {
                            raiz = q.Izquierda;
                            flag = true;
                        }
                        else
                        {
                            raiz.Izquierda = Replace(raiz, raiz.Izquierda, ref flag);
                            if (flag)
                            {
                                raiz = LeftBalance(raiz, ref flag);
                            }
                            q = null;
                        }
                    }
                }
            }
            return raiz;
        }
        public virtual NodeAVL<T> Replace(NodeAVL<T> n, NodeAVL<T> act, ref bool flag)
        {
            if (act.Derecha != null)
            {
                act.Derecha = Replace(n, act.Derecha, ref flag);
                if (flag)
                {
                    act = RightBalance(act, ref flag);
                }
            }
            else
            {
                n.value = act.value;
                n = act;
                act = act.Izquierda;
                n = null;
                flag = true;
            }
            return act;
        }
        public virtual NodeAVL<T> LeftBalance(NodeAVL<T> n, ref bool flag)
        {
            NodeAVL<T> n1;
            switch (n.balance)
            {
                case 1:
                    n.balance = 0;
                    break;
                case 0:
                    n.balance = -1;
                    flag = false;
                    break;
                case -1:
                    n1 = n.Derecha;
                    if (n1.balance <= 0)
                    {
                        flag = false;
                        n = RightRightRotation(n, n1);
                    }
                    else
                    {
                        n = RightLeftRotation(n, n1);
                    }
                    break;
            }
            return n;
        }
        public virtual NodeAVL<T> RightBalance(NodeAVL<T> n, ref bool flag)
        {
            NodeAVL<T> n1;
            switch (n.balance)
            {
                case 1:
                    n1 = n.Izquierda;
                    if (n1.balance >= 0)
                    {
                        if (n1.balance == 0)
                        {
                            flag = false;
                        }
                        n = LeftLeftRotation(n, n1);
                    }
                    else
                    {
                        n = LeftRightRotation(n, n1);
                    }
                    break;
                case 0:
                    n.balance = 1;
                    flag = false;
                    break;
                case -1:
                    n.balance = 0;
                    break;
            }
            return n;
        }
        public virtual NodeAVL<T> LeftLeftRotation(NodeAVL<T> n, NodeAVL<T> n1)
        {
            rotaciones++;
            n.Izquierda = n1.Derecha;
            n1.Derecha = n;
            if (n1.balance == 1)
            {
                n.balance = 0;
                n1.balance = 0;
            }
            else
            {
                n.balance = 1;
                n1.balance = -1;
            }
            return n1;
        }
        public virtual NodeAVL<T> LeftRightRotation(NodeAVL<T> n, NodeAVL<T> n1)
        {
            rotaciones++;
            NodeAVL<T> n2 = n1.Derecha;
            n.Izquierda = n2.Derecha;
            n2.Derecha = n;
            n1.Derecha = n2.Izquierda;
            n2.Izquierda = n1;
            n1.balance = (n2.balance == -1) ? 1 : 0;
            n.balance = (n2.balance == 1) ? -1 : 0;
            n2.balance = 0;
            return n2;
        }
        public virtual NodeAVL<T> RightRightRotation(NodeAVL<T> n, NodeAVL<T> n1)
        {
            rotaciones++;
            n.Derecha = n1.Izquierda;
            n1.Izquierda = n;
            if (n1.balance == -1)
            {
                n.balance = 0;
                n1.balance = 0;
            }
            else
            {
                n.balance = -1;
                n1.balance = -1;
            }
            return n1;
        }
        public virtual NodeAVL<T> RightLeftRotation(NodeAVL<T> n, NodeAVL<T> n1)
        {
            rotaciones++;
            NodeAVL<T> n2 = n1.Izquierda;
            n.Derecha = n2.Izquierda;
            n2.Izquierda = n;
            n1.Izquierda = n2.Derecha;
            n2.Derecha = n1;
            n.balance = (n2.balance == -1) ? 1 : 0;
            n1.balance = (n2.balance == 1) ? -1 : 0;
            n2.balance = 0;
            return n2;
        }
        public void Sort(IComparer<T> comparer)
        {
            Recorrido<T> recorrido = new Recorrido<T>();
            foreach (var item in recorrido.arbolEnLista as List<T>)
            {

            }

        }
    }
}