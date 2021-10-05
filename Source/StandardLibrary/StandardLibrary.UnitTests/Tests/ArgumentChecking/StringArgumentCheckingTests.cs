// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.ArgumentChecking;
using TestingHelper.Strings;

#endregion

namespace StandardLibrary.UnitTests.Tests.ArgumentChecking
{
    [Category("Unit")]
    [TestFixture]
    public class StringArgumentCheckingTests
    {
        [TestCaseSource(typeof(Strings), nameof(Strings.InvalidStrings))]
        public void IsStringValid_WithInvalidStrings_ShouldWork(string toCheck)
        {
            //Arrange & act
            bool result = StringArgumentChecking.IsStringValid(toCheck);

            //Assert
            result.Should().BeFalse();
        }

        [TestCaseSource(typeof(Strings), nameof(Strings.ValidStrings))]
        public void IsStringValid_WithValidStrings_ShouldWork(string toCheck)
        {
            //Arrange & act
            bool result = StringArgumentChecking.IsStringValid(toCheck);

            //Assert
            result.Should().BeTrue();
        }

        [TestCaseSource(typeof(Strings), nameof(Strings.CollectionsWithValidStrings))]
        public void ThrowExceptionIfContainsInvalidString_WithValidStrings_ShouldNotThrow_ArgumentException(
            IEnumerable<string> toCheck)
        {
            //Arrange
            Action act = () =>
                StringArgumentChecking.ThrowExceptionIfNullOrContainsInvalidString(toCheck, nameof(toCheck));

            //Act & assert
            act.Should().NotThrow();
        }

        [TestCaseSource(typeof(Strings), nameof(Strings.CollectionsWithMixedValidAndInvalidStrings))]
        public void
            ThrowExceptionIfNullOrContainsInvalidString_WithMixedInvalidAndValidStrings_ShouldThrow_ArgumentException(
                IEnumerable<string> toCheck)
        {
            //Arrange
            Action act = () =>
                StringArgumentChecking.ThrowExceptionIfNullOrContainsInvalidString(toCheck, nameof(toCheck));

            //Act & assert
            act.Should().ThrowExactly<ArgumentException>().Where(e => e.Message.Contains(nameof(toCheck)));
        }

        [TestCase(null)]
        public void ThrowExceptionIfNullOrContainsInvalidString_WithNullArgument_ShouldThrow_ArgumentNullException(
            IEnumerable<string> toCheck)
        {
            //Arrange
            Action act = () =>
                StringArgumentChecking.ThrowExceptionIfNullOrContainsInvalidString(toCheck, nameof(toCheck));

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>().Where(e => e.Message.Contains(nameof(toCheck)));
        }

        [TestCaseSource(typeof(Strings), nameof(Strings.InvalidStrings))]
        public void ThrowExceptionIfStringIsInvalid_WithInvalidStrings_ShouldThrow_ArgumentException(string toCheck)
        {
            //Arrange
            Action act = () => StringArgumentChecking.ThrowExceptionIfStringIsInvalid(toCheck, nameof(toCheck));

            //Act & assert
            act.Should().ThrowExactly<ArgumentException>().Where(e => e.Message.Contains(nameof(toCheck)));
        }

        [TestCaseSource(typeof(Strings), nameof(Strings.ValidStrings))]
        public void ThrowExceptionIfStringIsInvalid_WithValidStrings_ShouldNotThrow_ArgumentException(string toCheck)
        {
            //Arrange
            Action act = () => StringArgumentChecking.ThrowExceptionIfStringIsInvalid(toCheck, nameof(toCheck));

            //Act & assert
            act.Should().NotThrow();
        }
    }
}