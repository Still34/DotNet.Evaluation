using Xunit;

namespace UriVsRegEx.Test
{
    public class UriVsRegExTest
    {
        [Fact]
        public void RegExAndUri_Equal()
        {
            var regexResult = UriVsRegex.ContainsUrl_RegEx();
            var uriResult = UriVsRegex.ContainsUrl_Uri();

            Assert.Equal(regexResult, uriResult);
        }
    }
}