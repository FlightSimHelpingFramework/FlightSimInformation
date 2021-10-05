// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System.Collections.Generic;
using WeatherInformation.Core.Enums;
using WeatherInformation.Core.Source;

namespace TestingHelper.Weather.WeatherSourceData
{
    public static class WeatherSourceAsData
    {
        public static IEnumerable<object[]> CorrectWeatherSourceObjects
        {
            get
            {
                return new[]
                {
                    new object[] { new WeatherSource("Name 1", WeatherSourcesCategories.Custom, "Description 1") },
                    new object[]
                        { new WeatherSource("2 Name", WeatherSourcesCategories.Authenticated, "1 Description") },
                    // ReSharper disable once StringLiteralTypo
                    new object[] { new WeatherSource("3 Name 3", WeatherSourcesCategories.Public, "12 dEscription 1") }
                };
            }
        }
    }
}