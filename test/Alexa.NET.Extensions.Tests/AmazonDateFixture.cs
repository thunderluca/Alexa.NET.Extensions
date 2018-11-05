using Alexa.NET.Extensions.Globalization;
using Alexa.NET.Extensions.Types;
using System;
using Xunit;

namespace Alexa.NET.Extensions.Tests
{
    public class AmazonDateFixture
    {
        [Fact]
        public void AmazonDate_Parse_Should_Throw_When_Value_Is_Null()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AmazonDate.Parse(null, Season.Load("it")));

            Assert.Equal("value", exception.ParamName);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Throw_When_Value_Is_Empty_String()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AmazonDate.Parse(string.Empty, Season.Load("it")));

            Assert.Equal("value", exception.ParamName);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Throw_When_Value_Is_White_Spaces()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AmazonDate.Parse("   ", Season.Load("it")));

            Assert.Equal("value", exception.ParamName);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Not_Throw_When_Value_Is_Valid()
        {
            var exception = Record.Exception(() => AmazonDate.Parse("2018", Season.Load("it")));

            Assert.Null(exception);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Year()
        {
            var amazonDate = AmazonDate.Parse("2018", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 12, 31, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Year_With_Generic_Months()
        {
            var amazonDate = AmazonDate.Parse("2018-XX", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 12, 31, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Year_With_Generic_Months_And_Days()
        {
            var amazonDate = AmazonDate.Parse("2018-XX-XX", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 12, 31, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Date()
        {
            var amazonDate = AmazonDate.Parse("2018-10-20", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 10, 20, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(amazonDate.StartDate, amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Date_With_Generic_Days()
        {
            var amazonDate = AmazonDate.Parse("2018-10-XX", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 10, 1, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 10, 31, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Season()
        {
            var amazonDate = AmazonDate.Parse("2018-WI", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 12, 21, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 3, 20, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Invalid_Week()
        {
            var amazonDate = AmazonDate.Parse("2018-W3", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 15, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 1, 21, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Week()
        {
            var amazonDate = AmazonDate.Parse("2018-W03", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 15, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 1, 21, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Invalid_Week_With_Weekend()
        {
            var amazonDate = AmazonDate.Parse("2018-W3-WE", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 20, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 1, 21, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Week_With_Weekend()
        {
            var amazonDate = AmazonDate.Parse("2018-W03-WE", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 20, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2018, 1, 21, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }

        [Fact]
        public void AmazonDate_Parse_Should_Return_Correct_Instance_When_Value_Is_Valid_Decade()
        {
            var amazonDate = AmazonDate.Parse("201X", Season.Load("it"));

            Assert.NotNull(amazonDate);
            Assert.Equal(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), amazonDate.StartDate);
            Assert.Equal(new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc), amazonDate.EndDate);
        }
    }
}
