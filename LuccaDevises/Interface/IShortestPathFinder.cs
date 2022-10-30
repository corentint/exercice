using LuccaDevises.Algorithm;

namespace LuccaDevises.Interface
{
    public interface IShortestPathFinder
    {
        IEnumerable<string> GetShortestPath(Graph<string> graph, string start, string end);
    }
}
