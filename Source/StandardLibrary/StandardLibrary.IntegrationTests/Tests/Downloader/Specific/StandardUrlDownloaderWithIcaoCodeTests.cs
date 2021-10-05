// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.Specific;
using StandardLibrary.Implementations.Downloader.Specific;

#endregion

namespace StandardLibrary.IntegrationTests.Tests.Downloader.Specific
{
    [ExcludeFromCodeCoverage]
    [Category("SkipWhenLiveUnitTesting")]
    [Category("IntegrationWithInternetDataAccess")]
    [TestFixture]
    public class StandardUrlDownloaderWithIcaoCodeTests
    {
        [Test]
        public void DownloadForManyRequestsAsync_WithCollectionWithNullArgument_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            StandardUrlDownloaderWithIcaoCode downloader = new StandardUrlDownloaderWithIcaoCode();
            List<UrlDownloadRequestWithIcaoCode> listOfRequests = new List<UrlDownloadRequestWithIcaoCode>
            {
                null,
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeeeeeeeee.com/"),
                    new IcaoCode("TEST"))
            };

            //Act & assert
            Func<Task> act = async () => { await downloader.DownloadForManyRequestsAsync(listOfRequests); };

            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public async Task DownloadForManyRequestsAsync_WithInvalidDownloadRequest_ShouldReturn_EmptyList()
        {
            //Arrange
            StandardUrlDownloaderWithIcaoCode downloader = new StandardUrlDownloaderWithIcaoCode();
            List<UrlDownloadRequestWithIcaoCode> listOfRequests = new List<UrlDownloadRequestWithIcaoCode>
            {
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeeeeeeeee.com/"),
                    new IcaoCode("TEST"))
            };


            //Act
            List<DownloadResultWithIcaoCode<string>> resultList =
                await downloader.DownloadForManyRequestsAsync(listOfRequests);

            //Assert
            resultList.Should().BeEmpty();
        }

        [Test]
        public async Task
            DownloadForManyRequestsAsync_WithMixedValidAndInvalidDownloadRequests_ShouldDownload_SomethingWithNotNullResult()
        {
            //Arrange
            StandardUrlDownloaderWithIcaoCode downloader = new StandardUrlDownloaderWithIcaoCode();
            List<UrlDownloadRequestWithIcaoCode> listOfRequests = new List<UrlDownloadRequestWithIcaoCode>
            {
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.google.com/"), new IcaoCode("TEST")),
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g23444kygk5eeeeeeeeeeeee.com/"),
                    new IcaoCode("TEST")),
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeeeeeeeee.com/"),
                    new IcaoCode("TEST")),
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g234525roogleeeeeeee978eeeeeeeeeeeeee.com/"),
                    new IcaoCode("TEST")),
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.gmail.com/"), new IcaoCode("TEST")),
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g234525roogleeeeeeee7eeeeeeeeeeeeee.com/"),
                    new IcaoCode("TEST")),
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeee65461eee.com/"),
                    new IcaoCode("TEST"))
            };

            //Act
            List<DownloadResultWithIcaoCode<string>> resultList =
                await downloader.DownloadForManyRequestsAsync(listOfRequests);

            //Assert
            resultList.Should().NotBeEmpty().And.HaveCount(2).And
                .OnlyContain(item => item.ResponseCode == HttpStatusCode.OK)
                .And.NotContainNulls(item => item.DownloadedData);
        }

        [Test]
        public async Task DownloadForManyRequestsAsync_WithNotFoundResourceRequest_ShouldHave_NotFoundResponseCode()
        {
            //Arrange
            StandardUrlDownloaderWithIcaoCode downloader = new StandardUrlDownloaderWithIcaoCode();
            List<UrlDownloadRequestWithIcaoCode> listOfRequests = new List<UrlDownloadRequestWithIcaoCode>
            {
                new UrlDownloadRequestWithIcaoCode(new Uri("https://www.google.com/qwer123_not_eXistS"),
                    new IcaoCode("TEST"))
            };

            //Act
            List<DownloadResultWithIcaoCode<string>> resultList =
                await downloader.DownloadForManyRequestsAsync(listOfRequests);

            //Assert
            resultList.Should().OnlyContain(item =>
                item.ResponseCode == HttpStatusCode.NotFound && item.AirportIcaoCode.Equals(new IcaoCode("TEST")));
        }

        [Test]
        public void DownloadForManyRequestsAsync_WithNullArgument_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            StandardUrlDownloaderWithIcaoCode downloader = new StandardUrlDownloaderWithIcaoCode();

            //Act & assert
            // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
            Func<Task> act = async () => { await downloader.DownloadForManyRequestsAsync(null); };

            act.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}