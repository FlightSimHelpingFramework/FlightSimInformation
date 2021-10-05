// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Json;
using TestingHelper.IcaoCodes;
using TestingHelper.Strings;

#endregion

namespace StandardLibrary.UnitTests.Tests.Json
{
    #region Data preparation (class)

    internal class SampleClassData
    {
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string icao { get; init; }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string metar { get; init; }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string name { get; init; }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string taf { get; init; }
    }

    #endregion

    [Category("Unit")]
    [TestFixture]
    public class DeserializationTests
    {
        public static IEnumerable<object[]> ValidDataForCreatingSampleClassData
        {
            get
            {
                List<object[]> result = new List<object[]>();

                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (string icaoCode in IcaoCodes.ValidStringIcaoCodesCollection)
                {
                    // ReSharper disable once LoopCanBeConvertedToQuery
                    foreach (object validString in Strings.ValidStrings)
                    {
                        result.Add(new object[]
                        {
                            icaoCode, $"Airport with icao {icaoCode}!", $"METAR {validString}", $"TAF {validString}"
                        });
                    }
                }

                return result;
            }
        }

        [TestCaseSource(typeof(DeserializationTests), nameof(ValidDataForCreatingSampleClassData))]
        public void Deserialize_WithCorrectJsonString_ShouldWork(string icao, string name, string metar, string taf)
        {
            //Arrange & act
            SampleClassData data = Deserialization.Deserialize<SampleClassData>(
                "{\"icao\":\"" + icao + "\",\"name\":\"" + name + "\",\"metar\":\"" + metar + "\",\"taf\":\"" + taf +
                "\"}");

            //Assert
            data.Should().BeEquivalentTo(new SampleClassData { icao = icao, name = name, metar = metar, taf = taf });
        }

        [Test]
        public void Deserialize_WithNullArgument_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
            Action act = () => Deserialization.Deserialize<object>(null);

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}