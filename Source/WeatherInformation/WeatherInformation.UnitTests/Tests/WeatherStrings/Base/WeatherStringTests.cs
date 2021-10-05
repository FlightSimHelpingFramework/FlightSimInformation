// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using TestingHelper.Weather.UsefulData;
using WeatherInformation.Core.Source;
using WeatherInformation.Core.WeatherStrings;
using WeatherInformation.Core.WeatherStrings.Base;

#endregion

namespace WeatherInformation.UnitTests.Tests.WeatherStrings.Base
{
    [Category("Unit")]
    [TestFixture]
    public class WeatherStringTests
    {
        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void TafString_DeepCopy_ShouldCopy(IcaoCode icao, string data, bool found, WeatherSource source)
        {
            //Arrange
            WeatherString original = new MetarString(icao, data, found, source);

            //Act
            WeatherString copy = original.DeepCopy();

            //Assert
            original.Should().Be(copy);

            //We cannot test "original" for modification because it has no "changeable" field.
        }
    }
}