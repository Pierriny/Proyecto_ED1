using System;
using System.Collections.Generic;
using System.Text;

public delegate K  GetKeyDelegate<K, V>(V value);

public delegate int CompareKeyDelegate<k>(k key1, k key2);

namespace DataStructurs.ADT
{
    interface IBinarySearchTree<K, V>
    {

     

        void InsertB(K key, V value); //

        V SearchB(K key); //

        V DeleteB(K key);

        List<V> GetListB(); //

        void InOrderB(ITreeTraversal<V> traversal); //

        void PreOrderB(ITreeTraversal<V> traversal); //

        void PostOrderB(ITreeTraversal<V> traversal); //

        bool IsEmptyB();

        int CountB();

        

        //void AddB(K Item, V value,IComparer<K> comparer);


    }
}
