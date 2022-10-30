using LuccaDevises.Algorithm;

namespace Luccas_Suite_Test.Algorithm
{
    public class BFSAlgorithmTest
    {
        [Test]
        public void BFSAlgorithm_GetResult_WithMultipleExchangePaths_ReturnsShortestPath()
        {
            // Arrange
            // EUR -> USD -> CHF -> JPY -> GBP VS EUR -> JPY -> GBP
            var vertices = new[] { "EUR", "USD", "CHF", "JPY", "GBP" };
            var edges = new[]{Tuple.Create("EUR","USD"), Tuple.Create("USD","CHF"),
                Tuple.Create("CHF","JPY"), Tuple.Create("JPY","GBP"), Tuple.Create("EUR","JPY")};

            var graph = new Graph<string>(vertices, edges);
            var algorithm = new BFSAlgorithm();

            // Act
            var result = algorithm.GetShortestPath(graph, "EUR", "GBP").ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("EUR"));
            Assert.That(result[1], Is.EqualTo("JPY"));
            Assert.That(result[2], Is.EqualTo("GBP"));
        }
    }
}
