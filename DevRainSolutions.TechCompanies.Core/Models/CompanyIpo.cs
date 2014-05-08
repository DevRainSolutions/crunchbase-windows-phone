using System;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    public class CompanyIpo
    {
        public string valuation_amount;

        public string valuation_currency_code;

        public string pub_year;

        public string pub_month;

        public string pub_day;

        public string stock_symbol;

        public bool is_filled;

        public string StockSymbol 
        { 
            get
            {
                return this.stock_symbol;
            }
        }

        public string Description
        {
            get
            {
                if (!this.is_filled) return "No information";
                
                return string.Format(
                    "{0}\n\nStock: {1}\n\n{2}{3}",
                    new DateTime(int.Parse(this.pub_year), int.Parse(this.pub_month),
                                 int.Parse(this.pub_day)),
                    this.stock_symbol,
                    this.valuation_currency_code,
                    this.valuation_amount);
            }
        }

    }
}
