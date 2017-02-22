using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeRoot<int> myTree = new TreeRoot<int>(7);

            for (int i = 4; i < 10; i += 2)
                myTree.AddChild(i);
            for (int i = 1; i < 11; i += 3)
                myTree.Children[0].AddChild(i);
            for (int i = 1; i < 8; i += 2)
                myTree.Children[2].AddChild(i);


            Console.WriteLine("\nTree");
            DrawTree(myTree);

            myTree.SortChildren((a, b) => { return (b.Data - a.Data); });
            Console.WriteLine("\nSorted Tree");
            DrawTree(myTree);

            
            myTree.ForEach(WalkType.BreadthFirst, (x => x.Data = 6));
            Console.WriteLine("\nBF test");
            DrawTree(myTree);
            myTree.ForEach(WalkType.InOrder, (x => x.Data = 6));
            Console.WriteLine("\nIO test");
            DrawTree(myTree);
            myTree.ForEach(WalkType.PostOrder, (x => x.Data = 6));
            Console.WriteLine("\nPostO test");
            DrawTree(myTree);
            myTree.ForEach(WalkType.PreOrder, (x => x.Data = 6));
            Console.WriteLine("\nPreO test");
            DrawTree(myTree);

            TreeRoot<int> anotherTree = new TreeRoot<int>(8);
            for (int i = 4; i < 10; i += 2)
                anotherTree.AddChild(i);
            for (int i = 1; i < 11; i += 3)
                anotherTree.Children[0].AddChild(i);
            for (int i = 1; i < 8; i += 2)
                anotherTree.Children[2].AddChild(i);

            anotherTree.Children[1].AddChild(myTree.Children[0]);
            Console.WriteLine("\nRemove node from one tree to another");
            DrawTree(myTree);


            anotherTree.AddChild(myTree);
            Console.WriteLine("\nMerging trees");
            DrawTree(anotherTree);

            anotherTree.Remove(anotherTree.Children[0].Data);
            Console.WriteLine("\nRemove test");
            DrawTree(anotherTree);

            TreeRoot<string> mystringtree = new TreeRoot<string>("Yani");
            mystringtree.AddChild("Ava");
            mystringtree.Children.Add(new TreeNode<string>(mystringtree, "youCannotAddNodesThisWay"));
            mystringtree.AddChild("Yuma");
            mystringtree.AddChild("Ole");
            mystringtree.AddChild("Ire");
            mystringtree.Children[1].AddChild("Ima");
            mystringtree.Children[1].AddChild("Echo");
            mystringtree.Children[1].AddChild("Uta");
            mystringtree.Children[1].AddChild("Ani");
            mystringtree.SortChildren((x, y)=> { return x.Data.CompareTo(y.Data); });

            Console.WriteLine("\nString tree with childrenSorting");
            DrawTree(mystringtree);

            Console.ReadKey();
        }

        static void DrawTree<T>(TreeRoot<T> tree) where T:IComparable
        {
            RecursiveDraw(tree, string.Empty);
        }

        static void RecursiveDraw<T>(TreeNode<T> element, string prefix) where T : IComparable
        {
            if (element != null)
            {
                Console.WriteLine(prefix + element.Data.ToString());

                foreach (var child in element.Children)
                    RecursiveDraw(child, prefix + "-");
            }
        }
    }
}
