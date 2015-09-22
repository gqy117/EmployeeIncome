namespace EmployeeIncome.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    public class EmployeeIncomeService
    {
        public const int CountOfMonth = 12;

        private readonly TaxRuleService taxRuleService;
        private readonly RoundService roundService;

        public EmployeeIncomeService(TaxRuleService taxRuleService, RoundService roundService)
        {
            this.taxRuleService = taxRuleService;
            this.roundService = roundService;
        }

        public Payslip CalculatePayslip(EmployeeInfo employeeInfo)
        {
            this.CheckSuperRate(employeeInfo);
            this.CheckAnnualSalary(employeeInfo);

            Payslip payslip = new Payslip();

            this.CalcName(employeeInfo, payslip);
            this.CalcPayPeriod(employeeInfo, payslip);
            this.CalcGrossIncome(employeeInfo, payslip);
            this.CalcIncomeTax(employeeInfo, payslip);
            this.CalcNetIncome(payslip);
            this.CalcSuper(employeeInfo, payslip);

            return payslip;
        }

        private void CheckAnnualSalary(EmployeeInfo employeeInfo)
        {
            if (employeeInfo.AnnualSalary < 0)
            {
                throw new OverflowException();
            }
        }

        private void CheckSuperRate(EmployeeInfo employeeInfo)
        {
            if (employeeInfo.SuperRate > 0.5M || employeeInfo.SuperRate < 0M)
            {
                throw new OverflowException();
            }
        }

        private void CalcName(EmployeeInfo employeeInfo, Payslip payslip)
        {
            payslip.Name = string.Format("{0} {1}", employeeInfo.FirstName, employeeInfo.LastName);
        }

        private void CalcPayPeriod(EmployeeInfo employeeInfo, Payslip payslip)
        {
            payslip.PayPeriod = employeeInfo.PaymentStartDate;
        }

        private void CalcGrossIncome(EmployeeInfo employeeInfo, Payslip payslip)
        {
            payslip.GrossIncome = this.roundService.Round(employeeInfo.AnnualSalary / CountOfMonth);
        }

        private void CalcIncomeTax(EmployeeInfo employeeInfo, Payslip payslip)
        {
            payslip.IncomeTax = this.taxRuleService.CalculateTaxRule(employeeInfo.AnnualSalary);
        }

        private void CalcNetIncome(Payslip payslip)
        {
            payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
        }

        private void CalcSuper(EmployeeInfo employeeInfo, Payslip payslip)
        {
            payslip.Super = this.roundService.Round((double)(payslip.GrossIncome * employeeInfo.SuperRate));
        }
    }
}