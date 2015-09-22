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
        [TestCase(0.5)]
        [TestCase(0.9)]
        public void Round_ShouldReturn1_WhenInputIsGreaterThan0Point5(double input)
        {
            // Act
            int actual = this.roundService.Round(input);

            // Assert
            int expected = 1;
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(0.4999)]
        [TestCase(0)]
        public void Round_ShouldReturn2_WhenInputIsIsLessThan0Point5(double input)
        {
            // Act
            int actual = this.roundService.Round(input);

            // Assert
            int expected = 0;
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
