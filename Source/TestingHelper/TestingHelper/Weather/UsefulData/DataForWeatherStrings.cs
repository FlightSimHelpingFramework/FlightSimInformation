// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System.Collections.Generic;
using StandardLibrary.Codes;
using WeatherInformation.Core.Enums;
using WeatherInformation.Core.Source;

namespace TestingHelper.Weather.UsefulData
{
    public static class DataForWeatherStrings
    {
        public static IEnumerable<object[]> CorrectDataForConstruction
        {
            get
            {
                return new List<object[]>
                {
                    new object[]
                    {
                        new IcaoCode("UUEE"), "UUEE METAR TAF DATA", true,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Custom, "A source for testing")
                    },
                    new object[]
                    {
                        new IcaoCode("UUWW"), "UUWW METAR TAF DATA", true,
                        new WeatherSource("AnotherTestSource", WeatherSourcesCategories.Authenticated,
                            "Another source for testing")
                    },
                    new object[]
                    {
                        new IcaoCode("UUDD"), null, false,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Public, "A source for testing")
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForConstructionWithNullIcaoCode
        {
            get
            {
                return new List<object[]>
                {
                    new object[]
                    {
                        null, "METAR TAF UUEE DATA", true,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Custom, "A source for testing")
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataWithNotFoundWeatherString
        {
            get
            {
                return new List<object[]>
                {
                    new object[]
                    {
                        new IcaoCode("UUEE"), "something", false,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Custom, "A source for testing")
                    },
                    new object[]
                    {
                        new IcaoCode("UUWW"), null, false,
                        new WeatherSource("AnotherTestSource", WeatherSourcesCategories.Authenticated,
                            "Another source for testing")
                    },
                    new object[]
                    {
                        new IcaoCode("UUDD"), " ", false,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Public, "A source for testing")
                    }
                };
            }
        }

        public static IEnumerable<object[]> IncorrectDataForConstruction
        {
            get
            {
                return new List<object[]>
                {
                    new object[]
                    {
                        new IcaoCode("UUEE"), "", true,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Custom, "A source for testing")
                    },
                    new object[]
                    {
                        new IcaoCode("UUWW"), null, true,
                        new WeatherSource("AnotherTestSource", WeatherSourcesCategories.Authenticated,
                            "Another source for testing")
                    },
                    new object[]
                    {
                        new IcaoCode("UUDD"), " ", true,
                        new WeatherSource("TestSource", WeatherSourcesCategories.Public, "A source for testing")
                    }
                };
            }
        }
    }
}