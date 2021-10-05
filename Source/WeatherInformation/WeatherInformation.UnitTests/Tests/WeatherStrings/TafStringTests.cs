// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using TestingHelper.Weather.UsefulData;
using TestingHelper.Weather.WeatherSourceData;
using WeatherInformation.Core.Source;
using WeatherInformation.Core.WeatherStrings;

namespace WeatherInformation.UnitTests.Tests.WeatherStrings
{
    [Category("Unit")]
    [TestFixture]
    public class TafStringTests
    {
        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void Constructor_WithCorrectData_ShouldConstruct(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            TafString tafString = new TafString(icao, data, found, source);

            //Assert
            tafString.AirportIcaoCode.Should().Be(icao);
            tafString.String.Should().Contain(icao.Code);
            tafString.Found.Should().Be(found);
            tafString.Source.Should().Be(source);
        }


        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.IncorrectDataForConstruction))]
        public void Constructor_WithIncorrectDataString_ShouldThrow_ArgumentException(IcaoCode icao,
            string data, bool found, WeatherSource source)
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once UnusedVariable
                TafString tafString = new TafString(icao, data, found, source);
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
                TafString tafString = new TafString(icao, data, found, source);
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
            TafString tafString = new TafString(icao, data, found, source);

            //Assert
            tafString.AirportIcaoCode.Should().Be(icao);
            tafString.String.Should().Contain(icao.Code).And.Contain("Not found");
            tafString.Found.Should().Be(found);
            tafString.Source.Should().Be(source);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void Equals_WithEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            TafString weatherString1 = new TafString(icao, data, found, source);
            TafString weatherString2 = new TafString(icao, data, found, source);

            //Assert
            weatherString1.Should().Be(weatherString2);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void Equals_WithNotEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            TafString weatherString1 = new TafString(icao, data, found, source);
            TafString weatherString2 = new TafString(icao, data ?? "Something not null", !found, source);
            TafString weatherString3 = null;

            //Assert
            weatherString1.Should().NotBe(weatherString2);
            // ReSharper disable once ExpressionIsAlwaysNull
            weatherString1.Should().NotBe(weatherString3);
            // ReSharper disable once ExpressionIsAlwaysNull
            weatherString2.Should().NotBe(weatherString3);
            weatherString1.Should().NotBe(new object());
            weatherString2.Should().NotBe(4);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void GetHashCode_WithEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange
            TafString weatherString1 = new TafString(icao, data, found, source);
            TafString weatherString2 = new TafString(icao, data, found, source);

            //Act & assert
            weatherString1.GetHashCode().Should().Be(weatherString2.GetHashCode());
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void GetHashCode_WithNotEqualObjects_ShouldWorkAsExpected(IcaoCode icao, string data, bool found,
            WeatherSource source)
        {
            //Arrange & act
            TafString weatherString1 = new TafString(icao, data, found, source);
            TafString weatherString2 = new TafString(icao, data ?? "Something not null", !found, source);

            //Act & assert
            weatherString1.Should().NotBe(weatherString2.GetHashCode());
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void TafString_ToString(IcaoCode icao, string data, bool found, WeatherSource source)
        {
            //Arrange
            TafString tafString = new TafString(icao, data, found, source);

            //Act & assert
            tafString.ToString().Should().Contain(tafString.AirportIcaoCode.Code);
        }

        [TestCaseSource(typeof(DataForWeatherStrings), nameof(DataForWeatherStrings.CorrectDataForConstruction))]
        public void TafString_DeepCopy_ShouldCopy(IcaoCode icao, string data, bool found, WeatherSource source)
        {
            //Arrange
            TafString original = new TafString(icao, data, found, source);

            //Act
            TafString copy = original.DeepCopy();

            //Assert
            original.Should().Be(copy);

            //We cannot test "original" for modification because it has no "changeable" field.
        }

        [Test]
        public void TestFormatWithNormalization_ShouldWork()
        {
            //Arrange
            TafString tafString = new TafString(new IcaoCode("UUEE"),
                @"TAF UUEE 281657Z 2818/2918 25005MPS 9999 SCT020 TX09/2912Z TNM00/2903Z  
	        TEMPO 2818/2820 27008G13MPS -SHRA SCT016CB  
	        BECMG 2912/2914 18003MPS", true, DataForWeatherSource.GetDummyWeatherSource());

            const string expectedStringAsString =
                "TAF UUEE 281657Z 2818/2918 25005MPS 9999 SCT020 TX09/2912Z TNM00/2903Z\n\tTEMPO 2818/2820 " +
                "27008G13MPS -SHRA SCT016CB\n\tBECMG 2912/2914 18003MPS";

            //Act & Assert
            tafString.String.Should().Be(expectedStringAsString);
        }
    }
}