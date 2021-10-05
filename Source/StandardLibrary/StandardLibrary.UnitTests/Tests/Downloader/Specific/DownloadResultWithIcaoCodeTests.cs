// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.Specific;

#endregion

namespace StandardLibrary.UnitTests.Tests.Downloader.Specific
{
    [Category("Unit")]
    [TestFixture]
    public class DownloadResultWithIcaoCodeTests
    {
        public static readonly object[] ValidConstructorDataWithStringTypeCollection =
        {
            new object[] { "downloaded data", TimeSpan.Zero, HttpStatusCode.OK, new IcaoCode("EDDF") },
            new object[] { "downloaded data 2", TimeSpan.MaxValue, HttpStatusCode.Accepted, new IcaoCode("EVRA") },
            new object[] { "Something", TimeSpan.MinValue, HttpStatusCode.Redirect, new IcaoCode("OMDB") },
            new object[] { "", TimeSpan.Zero, HttpStatusCode.Continue, new IcaoCode("LTAI") },
            new object[] { " ", TimeSpan.MaxValue, HttpStatusCode.NotFound, new IcaoCode("LTAC") },
            new object[] { null, TimeSpan.MinValue, HttpStatusCode.BadRequest, new IcaoCode("KJFK") }
        };

        [TestCaseSource(typeof(DownloadResultWithIcaoCodeTests), nameof(ValidConstructorDataWithStringTypeCollection))]
        public void Constructor_WithValidArguments_ShouldConstruct(string downloadedData, TimeSpan downloadingTime,
            HttpStatusCode responseCode, IcaoCode icaoCode)
        {
            //Arrange & act
            DownloadResultWithIcaoCode<string> downloadResult =
                new DownloadResultWithIcaoCode<string>(downloadedData, downloadingTime, responseCode, icaoCode);

            //Assert
            downloadResult.DownloadedData.Should().Be(downloadedData);
            downloadResult.DownloadingTime.Should().Be(downloadingTime);
            downloadResult.ResponseCode.Should().Be(responseCode);
            downloadResult.AirportIcaoCode.Should().Be(icaoCode);
        }

        [Test]
        public void Constructor_WithNullIcaoCode_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once ObjectCreationAsStatement, because we simply need creating an instance.
                // ReSharper disable once CA1806
                new DownloadResultWithIcaoCode<string>(
                    "downloaded data", TimeSpan.Zero,
                    // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
                    HttpStatusCode.OK, null);
            };

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}