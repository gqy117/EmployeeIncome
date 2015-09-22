namespace EmployeeIncome.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class RoundServiceTest
    {
        private RoundService roundService;

        [TestFixtureSetUp]
        public void Init()
        {
            this.roundService = new RoundService();
        }

        [Test]
        public void Round_ShouldReturn3_WhenInputIs2Point5()
        {
            // Arrange
            double input = 2.5;

            // Act
            int actual = this.roundService.Round(input);

            // Assert
            int expected = 3;
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void Round_ShouldReturn2_WhenInputIs2Point4()
        {
            // Arrange
            double input = 2.4;

            // Act
            int actual = this.roundService.Round(input);

            // Assert
            int expected = 2;
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
