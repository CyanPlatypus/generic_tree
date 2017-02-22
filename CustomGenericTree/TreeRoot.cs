using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTree
{
    public enum WalkType { PreOrder, InOrder, PostOrder, BreadthFirst }

    public class TreeRoot<T> : TreeNode<T> where T:IComparable
    {
        public TreeRoot(T data) :base (null, data)
        {

        }

        /// <summary>
        /// For each...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="action"></param>
        public void ForEach(WalkType type, Action<TreeNode<T>> action) 
        {
            switch (type)
            {
                case WalkType.BreadthFirst:  { BreadthFirst( this, action); break; }
                case WalkType.InOrder:  { LeftRightRecursion( this, action); break; }
                case WalkType.PostOrder: { DownUpRecursion(this, action);  break; }
                case WalkType.PreOrder: { UpDownRecursion(this, action); break; }
            }
        }

        private void UpDownRecursion(TreeNode<T> node, Action<TreeNode<T>> action)
        {
            if (node != null)
            {
                action(node);

                foreach (var child in node.Children)
                    UpDownRecursion(child, action);
            }
        }

        private void DownUpRecursion(TreeNode<T> node, Action<TreeNode<T>> action)
        {
            if (node != null)
            {
                foreach (var child in node.Children)
                    DownUpRecursion(child, action);

                action(node);
            }
        }

        private void LeftRightRecursion(TreeNode<T> node, Action<TreeNode<T>> action)
        {
            if (node != null)
            {
                foreach (var child in node.Children)
                {
                    if (child.Data.CompareTo(node.Data) > 0)
                        LeftRightRecursion(child, action);
                }
                action(node);
                foreach (var child in node.Children)
                {
                    if (child.Data.CompareTo(node.Data) <= 0)
                        LeftRightRecursion(child, action);
                }
            }
        }

        private void BreadthFirst(TreeNode<T> node, Action<TreeNode<T>> action)
        {
            Queue queue = new Queue();
            queue.Enqueue(node);
            while (queue.Count != 0)
            {
                TreeNode<T> element = (TreeNode<T>)queue.Dequeue();
                action(element);
                foreach (var child in element.Children)
                    queue.Enqueue(child);
            }
        }

        /// <summary>
        /// For each...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="action"></param>
        public void ForEach(WalkType type, Action<T> action)
        {
            switch (type)
            {
                case WalkType.BreadthFirst: { BreadthFirst(this, action); break; }
                case WalkType.InOrder: { LeftRightRecursion(this, action); break; }
                case WalkType.PostOrder: { DownUpRecursion(this, action); break; }
                case WalkType.PreOrder: { UpDownRecursion(this, action); break; }
            }
        }

        private void UpDownRecursion(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Data);

                foreach (var child in node.Children)
                    UpDownRecursion(child, action);
            }
        }

        private void DownUpRecursion(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                foreach (var child in node.Children)
                    DownUpRecursion(child, action);

                action(node.Data);
            }
        }

        private void LeftRightRecursion(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                foreach (var child in node.Children)
                {
                    if (child.Data.CompareTo(node.Data) > 0)
                        LeftRightRecursion(child, action);
                }
                action(node.Data);
                foreach (var child in node.Children)
                {
                    if (child.Data.CompareTo(node.Data) <= 0)
                        LeftRightRecursion(child, action);
                }
            }
        }

        private void BreadthFirst(TreeNode<T> node, Action<T> action)
        {
            Queue queue = new Queue();
            queue.Enqueue(node);
            while (queue.Count != 0)
            {
                TreeNode<T> element = (TreeNode<T>)queue.Dequeue();
                action(element.Data);
                foreach (var child in element.Children)
                    queue.Enqueue(child);
            }
        }
    }
}
