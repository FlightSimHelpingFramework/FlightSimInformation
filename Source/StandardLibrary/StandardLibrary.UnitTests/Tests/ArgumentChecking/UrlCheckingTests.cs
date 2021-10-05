// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.ArgumentChecking;
using TestingHelper.Downloader;

#endregion

namespace StandardLibrary.UnitTests.Tests.ArgumentChecking
{
    [Category("Unit")]
    [TestFixture]
    public class UrlCheckingTests
    {
        [TestCaseSource(typeof(Urls), nameof(Urls.InvalidStringUrlsCollection))]
        public void IsUrlValid_WithInvalidUrls_ShouldWork(string urlToCheck)
        {
            //Arrange & act
            bool result = UrlChecking.IsUrlValid(urlToCheck);

            //Assert
            result.Should().BeFalse();
        }

        [TestCaseSource(typeof(Urls), nameof(Urls.ValidStringUrlsCollection))]
        public void IsUrlValid_WithValidUrls_ShouldWork(string urlToCheck)
        {
            //Arrange & act
            bool result = UrlChecking.IsUrlValid(urlToCheck);

            //Assert
            result.Should().BeTrue();
        }
    }
}