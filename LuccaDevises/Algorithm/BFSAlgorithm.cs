// Coming from https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core
using LuccaDevises.Interface;

namespace LuccaDevises.Algorithm
{
    public class BFSAlgorithm: IShortestPathFinder
    {
        public IEnumerable<string> GetShortestPath(Graph<string> graph, string start, string end)
        {
            Func<string, IEnumerable<string>> shortestPathFunc;

            try
            {
                shortestPathFunc = ShortestPathFunction(graph, start);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("No path possible for the current input");
                throw;
            }

            return shortestPathFunc(end);
        }

        private static Func<T, IEnumerable<T>> ShortestPathFunction<T>(Graph<T> graph, T start) where T : notnull
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, IEnumerable<T>> shortestPath = v =>
            {
                var path = new List<T> { };

                var current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }
    }
}
