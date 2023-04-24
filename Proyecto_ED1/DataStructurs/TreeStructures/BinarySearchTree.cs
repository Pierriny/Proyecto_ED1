using System;
using System.Collections.Generic;
using System.Text;
using DataStructurs.ADT;

namespace DataStructurs.TreeStructures
{
    public class BinarySearchTree<K, V> : IBinarySearchTree<K, V>
    {
        
        //Variables para implementacion
        private TreeNode<K, V> root;
        private CompareKeyDelegate<K> compareKeys;
        private int count;
        private int comparaciones;
        private int H;

        public BinarySearchTree(CompareKeyDelegate<K> compareKeys)
        {
            this.compareKeys = compareKeys;
            root = null;
            count = 0;
        }

        public int CountB()
        {
            return count;
        }
        public int getComps()
        {
            return comparaciones;
        }
        public int getAltura()
        {
            int n = 0;
            if (this.root != null)
            {
                if (this.root.Izquierda == null && this.root.Derecha == null)
                    H = 1;
                else
                {
                    H = calcularH(this.root, ref n);
                }
            }
            return H;
        }
        public int calcularH(TreeNode<K, V> raiz, ref int n)
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
        public V DeleteB(K key)
        {
            if (IsEmptyB())
            {
                int result = compareKeys(root.key, key);
                //If the node to be deleted is the root

                if (result == 0) //The root must be deleted
                {
                    V tempValue = root.value;

                    if (root.right != null) //Search for the most left of the rights 
                    {
                        TreeNode<K, V> leftOfTheRights = root.right;

                        while (leftOfTheRights.left != null)
                        { //When get the null it means it is the leaft
                            leftOfTheRights = leftOfTheRights.left;
                        }

                        //Assigning the left side
                        leftOfTheRights.left = root.left;
                        if (leftOfTheRights.left != null)
                            leftOfTheRights.left.parent = leftOfTheRights;

                        //Assigning the right side
                        if (compareKeys(root.right.key, leftOfTheRights.key) != 0) //only in case the leftOfTheRights is different from the root's right
                        {
                            //Left node of the parent should be null
                            leftOfTheRights.parent.left = null;

                            TreeNode<K, V> newRootRight = leftOfTheRights;
                            while (newRootRight.right != null)
                            {
                                newRootRight = newRootRight.right;
                            }

                            newRootRight.right = root.right;
                            if (newRootRight.right != null)
                                newRootRight.right.parent = newRootRight;
                        }

                        //Assigning the new parents
                        if (leftOfTheRights.right != null)
                            leftOfTheRights.right.parent = leftOfTheRights;
                        leftOfTheRights.parent = null;
                        root = leftOfTheRights;

                        count--;
                        return tempValue;
                    }
                    else if (root.left != null) //Search for the most right of the lefts 
                    {
                        TreeNode<K, V> rightOfTheLefts = root.left;

                        while (rightOfTheLefts.right != null)
                        { //When get the null it means it is the leaft
                            rightOfTheLefts = rightOfTheLefts.right;
                        }

                        //Assigning the right side
                        rightOfTheLefts.right = root.right;
                        if (rightOfTheLefts.right != null)
                            rightOfTheLefts.right.parent = rightOfTheLefts;

                        //Assigning the left side
                        if (compareKeys(root.left.key, rightOfTheLefts.key) != 0) //only in case the rightOfTheLefts is different from the root's left
                        {
                            //The parent right is null since the node now is the root
                            rightOfTheLefts.parent.right = null;

                            TreeNode<K, V> newRootLeft = rightOfTheLefts;
                            while (newRootLeft.left != null)
                            {
                                newRootLeft = newRootLeft.left;
                            }

                            newRootLeft.left = root.left;
                            if (newRootLeft.left != null)
                                newRootLeft.left.parent = newRootLeft;
                        }

                        //Assigning the new parents
                        if (rightOfTheLefts.left != null)
                            rightOfTheLefts.left.parent = rightOfTheLefts;
                        rightOfTheLefts.parent = null;
                        root = rightOfTheLefts;

                        count--;
                        return tempValue;
                    }
                    else //The root doesn't have childrens
                    {
                        count--;
                        root = null;
                        return tempValue;
                    }
                }
                else
                {
                    //IF the node to be deleted is an intermediate node
                    //If the node bo be deleted is a leaft
                    return internalDelete(root, key, false);
                }

            }
            else //IF is empty then return null 
            {
                return default;
            }

        }

        public List<V> GetListB()
        {
            List<V> tempList = new List<V>();

            internalListElements(tempList, root);

            return tempList;
        }

        public void InOrderB(ITreeTraversal<V> traversal)
        {
            internalInOrder(traversal, root);
        }

        public void InsertB(K key, V value)
        {
            if (!IsEmptyB()) //if the tree is not empty then need to start the internal search
            {
                internalInsertion(root, value, key);
            }
            else //if is empty then new node is the root 
            {
                TreeNode<K, V> newNode = new TreeNode<K, V>(key, value);
                root = newNode;
                count++;
            }
        }

        public bool IsEmptyB()
        {
            return count == 0;
        }

        public void PostOrderB(ITreeTraversal<V> traversal)
        {
            internalPostOrder(traversal, root);
        }

        public void PreOrderB(ITreeTraversal<V> traversal)
        {
            internalPreOrder(traversal, root);
        }

        public V SearchB(K key)
        {
            comparaciones = 0;
            return internalSearch(root, key);
        }

        private V internalDelete(TreeNode<K, V> actual, K key, bool isLeft)
        {
            //Searching for the node to be deleted
            if (actual != null)
            {
                int result = compareKeys(actual.key, key);

                if (result > 0) //actual key is greater than to be deleted key, then need to look into the left side
                {
                    return internalDelete(actual.left, key, true);
                }
                else if (result < 0) //actual key is smaller than to be deleted key, then need to look into the right side
                {
                    return internalDelete(actual.right, key, false);
                }
                else //To be deleted key found
                {
                    if ((actual.left == null) && (actual.right == null)) //A leaft will be deleted 
                    {
                        V tempValue = actual.value;
                        if (isLeft)
                        {
                            actual.parent.left = null;
                        }
                        else
                        {
                            actual.parent.right = null;
                        }
                        count--;
                        return tempValue;
                    }
                    else //An intermediate node will be deleted
                    {

                        //Delete operation starts
                        V tempValue = root.value;

                        if (actual.right != null) //Search for the most left of the rights 
                        {
                            TreeNode<K, V> leftOfTheRights = actual.right;

                            while (leftOfTheRights.left != null)
                            { //When get the null it means it is the leaft
                                leftOfTheRights = leftOfTheRights.left;
                            }

                            //Assigning the left side
                            leftOfTheRights.left = actual.left;
                            if (leftOfTheRights.left != null)
                                leftOfTheRights.left.parent = leftOfTheRights;

                            //Assigning the right side
                            if (compareKeys(actual.right.key, leftOfTheRights.key) != 0) //only in case the leftOfTheRights is different from the root's right
                            {
                                leftOfTheRights.parent.left = null;

                                TreeNode<K, V> newRootRight = leftOfTheRights;
                                while (newRootRight.right != null)
                                {
                                    newRootRight = newRootRight.right;
                                }

                                newRootRight.right = actual.right;
                                if (newRootRight.right != null)
                                    newRootRight.right.parent = newRootRight;
                            }

                            //Assigning the new parents
                            if (leftOfTheRights.right != null)
                                leftOfTheRights.right.parent = leftOfTheRights;
                            leftOfTheRights.parent = null;

                            count--;
                            return tempValue;
                        }
                        else //Search for the most right of the lefts, at least left must exist if not it is a leaft considered in the if statement above
                        {
                            TreeNode<K, V> rightOfTheLefts = actual.left;

                            while (rightOfTheLefts.right != null)
                            { //When get the null it means it is the leaft
                                rightOfTheLefts = rightOfTheLefts.right;
                            }

                            //Assigning the right side
                            rightOfTheLefts.right = actual.right;
                            if (rightOfTheLefts.right != null)
                                rightOfTheLefts.right.parent = rightOfTheLefts;

                            //Assigning the left side
                            if (compareKeys(actual.left.key, rightOfTheLefts.key) != 0) //only in case the rightOfTheLefts is different from the root's left
                            {
                                rightOfTheLefts.parent.right = null;

                                TreeNode<K, V> newRootLeft = rightOfTheLefts;
                                while (newRootLeft.left != null)
                                {
                                    newRootLeft = newRootLeft.left;
                                }

                                newRootLeft.left = actual.left;
                                if (newRootLeft.left != null)
                                    newRootLeft.left.parent = newRootLeft;
                            }

                            //Assigning the new parents
                            if (rightOfTheLefts.left != null)
                                rightOfTheLefts.left.parent = rightOfTheLefts;
                            rightOfTheLefts.parent = null;

                            count--;
                            return tempValue;
                        }
                    }

                    //Delete operation ends
                }
            }
            else
            {
                return default;
            }
        }

        private void internalListElements(List<V> list, TreeNode<K, V> actual)
        {
            if (actual != null)
            {
                internalListElements(list, actual.left);

                list.Add(actual.value);

                internalListElements(list, actual.right);
            }
        }

        private void internalInOrder(ITreeTraversal<V> traversal, TreeNode<K, V> actual)
        {
            if (actual != null)
            {
                internalInOrder(traversal, actual.left);

                traversal.Walk(actual.value);

                internalInOrder(traversal, actual.right);
            }
        }

        private void internalPreOrder(ITreeTraversal<V> traversal, TreeNode<K, V> actual)
        {
            if (actual != null)
            {
                traversal.Walk(actual.value);

                internalPreOrder(traversal, actual.left);

                internalPreOrder(traversal, actual.right);
            }
        }

        private void internalPostOrder(ITreeTraversal<V> traversal, TreeNode<K, V> actual)
        {
            if (actual != null)
            {
                internalPostOrder(traversal, actual.left);

                internalPostOrder(traversal, actual.right);

                traversal.Walk(actual.value);
            }
        }

        private void internalInsertion(TreeNode<K, V> actual, V value, K key)
        {
            int result = compareKeys(actual.key, key);

            if (result > 0) //It means actual key is greater than inserted, go to the left side
            {
                if (actual.left == null) //if left side is null then insert
                {
                    actual.left = new TreeNode<K, V>(key, value);
                    actual.left.parent = actual;
                    count++;
                }
                else //continue searching the insertion place
                {
                    internalInsertion(actual.left, value, key);
                }
            }
            else if (result < 0) //It means actual key is smaller than inserted, go to the right side
            {

                if (actual.right == null) //if right side is null then insert
                {
                    actual.right = new TreeNode<K, V>(key, value);
                    actual.right.parent = actual;
                    count++;
                }
                else //continue searching the insertion place
                {
                    internalInsertion(actual.right, value, key);
                }
            }
        }

        private V internalSearch(TreeNode<K, V> actual, K key)
        {
            comparaciones++;
            if (actual != null)
            {
                int result = compareKeys(actual.key, key); //if result is 0 the keys are equal
                if (result == 0)
                {
                    return actual.value;
                }
                else if (result > 0) //if result > 0 then actual key is greater than searched key, then search the left side
                {
                    return internalSearch(actual.left, key);
                }
                else //if result < 0 then actual key is smaller than searched key, then search the right side
                {
                    return internalSearch(actual.right, key);
                }
            }
            else
            {
                return default;
            }
        }


    }
}
