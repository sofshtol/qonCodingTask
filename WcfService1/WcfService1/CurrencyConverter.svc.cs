using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    public class CurrencyConverter : IServiceConverter
    {
        /// <summary>
        /// String array holding values of -ones and -teens from 0 to 19.
        /// </summary>
        private static readonly string[] ones = {
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
        "seventeen", "eighteen", "nineteen"
        };

        /// <summary>
        /// String array holding values of -tens from 20 to 90.
        /// </summary>
        private static readonly string[] tens = {
        "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
        };

        /// <summary>
        /// Converts int input less than thousand into words.
        /// </summary>
        /// <param name="number">Number to convert (only less than a thousand).</param>
        /// <returns>Returns string with word interpretation.</returns>
        private string ConvertLessThanThousand(int number)
        {
            string word;

            if (number % 100 < 20)
            {
                word = ones[number % 100];
                number /= 100;
            }
            else
            {
                word = ones[number % 10];
                number /= 10;

                word = tens[number % 10] + " " + word;
                number /= 10;
            }
            
            if (number == 0) return word;
            return ones[number] + " hundred " + word;
        }

        /// <summary>
        /// Converts decimal currency input into words.
        /// </summary>
        /// <param name="currency">Decimal currency to convert. Should have dollar part between 0 and 999 999 999 and cent part <= 99.</param>
        /// <returns>Returns string with word interpretation.</returns>
        public string ConvertCurrencyToWords(decimal currency)
        {
            long dollars = (long)currency;
            int cents = (int)((currency - dollars) * 100);

            if (dollars < 0 || dollars > 999999999 || cents < 0 || cents > 99)
            {
                return "Please enter a valid number between 0 and 999 999 999 dollars.";
            }

            string words = ConvertWholePart(dollars);
            
            if (dollars != 1)
            {
                words += "s";
            } 

            if (cents != 0)
            {
                words += " and " + ConvertDecimalPart(cents);
                if (cents != 1) words += "s";
            }

            return words.Trim();
        }

        /// <summary>
        /// Converts the whole part of currency input.
        /// </summary>
        /// <param name="number">Whole part to convert, between 0 and 999 999 999.</param>
        /// <returns>Returns string with word interpretation.</returns>
        private string ConvertWholePart(long number)
        {
            string words = "";

            if (number >= 1000000)
            {
                words += ConvertLessThanThousand((int)(number / 1000000)) + " million ";
                number %= 1000000;
            }

            if (number >= 1000)
            {
                words += ConvertLessThanThousand((int)(number / 1000)) + " thousand ";
                number %= 1000;
            }

            if (number >= 0)
            {
                words += ConvertLessThanThousand((int)number);
            }

            words += " dollar";

            return words;
        }

        /// <summary>
        /// Converts the decimal part of currency input.
        /// </summary>
        /// <param name="number">Decimal part to convert, <= 99.</param>
        /// <returns>Returns string with word interpretation.</returns>
        private string ConvertDecimalPart(long number)
        {
            string words = "";

            words += ConvertLessThanThousand((int)number);

            words += " cent";

            return words;
        }
    }
}