namespace EmployeeIncome.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaxRule
    {
        public int BaseIncome { get; set; }

        public int BaseTax { get; set; }

        public decimal TaxRate { get; set; }

        public int AnnualSalaryFrom { get; set; }

        public int AnnualSalaryTo { get; set; }
    }
}