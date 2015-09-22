namespace EmployeeIncome
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CsvHelper.Configuration;
    using Model;

    public sealed class EmployeeInfoMap : CsvClassMap<EmployeeInfo>
    {
        public EmployeeInfoMap()
        {
            Map(m => m.FirstName).Index(0);
            Map(m => m.LastName).Index(1);
            Map(m => m.AnnualSalary).Index(2);
            Map(m => m.SuperRate).ConvertUsing(row => decimal.Parse(row.GetField<string>(3).TrimEnd(new char[] { '%', ' ' })) / 100M);
            Map(m => m.PaymentStartDate).Index(4);
        }
    }
}