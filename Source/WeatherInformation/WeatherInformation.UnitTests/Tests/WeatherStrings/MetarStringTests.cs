// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using TestingHelper.Weather.UsefulData;
using WeatherInformation.Core.Source;
using WeatherInformation.Core.WeatherStrings;

#endregion

namespace WeatherInformation.UnitTests.Tests.WeatherStrings
{
    [Category("Unit")]
    [TestFixture]
    public class MetarStringTests
    {
        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void Constructor_WithCorrectData_ShouldConstruct(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            MetarString metarString = new MetarString(icao, data, found, source);

            //Assert
            metarString.AirportIcaoCode.Should().Be(icao);
            metarString.String.Should().Contain(icao.Code);
            metarString.Found.Should().Be(found);
            metarString.Source.Should().Be(source);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.IncorrectDataForConstruction))]
        public void Constructor_WithIncorrectDataString_ShouldThrow_ArgumentException(IcaoCode icao,
            string data, bool found, WeatherSource source)
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once UnusedVariable
                MetarString metarString = new MetarString(icao, data, found, source);
            };

            //Act & assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        [TestCaseSource(typeof(DataForWeatherStrings),
            nameof(DataForWeatherStrings.DataForConstructionWithNullIcaoCode))]
        public void Constructor_WithNullIcaoCode_ShouldThrow_ArgumentNullException(IcaoCode icao,
            string data, bool found, WeatherSource source)
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once UnusedVariable
                MetarString metarString = new MetarString(icao, data, found, source);
            };

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.DataWithNotFoundWeatherString))]
        public void Constructor_WithSemiCorrectData_ShouldConstructAndProcessInnerDataToBeSafe(IcaoCode icao,
            string data,
            bool found, WeatherSource source)
        {
            //Arrange & act
            MetarString metarString = new MetarString(icao, data, found, source);

            //Assert
            metarString.AirportIcaoCode.Should().Be(icao);
            metarString.String.Should().Contain(icao.Code).And.Contain("Not found");
            metarString.Found.Should().Be(found);
            metarString.Source.Should().Be(source);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void Equals_WithEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            MetarString weatherString1 = new MetarString(icao, data, found, source);
            MetarString weatherString2 = new MetarString(icao, data, found, source);

            //Assert
            weatherString1.Should().Be(weatherString2);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void Equals_WithNotEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            MetarString weatherString1 = new MetarString(icao, data, found, source);
            MetarString weatherString2 = new MetarString(icao, data ?? "Something not null", !found, source);
            MetarString weatherString3 = null;

            //Assert
            weatherString1.Should().NotBe(weatherString2);
            weatherString1.Should().NotBe(weatherString3);
            weatherString2.Should().NotBe(weatherString3);
            weatherString1.Should().NotBe(new object());
            weatherString2.Should().NotBe(4);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void GetHashCode_WithEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange
            MetarString weatherString1 = new MetarString(icao, data, found, source);
            MetarString weatherString2 = new MetarString(icao, data, found, source);

            //Act & assert
            weatherString1.GetHashCode().Should().Be(weatherString2.GetHashCode());
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void GetHashCode_WithNotEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            MetarString weatherString1 = new MetarString(icao, data, found, source);
            MetarString weatherString2 = new MetarString(icao, data ?? "Something not null", !found, source);

            //Act & assert
            weatherString1.Should().NotBe(weatherString2.GetHashCode());
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void MetarString_ToString(IcaoCode icao, string data, bool found, WeatherSource source)
        {
            //Arrange
            MetarString metarString = new MetarString(icao, data, found, source);

            //Act & assert
            metarString.ToString().Should().Contain(metarString.AirportIcaoCode.Code);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void MetarString_DeepCopy_ShouldCopy(IcaoCode icao, string data, bool found, WeatherSource source)
        {
            //Arrange
            MetarString original = new MetarString(icao, data, found, source);

            //Act
            MetarString copy = original.DeepCopy();

            //Assert
            original.Should().Be(copy);

            //We cannot test "original" for modification because it has no "changeable" field.
        }
    }
}