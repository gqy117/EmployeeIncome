namespace EmployeeIncome
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CsvHelper.Configuration;
    using Model;

    public sealed class PayslipMap : CsvClassMap<Payslip>
    {
        public PayslipMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.PayPeriod).Index(1);
            Map(m => m.GrossIncome).Index(2);
            Map(m => m.IncomeTax).Index(3);
            Map(m => m.NetIncome).Index(4);
            Map(m => m.Super).Index(5);
        }
    }
}