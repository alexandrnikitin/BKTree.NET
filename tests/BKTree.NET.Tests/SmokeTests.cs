using System.Linq;
using Xunit;

namespace BKTree.NET.Tests
{
    public class when_searching
    {
        [Fact]
        public void with_max_distance_0_should_return_exact_matches_only()
        {
            var tree = new BKTree<string>(new DamerauLevenshteinStringDistanceMeasurer());

            tree.Add(new BKTreeNode<string>("book")); //root
            tree.Add(new BKTreeNode<string>("rook")); //1
            tree.Add(new BKTreeNode<string>("nooks")); //2
            tree.Add(new BKTreeNode<string>("boon")); //1->2

            const string query = "boon";
            const int maxDistance = 0;
            var matches = tree.Matches(query, maxDistance);

            Assert.Equal(matches.Count, 1);
            Assert.Equal(matches.Single().Data, query);
        }
    }
}