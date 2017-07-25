using System;
using System.Collections.Generic;

namespace BKTree.NET.V2
{
    public class BKTree<T>
    { 
        public BKTreeNode<T> Root { get; private set; }

        public BKTree()
        {
        }

        public void Add(BKTreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (Root == null)
            {
                Root = node;
                return;
            }

            AddTo(Root, node);
        }

        private void AddTo(BKTreeNode<T> subTree, BKTreeNode<T> node)
        {
            var distance = EditDistance.DamerauLevenshteinDistance(subTree.Data as string, node.Data as string, int.MaxValue);

            BKTreeNode<T> child;
            if (!subTree.TryGetChildWith(distance, out child))
            {
                node.DistanceToParentNode = distance;
                subTree.AddChild(node);
                return;
            }

            AddTo(child, node);
        }

        public List<Match<T>> Matches(T query, int maxDistance)
        {
            var results = new List<Match<T>>();
            Matches(Root, query, maxDistance, results);
            return results;
        }

        private void Matches(BKTreeNode<T> subTree, T query, int maxDistance, ICollection<Match<T>> matches)
        {
            var distance = EditDistance.DamerauLevenshteinDistance(subTree.Data as string, query as string, maxDistance);

            if (distance <= maxDistance)
                matches.Add(new Match<T>(subTree.Data, distance));

            var lowerDistance = distance - maxDistance > 0 ? distance - maxDistance : 0;
            var upperDistance = distance + maxDistance;

            for (var i = lowerDistance; i <= upperDistance; i++)
            {
                BKTreeNode<T> child;
                if (subTree.TryGetChildWith(i, out child))
                    Matches(child, query, maxDistance, matches);
            }
        }
    }
}