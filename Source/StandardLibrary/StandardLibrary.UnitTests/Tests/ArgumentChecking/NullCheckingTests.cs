// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.ArgumentChecking;

#endregion

namespace StandardLibrary.UnitTests.Tests.ArgumentChecking
{
    [Category("Unit")]
    [TestFixture]
    public class NullCheckingTests
    {
        [Test]
        public void ThrowExceptionIfNull_WithGoodArguments_ShouldNotThrow_ArgumentNullException()
        {
            //Arrange
            Action act = () => NullChecking.ThrowExceptionIfNull(new object(), "Something");

            //Act & assert
            act.Should().NotThrow();
        }

        [Test]
        public void ThrowExceptionIfNull_WithNullArguments_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            Action act = () => NullChecking.ThrowExceptionIfNull(null, "Something");

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>().Where(e => e.Message.Contains("Something"));
        }

        [Test]
        public void ThrowExceptionIfNullOrContainsNull_WithGoodArguments_ShouldNotThrow_ArgumentNullException()
        {
            //Arrange
            Action act = () =>
                NullChecking.ThrowExceptionIfNullOrContainsNull(new List<object> { new object(), new object() },
                    "Something");

            //Act & assert
            act.Should().NotThrow();
        }

        [Test]
        public void ThrowExceptionIfNullOrContainsNull_WithNullArguments_ShouldNotThrow_ArgumentNullException()
        {
            //Arrange
            Action act = () =>
                NullChecking.ThrowExceptionIfNullOrContainsNull(new List<object> { new object(), null },
                    "Something interesting");

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>().Where(e => e.Message.Contains("Something interesting"));
        }
    }
}