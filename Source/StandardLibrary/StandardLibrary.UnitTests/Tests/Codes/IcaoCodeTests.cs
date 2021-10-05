// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Codes;
using StandardLibrary.Exceptions;
using TestingHelper.IcaoCodes;

#endregion

namespace StandardLibrary.UnitTests.Tests.Codes
{
    [Category("Unit")]
    [TestFixture]
    public class IcaoCodeTests
    {
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.InvalidStringIcaoCodesCollection))]
        public void Constructor_WithInvalidParameters_ShouldThrow_InvalidIcaoCodeException(string icaoCodeString)
        {
            //Arrange
            // ReSharper disable once ObjectCreationAsStatement, because we simply need creating an instance.
            Action act = () => { new IcaoCode(icaoCodeString); };

            //Act & assert
            act.Should().ThrowExactly<InvalidIcaoCodeException>();
        }

        [Theory]
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void Constructor_WithValidParameters_ShouldConstruct(string icaoCodeString)
        {
            //Arrange
            IcaoCode icaoCode = new IcaoCode(icaoCodeString);

            //Act & assert
            icaoCode.Code.Should().Be(icaoCodeString);
        }

        [Theory]
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void Equals_WithEqualObjects_ShouldWorkAsExpected(string icaoCodeString)
        {
            //Arrange & act
            IcaoCode icaoCode1 = new IcaoCode(icaoCodeString);
            IcaoCode icaoCode2 = new IcaoCode(icaoCodeString);

            //Assert
            icaoCode1.Should().Be(icaoCode2);
        }

        [Theory]
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void Equals_WithOneNullObject_ShouldWorkAsExpected(string icaoCodeString)
        {
            //Arrange & act
            IcaoCode icaoCode1 = new IcaoCode(icaoCodeString);
            IcaoCode icaoCode2 = null;
            // ReSharper disable once ExpressionIsAlwaysNull, because it is a test.
            bool result = icaoCode1.Equals(icaoCode2);

            //Assert
            result.Should().BeFalse();
        }

        [Theory]
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void GetHashCode_WithEqualObjects_ShouldWorkAsExpected(string icaoCodeString)
        {
            //Act & assert
            IcaoCode icaoCode1 = new IcaoCode(icaoCodeString);
            IcaoCode icaoCode2 = new IcaoCode(icaoCodeString);

            int icaoCode1HashCode = icaoCode1.GetHashCode();
            int icaoCode2HashCode = icaoCode2.GetHashCode();

            //Assert
            icaoCode1HashCode.Should().Be(icaoCode2HashCode);
        }

        [Theory]
        [TestCaseSource(typeof(IcaoCodes), nameof(IcaoCodes.ValidStringIcaoCodesCollection))]
        public void ToString_ShouldWorkAsExpected(string icaoCodeString)
        {
            //Arrange & act
            IcaoCode icaoCode1 = new IcaoCode(icaoCodeString);

            //Assert
            icaoCode1.ToString().Should().Contain(icaoCodeString);
        }
    }
}