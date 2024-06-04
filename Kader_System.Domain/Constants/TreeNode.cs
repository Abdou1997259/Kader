using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kader_System.Domain.Extensions;

namespace Kader_System.Domain.Constant
{
    public class TreeNode<T> : IEqualityComparer, IEnumerable<T>, IEnumerable<TreeNode<T>>
    {

        public TreeNode<T> ParentNode { get; private set; }
        public T Value { get; set; }
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();
        
        

        #region Constructors    

        public TreeNode(T value)
        {
            Value = value;
        }


        #endregion

        #region Reterive

        public bool IsRoot => ParentNode == null;
        public TreeNode<T> this[int index] => _children[index];
        public int Level => Ancestors.Count();
        private static bool IsSameId<TId>(TId? id, TId? parentId)
            where TId : struct
        {
            return parentId != null && id.Equals(parentId.Value);
        }
        public bool HasChildren => _children.Count > 0;


        /// <summary>
        /// الاسلاف/الاباء
        /// </summary>
        public IEnumerable<TreeNode<T>> Ancestors => IsRoot ? Enumerable.Empty<TreeNode<T>>() : ParentNode.ToIEnumarable().Concat(ParentNode.Ancestors);

        /// <summary>
        /// الاحفاد
        /// </summary>
        public IEnumerable<TreeNode<T>> Descendants => SelfAndDescendants.Skip(1);

        /// <summary>
        /// الابناء
        /// </summary>
        public IEnumerable<TreeNode<T>> Children => _children;

        /// <summary>
        /// الاخوة
        /// </summary>
        public IEnumerable<TreeNode<T>> Siblings => SelfAndSiblings.Where(Other);

        private bool Other(TreeNode<T> treeNode) => !ReferenceEquals(treeNode, this);

        /// <summary>
        /// النوده والابناء
        /// </summary>
        public IEnumerable<TreeNode<T>> SelfAndChildren => this.ToIEnumarable().Concat(Children);

        /// <summary>
        /// النوده والاسلاف/الاباء
        /// </summary>
        public IEnumerable<TreeNode<T>> SelfAndAncestors => this.ToIEnumarable().Concat(Ancestors);

        /// <summary>
        /// النوده والاحفاد
        /// </summary>
        public IEnumerable<TreeNode<T>> SelfAndDescendants => this.ToIEnumarable().Concat(Children.SelectMany(c => c.SelfAndDescendants));

        /// <summary>
        /// النوده والاخوة
        /// </summary>
        public IEnumerable<TreeNode<T>> SelfAndSiblings => IsRoot ? this.ToIEnumarable() : ParentNode.Children;

        /// <summary>
        /// الشجرة من الاول للاخر
        /// </summary>
        public IEnumerable<TreeNode<T>> All => Root.SelfAndDescendants;

        public IEnumerable<TreeNode<T>> SameLevel => SelfAndSameLevel.Where(Other);
        
        public IEnumerable<TreeNode<T>> SelfAndSameLevel => GetTreeNodesAtLevel(Level);

        public IEnumerable<TreeNode<T>> GetTreeNodesAtLevel(int level) => Root.GetTreeNodesAtLevelInternal(level);

        private IEnumerable<TreeNode<T>> GetTreeNodesAtLevelInternal(int level) => level == Level ? this.ToIEnumarable() : Children.SelectMany(c => c.GetTreeNodesAtLevelInternal(level));

        public TreeNode<T> Root => SelfAndAncestors.Last();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _children.Values().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            return _children.GetEnumerator();
        }


        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion

        #region Add

        public TreeNode<T> Add(T value, int index = -1)
        {
            var childTreeNode = new TreeNode<T>(value);
            Add(childTreeNode, index);
            return childTreeNode;
        }



        public void Add(TreeNode<T> childTreeNode, int index = -1)
        {
            if (index < -1)
            {
                throw new ArgumentException("The index can not be lower then -1");
            }
            if (index > Children.Count() - 1)
            {
                throw new ArgumentException("The index ({0}) can not be higher then index of the last iten. Use the AddChild() method without an index to add at the end " + index);
            }
            if (!childTreeNode.IsRoot)
            {
                throw new ArgumentException("The child TreeNode with value [{0}] can not be added because it is not a root TreeNode." + childTreeNode.Value);
            }

            if (Root == childTreeNode)
            {
                throw new ArgumentException("The child TreeNode with value [{0}] is the rootTreeNode of the parent." + childTreeNode.Value);
            }

            if (childTreeNode.SelfAndDescendants.Any(n => this == n))
            {
                throw new ArgumentException("The childTreeNode with value [{0}] can not be added to itself or its descendants. " + childTreeNode.Value);
            }
            childTreeNode.ParentNode = this;
            if (index == -1)
            {
                _children.Add(childTreeNode);
            }
            else
            {
                _children.Insert(index, childTreeNode);
            }
        }

        public TreeNode<T> AddFirstChild(T value)
        {
            var childTreeNode = new TreeNode<T>(value);
            AddFirstChild(childTreeNode);
            return childTreeNode;
        }

        public void AddFirstChild(TreeNode<T> childTreeNode)
        {
            Add(childTreeNode, 0);
        }

        public TreeNode<T> AddFirstSibling(T value)
        {
            var childTreeNode = new TreeNode<T>(value);
            AddFirstSibling(childTreeNode);
            return childTreeNode;
        }

        public void AddFirstSibling(TreeNode<T> childTreeNode)
        {
            ParentNode.AddFirstChild(childTreeNode);
        }
        public TreeNode<T> AddLastSibling(T value)
        {
            var childTreeNode = new TreeNode<T>(value);
            AddLastSibling(childTreeNode);
            return childTreeNode;
        }

        public void AddLastSibling(TreeNode<T> childTreeNode)
        {
            ParentNode.Add(childTreeNode);
        }

        public TreeNode<T> AddParent(T value)
        {
            var newTreeNode = new TreeNode<T>(value);
            AddParent(newTreeNode);
            return newTreeNode;
        }

        public void AddParent(TreeNode<T> parentTreeNode)
        {
            if (!IsRoot)
            {
                throw new ArgumentException("This TreeNode [{0}] already has a parent " + Value, "parentTreeNode");
            }
            parentTreeNode.Add(this);
        }

        #endregion

        #region Remove

        public void Disconnect()
        {
            if (IsRoot)
            {
                throw new InvalidOperationException("The root TreeNode [{0}] can not get disconnected from a parent. " + Value);
            }
            ParentNode._children.Remove(this);
            ParentNode = null;
        }


        #endregion

        #region Helpers

        public static IEnumerable<TreeNode<T>> CreateTree<TId>(IEnumerable<T> values, Func<T, TId?> idSelector, Func<T, TId?> parentIdSelector) where TId : struct
        {
            var valuesCache = values.ToList();
            if (!valuesCache.Any())
                return Enumerable.Empty<TreeNode<T>>();
            T itemWithIdAndParentIdIsTheSame = valuesCache.FirstOrDefault(v => IsSameId(idSelector(v), parentIdSelector(v)));
            if (itemWithIdAndParentIdIsTheSame != null) // Hier verwacht je ook een null terug te kunnen komen
            {
                throw new ArgumentException("At least one value has the samen Id and parentId " + itemWithIdAndParentIdIsTheSame);
            }

            var treeNodes = valuesCache.Select(v => new TreeNode<T>(v));
            return CreateTree(treeNodes, idSelector, parentIdSelector);

        }

        public static IEnumerable<TreeNode<T>> CreateTree<TId>(IEnumerable<TreeNode<T>> rootTreeNodes, Func<T, TId?> idSelector, Func<T, TId?> parentIdSelector) where TId : struct
        {
            var rootTreeNodesCache = rootTreeNodes.ToList();
            var duplicates = rootTreeNodesCache.Duplicates(n => n).ToList();
            if (duplicates.Any())
            {
                throw new ArgumentException($"One or more values contains {duplicates.Count} duplicate keys. The first duplicate is: [{duplicates[0]}]");
            }

            foreach (var rootTreeNode in rootTreeNodesCache)
            {
                var parentId = parentIdSelector(rootTreeNode.Value);
                var parent = rootTreeNodesCache.FirstOrDefault(n => IsSameId(idSelector(n.Value), parentId));

                if (parent != null)
                {
                    parent.Add(rootTreeNode);
                }
                else if (parentId != null)
                {
                    throw new ArgumentException($"A value has the parent ID [{parentId.Value}] but no other TreeNodes has this ID");
                }
            }
            var result = rootTreeNodesCache.Where(n => n.IsRoot);
            return result;
        }

        

        #region Equals en ==

        public static bool operator ==(TreeNode<T> value1, TreeNode<T> value2)
        {
            if ((object)(value1) == null && (object)value2 == null)
            {
                return true;
            }
            return ReferenceEquals(value1, value2);
        }

        public static bool operator !=(TreeNode<T> value1, TreeNode<T> value2)
        {
            return !(value1 == value2);
        }

        public override bool Equals(Object anderePeriode)
        {
            var valueThisType = anderePeriode as TreeNode<T>;
            return this == valueThisType;
        }

        public bool Equals(TreeNode<T> value)
        {
            return this == value;
        }

        public bool Equals(TreeNode<T> value1, TreeNode<T> value2)
        {
            return value1 == value2;
        }

        bool IEqualityComparer.Equals(object value1, object value2)
        {
            var valueThisType1 = value1 as TreeNode<T>;
            var valueThisType2 = value2 as TreeNode<T>;

            return Equals(valueThisType1, valueThisType2);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode(obj as TreeNode<T>);
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public int GetHashCode(TreeNode<T> value)
        {
            return base.GetHashCode();
        }
        #endregion

        #endregion

    }
}
