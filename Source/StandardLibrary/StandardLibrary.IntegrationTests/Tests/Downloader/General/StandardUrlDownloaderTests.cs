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
using StandardLibrary.Downloader.General;
using StandardLibrary.Implementations.Downloader.General;

#endregion

namespace StandardLibrary.IntegrationTests.Tests.Downloader.General
{
    [ExcludeFromCodeCoverage]
    [Category("SkipWhenLiveUnitTesting")]
    [Category("IntegrationWithInternetDataAccess")]
    [TestFixture]
    public class StandardDownloaderTests
    {
        [Test]
        public void DownloadForManyRequestsAsync_WithCollectionWithNullArgument_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            StandardUrlDownloader downloader = new StandardUrlDownloader();
            List<UrlDownloadRequest> listOfRequests = new List<UrlDownloadRequest>
            {
                null,
                new UrlDownloadRequest(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeeeeeeeee.com/"))
            };

            //Act & assert
            Func<Task> act = async () => { await downloader.DownloadForManyRequestsAsync(listOfRequests); };

            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public async Task DownloadForManyRequestsAsync_WithInvalidDownloadRequest_ShouldReturn_EmptyList()
        {
            //Arrange
            StandardUrlDownloader downloader = new StandardUrlDownloader();
            List<UrlDownloadRequest> listOfRequests = new List<UrlDownloadRequest>
            {
                new UrlDownloadRequest(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeeeeeeeee.com/"))
            };

            //Act
            List<DownloadResult<string>> resultList =
                await downloader.DownloadForManyRequestsAsync(listOfRequests);

            //Assert
            resultList.Should().BeEmpty();
        }

        [Test]
        public async Task
            DownloadForManyRequestsAsync_WithMixedValidAndInvalidDownloadRequests_ShouldDownload_SomethingWithNotNullResult()
        {
            //Arrange
            StandardUrlDownloader downloader = new StandardUrlDownloader();
            List<UrlDownloadRequest> listOfRequests = new List<UrlDownloadRequest>
            {
                new UrlDownloadRequest(new Uri("https://www.google.com/")),
                new UrlDownloadRequest(new Uri("https://www.g23444kygk5eeeeeeeeeeeee.com/")),
                new UrlDownloadRequest(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeeeeeeeee.com/")),
                new UrlDownloadRequest(new Uri("https://www.g234525roogleeeeeeee978eeeeeeeeeeeeee.com/")),
                new UrlDownloadRequest(new Uri("https://www.gmail.com/")),
                new UrlDownloadRequest(new Uri("https://www.g234525roogleeeeeeee7eeeeeeeeeeeeee.com/")),
                new UrlDownloadRequest(new Uri("https://www.g234525roogleeeeeeeeeeeeeeeee65461eee.com/"))
            };

            //Act
            List<DownloadResult<string>> resultList =
                await downloader.DownloadForManyRequestsAsync(listOfRequests);

            //Assert
            resultList.Should().NotBeEmpty().And.HaveCount(2).And.OnlyContain(item =>
                    item.ResponseCode == HttpStatusCode.OK && item.DownloadingTime.TotalSeconds > 0)
                .And.NotContainNulls(item => item.DownloadedData);
        }

        [Test]
        public async Task DownloadForManyRequestsAsync_WithNotFoundResourceRequest_ShouldHave_NotFoundResponseCode()
        {
            //Arrange
            StandardUrlDownloader downloader = new StandardUrlDownloader();
            List<UrlDownloadRequest> listOfRequests = new List<UrlDownloadRequest>
            {
                new UrlDownloadRequest(new Uri("https://www.google.com/qwer123_not_eXistS"))
            };

            //Act
            List<DownloadResult<string>> resultList =
                await downloader.DownloadForManyRequestsAsync(listOfRequests);

            //Assert
            resultList.Should().OnlyContain(item => item.ResponseCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void DownloadForManyRequestsAsync_WithNullArgument_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            StandardUrlDownloader downloader = new StandardUrlDownloader();

            //Act & assert
            // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
            Func<Task> act = async () => { await downloader.DownloadForManyRequestsAsync(null); };

            act.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}