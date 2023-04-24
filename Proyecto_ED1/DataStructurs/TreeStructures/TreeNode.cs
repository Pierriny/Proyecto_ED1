using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructurs.TreeStructures
{
    public class TreeNode<K, V>
    {
           
        public K valueB { get; set; }
        public TreeNode<K, V> Izquierda { get; set; }
        public TreeNode<K, V> Derecha { get; set; }
        public TreeNode<K, V> Padre { get; set; }
        public K key { get; set; }
        public TreeNode() { }
        

        public TreeNode(K key, V value)
        {
            this.key = key;
            this.value = value;
            this.left = null;
            this.right = null;
            this.parent = null;
        }

        public V value { get; set; }

        public TreeNode<K, V> left { get; set; }

        public TreeNode<K, V> right { get; set; }

        public TreeNode<K, V> parent { get; set; }

        
    }
}
        