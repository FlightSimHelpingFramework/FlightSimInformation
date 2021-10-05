// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Downloader.General;

#endregion

namespace StandardLibrary.UnitTests.Tests.Downloader.General
{
    [Category("Unit")]
    [TestFixture]
    public class DownloadResultTests
    {
        public static readonly object[] ValidConstructorDataWithStringTypeCollection =
        {
            new object[] { "downloaded data", TimeSpan.Zero, HttpStatusCode.OK },
            new object[] { "downloaded data 2", TimeSpan.MaxValue, HttpStatusCode.Accepted },
            new object[] { "Something", TimeSpan.MinValue, HttpStatusCode.Redirect },
            new object[] { "", TimeSpan.Zero, HttpStatusCode.Continue },
            new object[] { " ", TimeSpan.MaxValue, HttpStatusCode.NotFound },
            new object[] { null, TimeSpan.MinValue, HttpStatusCode.BadRequest }
        };

        [TestCaseSource(typeof(DownloadResultTests), nameof(ValidConstructorDataWithStringTypeCollection))]
        public void Constructor_WithValidArguments_ShouldConstruct(string downloadedData, TimeSpan downloadingTime,
            HttpStatusCode responseCode)
        {
            //Arrange & act
            DownloadResult<string> downloadResult =
                new DownloadResult<string>(downloadedData, downloadingTime, responseCode);

            //Assert
            downloadResult.DownloadedData.Should().Be(downloadedData);
            downloadResult.DownloadingTime.Should().Be(downloadingTime);
            downloadResult.ResponseCode.Should().Be(responseCode);
        }
    }
}