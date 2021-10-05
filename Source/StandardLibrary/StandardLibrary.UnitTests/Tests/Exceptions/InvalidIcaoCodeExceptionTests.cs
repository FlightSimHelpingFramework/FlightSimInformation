// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Exceptions;
using TestingHelper.Strings;

#endregion

namespace StandardLibrary.UnitTests.Tests.Exceptions
{
    [Category("Unit")]
    [TestFixture]
    public class InvalidIcaoCodeExceptionTests
    {
        [TestCaseSource(typeof(Strings), nameof(Strings.ValidStrings))]
        public void Constructor_WithValidMessage_ShouldConstruct(string message)
        {
            //Arrange & act
            InvalidIcaoCodeException ex = new InvalidIcaoCodeException(message);

            //Assert
            ex.Message.Should().Be(message);
        }

        [TestCaseSource(typeof(Strings), nameof(Strings.ValidStrings))]
        public void
            Constructor_WithValidMessageAndNullInnerException_ShouldConstructWithDefaultMessage(
                string message)
        {
            //Arrange & act
            InvalidIcaoCodeException ex =
                // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
                new InvalidIcaoCodeException(message, null);

            //Assert
            ex.InnerException.Should().BeNull();
        }

        [Test]
        public void Constructor_WithNullMessage_ShouldConstructWithDefaultMessage()
        {
            //Arrange & act
            // ReSharper disable once AssignNullToNotNullAttribute, because it is a test.
            InvalidIcaoCodeException ex = new InvalidIcaoCodeException(null);

            //Assert
            ex.Message.Should().NotBeNull();
        }

        [Test]
        public void
            Constructor_WithValidMessageAndInnerException_ShouldConstructWithDefaultMessage()
        {
            //Arrange & act
            InvalidIcaoCodeException ex =
                new InvalidIcaoCodeException("Message", new InvalidIcaoCodeException(nameof(ex)));

            //Assert
            ex.InnerException?.Message.Should().Be(nameof(ex));
        }

        [Test]
        public void DefaultConstructor_ShouldConstruct()
        {
            //Arrange & act
            InvalidIcaoCodeException ex = new InvalidIcaoCodeException();

            //Assert
            ex.Should().NotBeNull();
        }
    }
}