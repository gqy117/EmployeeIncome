namespace EmployeeIncome.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Payslip
    {
        public string Name { get; set; }

        public string PayPeriod { get; set; }

        public int GrossIncome { get; set; }

        public int IncomeTax { get; set; }

        public int NetIncome { get; set; }

        public int Super { get; set; }
    }
}