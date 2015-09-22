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
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIs0_SuperRateIs0()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 0,
                SuperRate = 0M,
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
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIs0AndSuperRateIs50()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 0,
                SuperRate = 0.50M,
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
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIsIntMaxAndSuperRateIs50()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = 0.5M,
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
                Super = 89478485
            };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePayslip_ShouldReturnPayslip_WhenAnnualSalaryIsIntMaxAndSuperRateIs0()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = 0M,
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
                Super = 0
            };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(-0.1)]
        [TestCase(0.51)]
        public void CalculatePayslip_ShouldThrowOverflowException_WhenAnnualSalaryIsIntMaxAndSuperRateIsGreaterThan50OrLessThan0(decimal superRate)
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = superRate,
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

        [Test]
        [TestCase(0.1)]
        [TestCase(0.36)]
        [TestCase(0.49)]
        public void CalculatePayslip_ShouldNOTThrowOverflowException_WhenAnnualSalaryIsIntMaxAndSuperRateIsGreaterThan50OrLessThan0(decimal superRate)
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = int.MaxValue,
                SuperRate = superRate,
                PaymentStartDate = "01 March – 31 March",
            };

            // Act
            Action action = () =>
            {
                this.employeeIncomeService.CalculatePayslip(employeeInfo);
            };

            // Assert
            action.ShouldNotThrow<OverflowException>();
        }

        [Test]
        public void CalculatePayslip_ShouldThrowOverflowException_WhenAnnualSalaryIsNegative()
        {
            // Arrange
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = -1,
                SuperRate = 0,
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