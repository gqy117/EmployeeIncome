namespace EmployeeIncome
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Model;
    using Service;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            RoundService roundService = new RoundService();
            TaxRuleService taxRuleService = new TaxRuleService(roundService);

            EmployeeIncomeService employeeIncomeService = new EmployeeIncomeService(taxRuleService, roundService);

            var employeeList = GetEmployeeInfos();

            OutputToFile(employeeList, employeeIncomeService);
        }

        private static void OutputToFile(IList<EmployeeInfo> employeeList, EmployeeIncomeService employeeIncomeService)
        {
            using (StreamWriter file = new StreamWriter(@"output.csv"))
            {
                IList<Payslip> payslipList = new List<Payslip>();

                foreach (var employeeInfo in employeeList)
                {
                    var payslip = employeeIncomeService.CalculatePayslip(employeeInfo);

                    payslipList.Add(payslip);
                }

                var csvWriter = new CsvWriter(file);
                csvWriter.Configuration.RegisterClassMap<PayslipMap>();
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.WriteRecords(payslipList);
            }
        }

        private static IList<EmployeeInfo> GetEmployeeInfos()
        {
            IList<EmployeeInfo> employeeList = new List<EmployeeInfo>();

            using (StreamReader sr = new StreamReader("input.csv"))
            {
                var csvReader = new CsvReader(sr);

                csvReader.Configuration.RegisterClassMap<EmployeeInfoMap>();
                csvReader.Configuration.HasHeaderRecord = false;

                employeeList = csvReader.GetRecords<EmployeeInfo>().ToList();
            }

            return employeeList;
        }
    }
}