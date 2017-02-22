using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace GenericTree
{
    public class TreeNode<T> where T : IComparable
    {
        //properties
        public TreeNode<T> Parent { get; protected set; }

        public TreeRoot<T> Tree   {   get;   protected set;  }

        public ImmutableList<TreeNode<T>> Children { get { return children.ToImmutableList(); } }

        public T Data
        {
            get { return data; }
            set
            {
                if (value != null)
                    data = value; 
            }
        }

        public int ChildCount { get { return children.Count(); } }

        public bool IsEmpty { get { return (children.Count > 0) ? true : false; } }

        //fields
        protected T data;
        protected List<TreeNode<T>> children;

        //constructors
        public TreeNode(TreeNode<T> parent, T data)
        {
            Parent = parent;
            Data = data;

            if (Parent != null)
                Tree = Parent.Tree;
            else
            {
                if ((this as TreeRoot<T>) != null)
                    Tree = (TreeRoot<T>)this;
                else
                    throw new TreeNodeWithoutParentIsNotARootException("TreeNode without a parent should be of type TreeRoot.");
            }


            children = new List<TreeNode<T>>();
        }

        public TreeNode(TreeNode<T> parent, T data, List<TreeNode<T>> children)
            : this(parent, data)
        {
            foreach (var child in children.ToArray())
            {
                child.Tree = this.Tree;
                AddChild(child);
            }
        }

        public TreeNode(TreeNode<T> parent, T data, List<T> children)
            : this(parent, data)
        {
            foreach (var ch in children)
            {
                AddChild(WrapInNode(ch, this));
            }
        }

        //methods
        protected TreeNode<T> WrapInNode(T element, TreeNode<T> parent)
        {
            return new TreeNode<T>(parent, element);
        }
        /// <summary>
        /// Adds the child with Data = element to the current node.
        /// </summary>
        /// <param name="element"></param>
        public void AddChild(T element)
        {
            AddChild(WrapInNode(element, this));
        }

        /// <summary>
        /// Adds the child to the current node.
        /// </summary>
        /// <param name="element"></param>
        public void AddChild(TreeNode<T> node) 
        {
            //if node is a root of another tree
            if (node is TreeRoot<T>)
            {
                node.Parent = this;
            }

            //if node is from another tree
            if (node.Tree != this.Tree)
            {
                if (node.Parent.Tree == this.Tree)
                    RelocateToTheTree(node, this.Tree);
            }

            //if node has a Parent that != this
            if (node.Parent != this)
            {
                node.Parent.children.Remove(node);
                node.Parent = this;
            }

            foreach (var child in node.children)
            {
                //chek wether a future Parent of the node is not in it's Children list
                if (child == this)
                    throw new AttemptToRemoveTreeRoot("Node cannot be simultaniously the parent and the children of another node.");
            }
            foreach (var child in this.children)
            {
                //check wether this node has already instance of node in his children
                if (child == node)
                    throw new TheSameChildOfOneParentException("Node cannot have several same children.");
            }

            children.Add(node);
        }

        private void RelocateToTheTree(TreeNode<T> node, TreeRoot<T> tree)
        {
            if (node != null)
            {
                node.Tree = tree;
                foreach (var child in node.children)
                    RelocateToTheTree(child, tree);
            }
        }

        /// <summary>
        /// Removes all node's children
        /// </summary>
        public void RemoveAllChildren()
        {
            children = new List<TreeNode<T>>();
        }

        /// <summary>
        /// Removes the instance of node in current tree with all it's children, returns the number of deleted nodes 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int Remove(TreeNode<T> node)
        {
            Queue queue = new Queue();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                TreeNode<T> currNode = (TreeNode<T>)queue.Dequeue();

                if (currNode == node)
                {
                    if (currNode.Parent == null)
                        throw new AttemptToRemoveTreeRoot("Use .Clear() to remove TreeRoot.");
                    else
                    {
                        currNode.Parent.children.Remove(node);
                        return currNode.ChildCount + 1;
                    }
                }
                else
                {
                    foreach (var child in currNode.children)
                        queue.Enqueue(child);
                }
            }

            return 0;
        }

        /// <summary>
        /// Removes all nodes where Data = node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int Remove(T node)
        {
            int result = 0;
            Queue queue = new Queue();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                TreeNode<T> currNode = (TreeNode<T>)queue.Dequeue();

                if (currNode.Data.CompareTo(node) == 0)
                {
                    if (currNode.Parent == null)
                        throw new AttemptToRemoveTreeRoot("Use .Clear() to remove TreeRoot.");
                    else
                    {
                    currNode.Parent.children.Remove(currNode);
                        result += currNode.ChildCount + 1;
                    }
                }
                else
                {
                    foreach (var child in currNode.children.ToArray())
                        queue.Enqueue(child);
                }
            }

            return result;
        }

        /// <summary>
        /// Sorts node's children
        /// </summary>
        /// <param name="comparison"></param>
        public void SortChildren(Comparison<TreeNode<T>> comparison)
        {
            children.Sort(comparison);

            foreach (var child in children)
                child.children.Sort(comparison);
        }
    }
}
