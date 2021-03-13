using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ServiceModelClass
{
    public class AutoGenerateNumber
    {
        //product code generate
        public string GenAutoKeyNumber()
        {
            Random random = new Random();
            string first_digit = random.Next(1,10).ToString();
            string second_digit = random.Next(1,10).ToString();
            string third_digit = random.Next(1,10).ToString();
            string fourth_digit = random.Next(1,10).ToString();
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string finalGenNum = month + "" + day + "" + first_digit + "" + second_digit + "" + third_digit + "" + fourth_digit;
            return finalGenNum;
        }

        //generate order status code
        public string GenStatusCode()
        {
            Random random = new Random();
            string first_digit = random.Next(1,10).ToString();
            string second_digit = random.Next(1, 10).ToString();
            string day = DateTime.Now.ToString("dd");
            string finalGenNum = day + "" + first_digit + "" + second_digit;
            return finalGenNum;
        }

        //generate order item status code
        public string GenItemStatusCode()
        {
            Random random = new Random();
            string first_digit = random.Next(1, 10).ToString();
            string second_digit = random.Next(1, 10).ToString();
            string day = DateTime.Now.ToString("dd");
            string finalGenNum = day + "" + first_digit + "" + second_digit;
            return finalGenNum;
        }

        //generate invoice status code
        public string GenInvoiceStatusCode()
        {
            Random random = new Random();
            string first_digit = random.Next(1, 10).ToString();
            string second_digit = random.Next(1, 10).ToString();
            string day = DateTime.Now.ToString("dd");
            string finalGenNum = day + "" + first_digit + "" + second_digit;
            return finalGenNum;
        }

        //generate invoice status code
        public string GenInvoiceNumber()
        {
            Random random = new Random();
            string first_digit = random.Next(1, 10).ToString();
            string second_digit = random.Next(1, 10).ToString();
            string third_digit = random.Next(1, 10).ToString();
            string fourth_digit = random.Next(1, 10).ToString();
            string finalGenNum = first_digit + "" + second_digit + "" + third_digit + "" + fourth_digit;
            return finalGenNum;
        }

        // generate shipment track number
        public string GenShipmentTrackNum()
        {
            Random random = new Random();
            string first_digit = random.Next(1, 10).ToString();
            string second_digit = random.Next(1, 10).ToString();
            string third_digit = random.Next(1, 10).ToString();
            string fourth_digit = random.Next(1, 10).ToString();
            string fifth_digit = random.Next(1, 10).ToString();
            string sixth_digit = random.Next(1, 10).ToString();
            string seventh_digit = random.Next(1, 10).ToString();
            string eighth_digit = random.Next(1, 10).ToString();
            string finalGenNum = first_digit + "" + second_digit + "" + third_digit + "" + fourth_digit + "" + fifth_digit + "" + sixth_digit + "" + seventh_digit + "" + eighth_digit;
            return finalGenNum;
        }
    }
}
