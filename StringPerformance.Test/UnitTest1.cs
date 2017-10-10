using Xunit;

namespace StringPerformance.Test
{
    public class UnitTest1
    {
        [Fact]
        public void StringCompare_Methods_Equal()
        {
            var stringCompare = new StringCompare();
            Assert.Equal(stringCompare.CompareStringByIndexOf(), stringCompare.CompareStringByStringEquals());
            Assert.Equal(stringCompare.CompareStringByToLower(), stringCompare.CompareStringByStringEquals());
        }

        [Fact]
        public void StringContains_Methods_Equal()
        {
            var stringContain = new StringContain();
            Assert.Equal(stringContain.ContainsStringByIndexOf(), stringContain.ContainsStringByToLower());
        }
    }
}