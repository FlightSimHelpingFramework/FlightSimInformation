// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System.Collections.Generic;
using WeatherInformation.Core.Enums;
using WeatherInformation.Core.Source;

namespace TestingHelper.Weather.WeatherSourceData
{
    public static class DataForWeatherSource
    {
        public static IEnumerable<object[]> CorrectDataForConstruction
        {
            get
            {
                return new[]
                {
                    new object[] { "Name 1", WeatherSourcesCategories.Custom, "Description 1" },
                    new object[] { "2 Name", WeatherSourcesCategories.Authenticated, "1 Description" },
                    // ReSharper disable once StringLiteralTypo
                    new object[] { "3 Name 3", WeatherSourcesCategories.Public, "12 dEscription 1" }
                };
            }
        }

        public static IEnumerable<object[]> IncorrectDataForConstruction
        {
            get
            {
                return new[]
                {
                    new object[] { "", WeatherSourcesCategories.Custom, "With blank string" },
                    new object[] { " ", WeatherSourcesCategories.Authenticated, "With whitespace" },
                    new object[] { null, WeatherSourcesCategories.Public, "With null" }
                };
            }
        }

        public static WeatherSource GetDummyWeatherSource()
        {
            return new WeatherSource("Name 1", WeatherSourcesCategories.Custom, "Description 1");
        }
    }
}