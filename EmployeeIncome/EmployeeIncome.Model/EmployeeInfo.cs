namespace EmployeeIncome.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmployeeInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AnnualSalary { get; set; }

        public decimal SuperRate { get; set; }

        public string PaymentStartDate { get; set; }
    }
}