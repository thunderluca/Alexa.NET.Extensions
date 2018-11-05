using Alexa.NET.Extensions.Globalization;
using Xunit;

namespace Alexa.NET.Extensions.Tests
{
    public class SeasonFixture
    {
        [Fact]
        public void Season_Load_Should_Not_Throw()
        {
            var exception = Record.Exception(() => Season.Load("it"));

            Assert.Null(exception);
        }

        [Fact]
        public void Season_Load_Should_Return_Season_Array()
        {
            var seasons = Season.Load("it");

            Assert.NotNull(seasons);
            Assert.NotEmpty(seasons);
        }
    }
}
