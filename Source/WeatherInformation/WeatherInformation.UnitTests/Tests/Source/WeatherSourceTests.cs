// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using FluentAssertions;
using NUnit.Framework;
using TestingHelper.Weather.WeatherSourceData;
using WeatherInformation.Core.Enums;
using WeatherInformation.Core.Source;

#endregion

namespace WeatherInformation.UnitTests.Tests.Source
{
    [Category("Unit")]
    [TestFixture]
    public class WeatherSourceTests
    {
        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void Constructor_WithCorrectDataForConstruction_ShouldConstruct(string name,
            WeatherSourcesCategories category,
            string description)
        {
            //Arrange & act
            WeatherSource source = new WeatherSource(name, category, description);

            //Assert
            source.Name.Should().Be(name);
            source.Category.Should().Be(category);
            source.Description.Should().Be(description);
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.IncorrectDataForConstruction))]
        public void Constructor_WithIncorrectDataForConstruction_ShouldThrow_ArgumentException(string name,
            WeatherSourcesCategories category,
            string description)
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once UnusedVariable
                WeatherSource weatherSource = new WeatherSource(name, category, description);
            };

            //Act & assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void Equals_WithEqualObjects_ShouldWorkAsExpected(string name, WeatherSourcesCategories category,
            string description)
        {
            //Arrange & act
            WeatherSource source1 = new WeatherSource(name, category, description);
            WeatherSource source2 = new WeatherSource(name, category, description);

            //Assert
            source1.Should().Be(source2);
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void Equals_WithNotEqualObjects_ShouldWorkAsExpected(string name, WeatherSourcesCategories category,
            string description)
        {
            //Arrange & act
            WeatherSource source1 = new WeatherSource(name, category, description);
            WeatherSource source2 = new WeatherSource(name + "2", category, description);
            WeatherSource source3 = new WeatherSource(name, WeatherSourcesCategories.Custom, description + "2");

            //Assert
            source1.Should().NotBe(source2);
            source1.Should().NotBe(source3);
            source2.Should().NotBe(source3);
            source1.Should().NotBe(new object());
            source2.Should().NotBe(3);
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void GetHashCode_WithEqualObjects_ShouldWorkAsExpected(string name, WeatherSourcesCategories category,
            string description)
        {
            //Arrange & act
            WeatherSource source1 = new WeatherSource(name, category, description);
            WeatherSource source2 = new WeatherSource(name, category, description);

            //Assert
            source1.GetHashCode().Should().Be(source2.GetHashCode());
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void GetHashCode_WithNotEqualObjects_ShouldWorkAsExpected(string name, WeatherSourcesCategories category,
            string description)
        {
            //Arrange & act
            WeatherSource source1 = new WeatherSource(name, category, description);
            WeatherSource source2 = new WeatherSource(name + "2", WeatherSourcesCategories.Custom, description);
            WeatherSource source3 = new WeatherSource(name, WeatherSourcesCategories.Custom, description + "D");

            //Assert
            source1.GetHashCode().Should().NotBe(source2.GetHashCode());
            source2.GetHashCode().Should().NotBe(source3.GetHashCode());
            source3.GetHashCode().Should().NotBe(source1.GetHashCode());
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void WeatherSource_ToString_ShouldWorkAsExpected(string name, WeatherSourcesCategories category,
            string description)
        {
            //Arrange & act
            WeatherSource source = new WeatherSource(name, category, description);
            string resultString = source.ToString();

            //Assert
            resultString.Should().Contain(name).And.Contain(category.ToString()).And.Contain(description);
        }

        [TestCaseSource(typeof(DataForWeatherSource), nameof(DataForWeatherSource.CorrectDataForConstruction))]
        public void WeatherSource_DeepCopy_ShouldCopy(string name, WeatherSourcesCategories category,
            string description)
        {
            //Arrange
            WeatherSource original = new WeatherSource(name, category, description);

            //Act
            WeatherSource copy = original.DeepCopy();

            //Assert
            copy.Should().Be(original);

            //We cannot test "original" for modification because it has no "changeable" field.
        }
    }
}