using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Kader_System.Domain.Constant;

namespace Kader_System.Domain.Extensions
{
    public static class TreeNodeExtensions
    {
        public static IEnumerable<T> Values<T>(this IEnumerable<TreeNode<T>> nodes)
        {
            return nodes.Select(n => n.Value);
        }


        public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            var grouped = source.GroupBy(selector);
            var moreThen1 = grouped.Where(i => i.IsMultiple());

            return moreThen1.SelectMany(i => i);
        }

        public static bool IsMultiple<T>(this IEnumerable<T> source)
        {
            var enumerator = source?.GetEnumerator();
            return enumerator != null && enumerator.MoveNext() && enumerator.MoveNext();
        }

        public static IEnumerable<T> ToIEnumarable<T>(this T item)
        {
            yield return item;
        }

        public static string FormatInvariant(this string text, params object[] parameters)
        {
            // This is not the "real" implementation, but that would go out of Scope
            return string.Format(CultureInfo.InvariantCulture, text, parameters);
        }


        //public static IEnumerable<TreeNode<TNode>> ToTree<TNode,TId>(this IList<TNode> nodes, Func<TNode, TId?> idSelector, Func<TNode, TId?> parentIdSelector,TNode root=null) where TNode : class where TId : struct
        //{
        //    if (root != null)
        //    {
        //        nodes.Insert(0,root);
        //    }
            
        //    return TreeNode<TNode>.CreateTree(nodes, idSelector, parentIdSelector);
        //}

        /// <summary>
        /// Convert List of Items To Tree with Root , if no Root or multi Root then use (default root) as Root
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="idSelector"></param>
        /// <param name="parentIdSelector"></param>
        /// <param name="defaultRoot"></param>
        /// <returns></returns>
        public static TreeNode<TNode> ToTree<TNode, TId>(this IList<TNode> nodes, Func<TNode, TId?> idSelector, Func<TNode, TId?> parentIdSelector, TNode defaultRoot) where TNode : class where TId : struct
        {
            var tree = TreeNode<TNode>.CreateTree(nodes, idSelector, parentIdSelector);

            //no root or multi root exist
            if (tree.Count() > 1)
            {
                var root = new TreeNode<TNode>(defaultRoot);
                

                foreach (var node in tree)
                {
                    root.Add(node);
                }

                return root;

            }

            return tree.FirstOrDefault();
        }



        /// <summary>
        /// Loop Throw Tree From Down To Up
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static IEnumerable<TreeNode<TNode>> Traverse<TNode>(this IList<TreeNode<TNode>> tree) where TNode:class
        {
            return tree[0].All.Reverse();
        }

        /// <summary>
        /// Loop Throw Tree Node From Down To Up
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="treeRoot"></param>
        /// <returns></returns>
        public static IEnumerable<TreeNode<TNode>> TraverseNode<TNode>(this TreeNode<TNode> treeRoot) where TNode : class
        {
            return treeRoot.All.Reverse();
        }

        /// <summary>
        /// Search For Specific Node and if Exist return Node and its Children , otherwise return null
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="tree"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TreeNode<TNode> Search<TNode>(this IList<TreeNode<TNode>> tree, Func<TreeNode<TNode>,bool> predicate) where TNode : class
        {
            foreach (var node in tree[0].All)
            {
                if (!node.IsRoot)
                {
                    if (predicate(node))
                    {
                        return node;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Search For Specific Node on All Tree Levels (Parents & Children) and if Exist return Node and its Children , otherwise return null
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="treeRoot">Root Node of Tree</param>
        /// <param name="predicate">Function To Check with every node</param>
        /// <returns></returns>
        public static TreeNode<TNode> SearchNode<TNode>(this TreeNode<TNode> treeRoot, Func<TreeNode<TNode>, bool> predicate) where TNode : class
        {
            return treeRoot.All.Where(node => !node.IsRoot).FirstOrDefault(predicate);
        }


    }
}
