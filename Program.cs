using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAmortizationSchedule
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#############Amortization Schedule Calculator##############");
            Console.WriteLine("Please enter amount to collect");
            string principal = Console.ReadLine ();
            Console.WriteLine("Your annual rate is 6%");
            Console.WriteLine("And you are to make payment within; how many years?");
            string noOfYears = Console.ReadLine();
            var amortPeriod = GetAmortizationPeriodTotal (int.Parse(noOfYears));
            Console.WriteLine($"Amortization Period Total: {amortPeriod}");
            var monthlyRate = GetMonthlyRate(6.0);
            Console.WriteLine($"Monthly Rate: {monthlyRate}");
            var numerator = GetNumerator (amortPeriod, monthlyRate);
            Console.WriteLine($"Numerator: {numerator}");

            var denominator = GetDenominator (amortPeriod, monthlyRate);
            Console.WriteLine($"Denominator: {denominator}");

            var mortgagePayment = GetMortgagePayment (int.Parse (principal), numerator, denominator);
            Console.WriteLine($"Mortgage Payment: {mortgagePayment.ToString("0.00")}");

            var principalBalance = decimal.Parse(principal);
            decimal interestPayment = 0;
            decimal principalPayment = 0;

            int count = 0;
            decimal interestAccrued = 0;

            Console.WriteLine($"S/N\tInstallment\tPrincipal Payment\tInterest Payment\tInterest Accrued\tPrincipal Balance");
            

            while (count <= amortPeriod)
            {
                Console.WriteLine($"{count}\t{mortgagePayment.ToString ("0.00")}\t{principalPayment.ToString("0.00")}\t{interestPayment.ToString("0.00")}\t{interestAccrued.ToString("0.00")}\t{principalBalance.ToString("0.00")}");

                interestPayment = principalBalance * (decimal)(monthlyRate / 100);
                interestAccrued += interestPayment;
                principalPayment = mortgagePayment - interestPayment;
                principalBalance -= principalPayment;
                

                count++;
            }

            Console.ReadLine();
        }

        public static int GetAmortizationPeriodTotal (int NoOfYears, int FrequencyOfPayments = 12)
        {
            return NoOfYears * FrequencyOfPayments; 
        }

        public static double GetMonthlyRate (double AnnualRate, double FrequencyOfPayments = 12.0)
        {
            return (double)(AnnualRate / FrequencyOfPayments);
        }

        public static decimal GetNumerator (decimal AmortPeriodTotal, double MonthlyRate)
        {
            return (decimal) (Math.Pow((MonthlyRate / 100) + 1, (double)AmortPeriodTotal) * (MonthlyRate / 100));
        }

        public static decimal GetDenominator (double AmortPeriodTotal, double MonthlyRate)
        {
            return (decimal) (Math.Pow ((MonthlyRate / 100) + 1, AmortPeriodTotal) - 1);
        }

        public static decimal GetMortgagePayment (int principal, decimal numerator, decimal denominator)
        {
            return (decimal) (principal * (numerator / denominator));
        }
    }
}
 