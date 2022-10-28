using Lucca_Suite.Algorithm;

namespace Lucca_Suite.Interface
{
    public interface IShortestPathFinder
    {
        IEnumerable<string> GetShortestPath(Graph<string> graph, string start, string end);
    }
}
