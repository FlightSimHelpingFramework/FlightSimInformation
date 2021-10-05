// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using WeatherInformation.Core.Enums;
using WeatherInformation.Core.Source;
using WeatherInformation.Core.WeatherStrings;

namespace WeatherInformation.UnitTests.Tests.WeatherStrings
{
    [Category("Unit")]
    [TestFixture]
    public class MetarAndTafStringTests
    {
        private readonly TafString _newTafStringForNotEqualChecking = new TafString(new IcaoCode("UUEE"),
            "METAR UUEE NOTEQUAL DATA", true,
            new WeatherSource("FakeSource", WeatherSourcesCategories.Authenticated, "Something"));

        public static IEnumerable<object[]> CorrectDataForConstruction
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new MetarString(new IcaoCode("UUEE"), "METAR UUEE DATA", true,
                            new WeatherSource("TestSource", WeatherSourcesCategories.Authenticated,
                                "A source for testing")),
                        new TafString(new IcaoCode("UUEE"), "TAF UUEE DATA", true,
                            new WeatherSource("TestSource", WeatherSourcesCategories.Custom, "A source for testing"))
                    },
                    new object[]
                    {
                        new MetarString(new IcaoCode("UUDD"), "METAR UUDD DATA", true,
                            new WeatherSource("AnotherTestSource", WeatherSourcesCategories.Authenticated,
                                "Just another source for testing")),
                        new TafString(new IcaoCode("UUDD"), "TAF UUDD DATA", true,
                            new WeatherSource("AnotherTestSource", WeatherSourcesCategories.Public,
                                "Just another source for testing"))
                    },
                    new object[]
                    {
                        new MetarString(new IcaoCode("UUWW"), "METAR UUWW DATA", true,
                            new WeatherSource("CustomTestSource", WeatherSourcesCategories.Custom,
                                "CustomTestSource is just a testing source")),
                        new TafString(new IcaoCode("UUWW"), "TAF UUWW DATA", true,
                            new WeatherSource("CustomTestSource", WeatherSourcesCategories.Authenticated,
                                "CustomTestSource is just a testing source"))
                    }
                };
            }
        }

        public static IEnumerable<object[]> IncorrectDataForConstruction
        {
            get
            {
                MetarString metarString =
                    new MetarString(new IcaoCode("EDDF"), "METAR EDDF DATA", true,
                        new WeatherSource("name", WeatherSourcesCategories.Public, "description"));
                TafString tafString =
                    new TafString(new IcaoCode("EDDF"), "TAF EDDF DATA", true,
                        new WeatherSource("name", WeatherSourcesCategories.Custom, "one more description"));

                return new List<object[]>
                {
                    new object[] { null, tafString },
                    new object[] { metarString, null },
                    new object[] { null, null }
                };
            }
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void Constructor_WithCorrectData_ShouldConstruct(MetarString metarString, TafString tafString)
        {
            //Arrange & act
            MetarAndTafString metarAndTafString = new MetarAndTafString(metarString, tafString);

            //Assert
            metarAndTafString.MetarString.Should()
                .Be(metarString);
            metarAndTafString.TafString.Should()
                .Be(tafString);
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(IncorrectDataForConstruction))]
        public void Constructor_WithNullArguments_ShouldThrow_ArgumentNullException(MetarString metarString,
            TafString tafString)
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once UnusedVariable
                MetarAndTafString metarAndTafString = new MetarAndTafString(metarString, tafString);
            };

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void Equals_WithEqualObjects_ShouldWorkAsExpected(MetarString metarString, TafString tafString)
        {
            //Arrange & act
            MetarAndTafString metarAndTafString1 = new MetarAndTafString(metarString, tafString);
            MetarAndTafString metarAndTafString2 = new MetarAndTafString(metarString, tafString);

            //Assert
            metarAndTafString1.Should().Be(metarAndTafString2);
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void Equals_WithNotEqualObjects_ShouldWorkAsExpected(MetarString metarString, TafString tafString)
        {
            //Arrange & act
            MetarAndTafString metarAndTafString1 = new MetarAndTafString(metarString, tafString);
            MetarAndTafString metarAndTafString2 = new MetarAndTafString(metarString, _newTafStringForNotEqualChecking);
            MetarAndTafString metarAndTafString3 = null;

            //Assert
            metarAndTafString1.Should().NotBe(metarAndTafString2);
            // ReSharper disable once ExpressionIsAlwaysNull
            metarAndTafString2.Should().NotBe(metarAndTafString3);
            // ReSharper disable once ExpressionIsAlwaysNull
            metarAndTafString1.Should().NotBe(metarAndTafString3);
            metarAndTafString1.Should().NotBe(new object());
            metarAndTafString2.Should().NotBe(4);
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void GetHashCode_WithEqualObjects_ShouldWorkAsExpected(MetarString metarString, TafString tafString)
        {
            //Arrange
            MetarAndTafString metarAndTafString1 = new MetarAndTafString(metarString, tafString);
            MetarAndTafString metarAndTafString2 = new MetarAndTafString(metarString, tafString);

            //Act & assert
            metarAndTafString1.GetHashCode().Should().Be(metarAndTafString2.GetHashCode());
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void GetHashCode_WithNotEqualObjects_ShouldWorkAsExpected(MetarString metarString, TafString tafString)
        {
            //Arrange
            MetarAndTafString metarAndTafString1 = new MetarAndTafString(metarString, _newTafStringForNotEqualChecking);
            MetarAndTafString metarAndTafString2 = new MetarAndTafString(metarString, tafString);

            //Act & assert
            metarAndTafString1.GetHashCode().Should().NotBe(metarAndTafString2.GetHashCode());
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void ExplicitConversionToMetarString_WithCorrectData_ShouldWork(MetarString metarString,
            TafString tafString)
        {
            //Arrange
            MetarAndTafString metarAndTafString = new MetarAndTafString(metarString, tafString);

            //Act
            MetarString convertedMetarString = (MetarString)metarAndTafString;

            //Assert
            convertedMetarString.Should().Be(metarString);
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void ExplicitConversionToTafString_WithCorrectData_ShouldWork(MetarString metarString,
            TafString tafString)
        {
            //Arrange
            MetarAndTafString metarAndTafString = new MetarAndTafString(metarString, tafString);

            //Act
            TafString convertedTafString = (TafString)metarAndTafString;

            //Assert
            convertedTafString.Should().Be(tafString);
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void MetarAndTafString_ToString_ShouldWorkAsExpected(MetarString metarString, TafString tafString)
        {
            //Arrange
            MetarAndTafString metarAndTafString = new MetarAndTafString(metarString, tafString);

            //Act & assert
            metarAndTafString.ToString().Should().Contain(metarString.ToString()).And.Contain(tafString.ToString());
        }

        [TestCaseSource(typeof(MetarAndTafStringTests), nameof(CorrectDataForConstruction))]
        public void MetarAndTafString_Clone_ShouldClone(MetarString metarString, TafString tafString)
        {
            //Arrange
            MetarAndTafString original = new MetarAndTafString(metarString, tafString);

            //Act
            MetarAndTafString copy = original.DeepCopy();

            //Assert
            original.Should().Be(copy);

            //We cannot test "original" for modification because it has no "changeable" field.
        }
    }
}