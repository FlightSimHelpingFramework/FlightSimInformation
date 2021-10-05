// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using FluentAssertions;
using NUnit.Framework;
using StandardLibrary.Hashing;

#endregion

namespace StandardLibrary.UnitTests.Tests.Hashing
{
    [Category("Unit")]
    [TestFixture]
    public class CustomHashTests
    {
        [Test]
        public void AddToHashNumber_ShouldGiveNewHash_SameNumberEachTime()
        {
            //Arrange & act
            int firstResult = CustomHash
                .GetInitialHashNumber()
                .AddToHashNumber("Test".GetHashCode())
                .AddToHashNumber("Test2".GetHashCode());

            int secondResult = CustomHash
                .GetInitialHashNumber()
                .AddToHashNumber("Test".GetHashCode())
                .AddToHashNumber("Test2".GetHashCode());

            //Assert
            firstResult.Should().Be(secondResult);
        }

        [Test]
        public void GetInitialHashNumber_ShouldReturnNonzeroNumber_SameNumberEachTime()
        {
            //Arrange & act
            int firstResult = CustomHash.GetInitialHashNumber();
            int secondResult = CustomHash.GetInitialHashNumber(); //-V3086

            //Assert
            firstResult.Should().NotBe(0);
            firstResult.Should().Be(secondResult);
        }
    }
}