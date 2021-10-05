// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using FluentAssertions;
using NUnit.Framework;
using WeatherInformation.Core.Statistics;
using WeatherInformation.Core.WeatherStrings;

namespace WeatherInformation.UnitTests.Tests.Statistics
{
    [Category("Unit")]
    [TestFixture]
    public class InformationStatisticsTests
    {
        [Test]
        public void Constructor_ShouldProcessInformationTypeCorrectly()
        {
            //Arrange & act
            InformationStatistics<MetarString> testStatistics = new InformationStatistics<MetarString>();

            //Assert
            testStatistics.StatisticsForType.Should().Be(typeof(MetarString));
        }

        [Test]
        public void SetValueOfNumberOfRequests()
        {
            //Arrange & act
            InformationStatistics<object> testStatistics = new InformationStatistics<object>();
            int initialValue = testStatistics.NumberOfRequestsToDownloadInformation;
            const int expectedValue = 9;
            testStatistics.NumberOfRequestsToDownloadInformation = expectedValue;

            //Assert
            initialValue.Should().Be(0);
            testStatistics.NumberOfRequestsToDownloadInformation.Should().Be(expectedValue);
        }
    }
}