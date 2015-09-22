1. About Visual Studio

This solution is built via VS2013 Update 5, I am not sure if you want to run/debug all the test cases. Personally, I use [Resharper], so that I can run the test cases easily. If you don't have [Resharper] and you still want to run the test cases, maybe you need to install some plugin for NUnit.

2. Solution Architecture

This is a c# console application. For demo propose, I didn't build a web application.

It contains the following folders/projects:

(1) Demo

This folder is for demo. Run "EmployeeIncome.exe" to see the result if you don't install Visual Studio.

(2) EmployeeIncome

The console application project, I added [CsvHelper] library, since I want to use CSV as input and output format, but it's not necessary. That's why I didn't add test cases for this project, because the bussiness logic should not be here.

(3) EmployeeIncome.Service

Business logic is in this project. It contains 3 service classes:

(a) EmployeeIncomeService

To calculate the Payslip.

(b) TaxRuleService

I saw "Taxable income" is quite complex, so I decided to create a seperated class, so that I can test all the edge cases.

(c) RoundService

According to the statement "If >= 50 cents round up to the next dollar increment, otherwise round down.", I created a class to test the corner cases.

(4) EmployeeIncome.Model

An independent project to keep all the models.

(5) EmployeeIncome.Service.Test

Since there are 3 service class, I created 3 test classes for them:

(a) EmployeeIncomeServiceTest

One happy pass case, a few cases to cover all the edge cases, as it says: "annual salary(positive integer) and super rate(0% - 50% inclusive)".

So there should be 4 cases:

*annual salary: 0, super rate: 0%

*annual salary: 0, super rate: 50%

*annual salary: int.Max, super rate: 0%

*annual salary: int.Max, super rate: 50%

Also if the values are out of range, throw an expcetion.

(b) TaxRuleServiceTest

Test all the edge cases according to the "Taxable income" rule.

(c) RoundServiceTest

Test the logic "If >= 50 cents round up to the next dollar increment, otherwise round down."
