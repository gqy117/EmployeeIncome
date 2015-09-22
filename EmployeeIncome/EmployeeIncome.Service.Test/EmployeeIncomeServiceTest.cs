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
    public class EmployeeIncomeServiceTest
    {
        private EmployeeIncomeService employeeIncomeService;

        [TestFixtureSetUp]
        public void Init()
        {
            RoundService roundService = new RoundService();
            TaxRuleService taxRuleService = new TaxRuleService(roundService);
            this.employeeIncomeService = new EmployeeIncomeService(taxRuleService, roundService);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 60050,
                SuperRate = 0.09M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Payslip actual = this.employeeIncomeService.CalculatePayslip(employeeInfo);

            // Assert
            Payslip expected = new Payslip
            {
                Name = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 5004,
                IncomeTax = 922,
                NetIncome = 4082,
                Super = 450
            };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIs0()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 0,
                SuperRate = 0.09M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Payslip actual = this.employeeIncomeService.CalculatePayslip(employeeInfo);

            // Assert
            Payslip expected = new Payslip
            {
                Name = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 0,
                IncomeTax = 0,
                NetIncome = 0,
                Super = 0
            };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIsIntMax()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = 0.09M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Payslip actual = this.employeeIncomeService.CalculatePayslip(employeeInfo);

            // Assert
            Payslip expected = new Payslip
            {
                Name = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 178956970,
                IncomeTax = 80528432,
                NetIncome = 98428538,
                Super = 16106127
            };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIsIntMaxAndSuperRateIs1()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = 1M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Payslip actual = this.employeeIncomeService.CalculatePayslip(employeeInfo);

            // Assert
            Payslip expected = new Payslip
            {
                Name = "David Rudd",
                PayPeriod = "01 March – 31 March",
                GrossIncome = 178956970,
                IncomeTax = 80528432,
                NetIncome = 98428538,
                Super = 178956970
            };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePayslip_ShouldThrowOverflowException_WhenAnnualSalaryIsIntMaxAndSuperRateIsGreaterThan1()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = 1.1M,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Action action = () =>
            {
                this.employeeIncomeService.CalculatePayslip(employeeInfo);
            };

            // Assert
            action.ShouldThrow<OverflowException>();
        }
    }
}