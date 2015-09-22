namespace EmployeeIncome.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class TaxRuleServiceTest
    {
        private TaxRuleService taxRuleService;

        [TestFixtureSetUp]
        public void Init()
        {
            RoundService roundService = new RoundService();
            this.taxRuleService = new TaxRuleService(roundService);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10000)]
        [TestCase(18200)]
        public void CalculateTaxRule_ShouldReturn0_WhenIncomeIsLessThan18201(int annualSalary)
        {
            // Act
            int actual = this.taxRuleService.CalculateTaxRule(annualSalary);

            // Assert
            int expected = 0;

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(18201, 0)]
        [TestCase(25000, 108)]
        [TestCase(37000, 298)]
        public void CalculateTaxRule_ShouldReturn0PlusTaxRate_WhenIncomeIsBetween18201And37000(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRuleService.CalculateTaxRule(annualSalary);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(37001, 298)]
        [TestCase(60000, 921)]
        [TestCase(80000, 1462)]
        public void CalculateTaxRule_ShouldReturn3572PlusTaxRate_WhenIncomeIsBetween37001And80000(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRuleService.CalculateTaxRule(annualSalary);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(80001, 1462)]
        [TestCase(120000, 2696)]
        [TestCase(180000, 4546)]
        public void CalculateTaxRule_ShouldReturn17547PlusTaxRate_WhenIncomeIsBetween80001And180000(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRuleService.CalculateTaxRule(annualSalary);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(180001, 4546)]
        [TestCase(330000, 10171)]
        [TestCase(int.MaxValue, 80528432)]
        public void CalculateTaxRule_ShouldReturn54547PlusTaxRate_WhenIncomeIsBetween180001AndIntMax(int annualSalary, int expected)
        {
            // Act
            int actual = this.taxRuleService.CalculateTaxRule(annualSalary);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
