namespace EmployeeIncome.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoundService
    {
        public int Round(double input)
        {
            int res = (int)Math.Round(input, MidpointRounding.AwayFromZero);

            return res;
        }
    }
}