namespace EmployeeIncome.Service
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    public class TaxRuleService
    {
        private static readonly IList<TaxRule> TaxRules = new List<TaxRule>()
            {
                new TaxRule 
                {
                    AnnualSalaryFrom = 0,
                    AnnualSalaryTo = 18200,
                    TaxRate = 0M,
                    BaseIncome = 0,
                    BaseTax = 0
                },
                new TaxRule 
                {
                    AnnualSalaryFrom = 18201,
                    AnnualSalaryTo = 37000,
                    TaxRate = 0.19M,
                    BaseIncome = 18200,
                    BaseTax = 0
                },
                new TaxRule 
                {
                    AnnualSalaryFrom = 37001,
                    AnnualSalaryTo = 80000,
                    TaxRate = 0.325M,
                    BaseIncome = 37000,
                    BaseTax = 3572
                },
                new TaxRule 
                {
                    AnnualSalaryFrom = 80001,
                    AnnualSalaryTo = 180000,
                    TaxRate = 0.37M,
                    BaseIncome = 80000,
                    BaseTax = 17547
                },
                new TaxRule 
                {
                    AnnualSalaryFrom = 180001,
                    AnnualSalaryTo = int.MaxValue,
                    TaxRate = 0.45M,
                    BaseIncome = 180000,
                    BaseTax = 54547
                },
            };

        private RoundService roundService;

        public TaxRuleService(RoundService roundService)
        {
            this.roundService = roundService;
        }

        public int CalculateTaxRule(int annualSalary)
        {
            TaxRule taxRule = TaxRules.Single(x => x.AnnualSalaryFrom <= annualSalary
                && annualSalary <= x.AnnualSalaryTo);

            var tax = (taxRule.BaseTax + ((annualSalary - taxRule.BaseIncome) * taxRule.TaxRate)) / EmployeeIncomeService.CountOfMonth;

            var res = this.roundService.Round((double)tax);

            return res;
        }
    }
}