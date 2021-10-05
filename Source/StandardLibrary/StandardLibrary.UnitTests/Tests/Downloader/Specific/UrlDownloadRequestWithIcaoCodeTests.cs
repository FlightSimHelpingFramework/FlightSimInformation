// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.Specific;
using TestingHelper.Downloader;

#endregion

namespace StandardLibrary.UnitTests.Tests.Downloader.Specific
{
    [Category("Unit")]
    [TestFixture]
    public class UrlDownloadRequestWithIcaoCodeTests
    {
        [TestCaseSource(typeof(Urls), nameof(Urls.ValidUrlsCollection))]
        public void Constructor_WithValidParameters_ShouldConstruct(Uri url)
        {
            //Arrange
            IcaoCode expectedIcaoCode = new IcaoCode("UUEE");

            //Act
            UrlDownloadRequestWithIcaoCode request = new UrlDownloadRequestWithIcaoCode(url, expectedIcaoCode);

            //Assert
            request.Url.Should().Be(url);
            request.AirportIcaoCode.Should().Be(expectedIcaoCode);
        }

        [TestCaseSource(typeof(Urls), nameof(Urls.ValidUrlsCollection))]
        public void ToString_WithValidParameters_ShouldReturnCorrectString(Uri url)
        {
            //Arrange & act
            IcaoCode expectedIcaoCode = new IcaoCode("EDDF");
            UrlDownloadRequestWithIcaoCode request = new UrlDownloadRequestWithIcaoCode(url, expectedIcaoCode);
            string toStringRepresentation = request.ToString();

            //Assert
            toStringRepresentation.Should().Contain(url.ToString()).And.Contain(expectedIcaoCode.ToString());
        }

        [Test]
        public void Constructor_WithNullParameters_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            Action act = () =>
            {
                // ReSharper disable once ObjectCreationAsStatement, because we simply need creating an instance.
                // ReSharper disable once CA1806
                new UrlDownloadRequestWithIcaoCode(
                    // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
                    null,
                    // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
                    null);
            };

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}