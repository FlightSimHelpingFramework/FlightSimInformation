// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Exceptions;
using TestingHelper.IcaoCodes;

#endregion

namespace StandardLibrary.UnitTests.Tests.ArgumentChecking
{
    [Category("Unit")]
    [TestFixture]
    public class IcaoCodeCheckingTests
    {
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void IsIcaoCodeStringValid_WithValidCodes_ShouldWork(string icaoCode)
        {
            //Arrange & act
            bool result = IcaoCodeChecking.IsIcaoCodeStringValid(icaoCode);

            //Assert
            result.Should().BeTrue();
        }

        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.InvalidStringIcaoCodesCollection))]
        public void IsIcaoCodeStringValid_WithInvalidCodes_ShouldWork(string icaoCode)
        {
            //Arrange & act
            bool result = IcaoCodeChecking.IsIcaoCodeStringValid(icaoCode);

            //Assert
            result.Should().BeFalse();
        }

        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.InvalidStringIcaoCodesCollection))]
        public void ThrowExceptionIfIcaoCodeIsInvalid_WithInvalidCodes_ShouldThrow_InvalidIcaoCodeException(
            string icaoCode)
        {
            //Arrange
            Action act = () => IcaoCodeChecking.ThrowExceptionIfIcaoCodeIsInvalid(icaoCode);

            //Act & assert
            act.Should().ThrowExactly<InvalidIcaoCodeException>();
        }

        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void ThrowExceptionIfIcaoCodeIsInvalid_WithValidCodes_ShouldNotThrow_InvalidIcaoCodeException(
            string icaoCode)
        {
            //Arrange
            Action act = () => IcaoCodeChecking.ThrowExceptionIfIcaoCodeIsInvalid(icaoCode);

            //Act & assert
            act.Should().NotThrow();
        }

        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.CollectionsWithMixedValidAndInvalidStringIcaoCodes))]
        public void
            ThrowExceptionIfContainsInvalidIcaoCode_WithMixedValidAndInvalidCodes_ShouldThrow_InvalidIcaoCodeException(
                IEnumerable<string> icaoCodes)
        {
            //Arrange
            Action act = () => IcaoCodeChecking.ThrowExceptionIfContainsInvalidIcaoCode(icaoCodes);

            //Act & assert
            act.Should().ThrowExactly<InvalidIcaoCodeException>();
        }

        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.CollectionsWithValidStringIcaoCodes))]
        public void ThrowExceptionIfContainsInvalidIcaoCode_WithValidCodes_ShouldNotThrow_InvalidIcaoCodeException(
            IEnumerable<string> icaoCodes)
        {
            //Arrange
            Action act = () => IcaoCodeChecking.ThrowExceptionIfContainsInvalidIcaoCode(icaoCodes);

            //Act & assert
            act.Should().NotThrow();
        }

        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.CollectionsWithInvalidStringIcaoCodes))]
        public void
            ThrowExceptionIfContainsInvalidIcaoCode_WithInvalidCodes_ShouldThrow_InvalidIcaoCodeException(
                IEnumerable<string> icaoCodes)
        {
            //Arrange
            Action act = () => IcaoCodeChecking.ThrowExceptionIfContainsInvalidIcaoCode(icaoCodes);

            //Act & assert
            act.Should().ThrowExactly<InvalidIcaoCodeException>();
        }

        [Test]
        public void
            ThrowExceptionIfContainsInvalidIcaoCode_WithNullCollection_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            // ReSharper disable once AssignNullToNotNullAttribute, because it is a test case
            Action act = () => IcaoCodeChecking.ThrowExceptionIfContainsInvalidIcaoCode(null);

            //Act & assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}