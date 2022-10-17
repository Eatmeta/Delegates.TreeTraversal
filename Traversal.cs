using System;
using System.Collections.Generic;

namespace Delegates.TreeTraversal
{
    public static class Traversal
    {
        private static List<TValue> DoTraversal<TTree, TValue>(
            Func<TTree, IEnumerable<TTree>> enumerable,
            Func<TTree, bool> selector,
            Func<TTree, IEnumerable<TValue>> initial,
            List<TValue> result,
            TTree root)
        {
            if (root == null)
                return new List<TValue>();

            if (selector(root))
                result.AddRange(initial(root));

            foreach (var tree in enumerable(root))
                DoTraversal(enumerable, selector, initial, result, tree);

            return result;
        }

        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            return DoTraversal(
                x => x.Categories,
                x => true,
                x => x.Products,
                new List<Product>(),
                root);
        }

        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> root)
        {
            return DoTraversal(
                x => new List<BinaryTree<T>> {x.Left, x.Right},
                x => x.Left == null && x.Right == null,
                x => new List<T> {x.Value},
                new List<T>(),
                root);
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {
            return DoTraversal(
                x => x.Subjobs,
                x => x.Subjobs.Count == 0,
                x => new List<Job> {x},
                new List<Job>(),
                root);
        }
    }
}