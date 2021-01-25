using System;
using System.Collections.Generic;

namespace SImpl.Stack.Runtime.Extensions
{
    public static class EnumerableOfTTopologicalSortExtensions
    {
        public static IEnumerable<T> TopologicalSort<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> dependencyDelegate)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                Visit(item, dependencyDelegate, sorted, visited);
            }

            return sorted;
        }

        private static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, IList<T> sorted, IDictionary<T, bool> visited)
        {
            var alreadyVisited = visited.TryGetValue(item, out var inProcess);
            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        Visit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}