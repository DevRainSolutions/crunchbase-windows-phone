using System;
using System.Collections.Generic;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    public class IPO
    {
        public string valuation_amount;

        public string valuation_currency_code;

        public string pub_year;

        public string pub_month;

        public string pub_day;

        public string stock_symbol;
    }

    //class for the JSON data about logos
    public class Image
    {
        public List<List<object>> available_sizes;
        public string attribution;
    }

    public class nlStructure
    {
        public string name { get; set; }
        public string permalink { get; set; }
    }

    public class Link
    {
        public string external_url;
        public string title;
    }

    //class for the JSON data about funding
    public class Funding
    {
        public string round_code;
        public double? raised_amount;
        public int? funded_year;
    }

    //class for the JSON data about the company office location
    public class OfficeLocation
    {
        public int Id { get; set; }
        public string city;
        public string state_code;
        public string country_code;
        public string description;
        public string address1;
        public string address2;
        public string zip_code;
        public double latitude;
        public double longitude;

        public string Summary1
        {
            get
            {
                return string.Format("{0} {1} {2}", this.city, this.state_code, this.country_code);
            }
        }

        public string Summary2
        {
            get
            {
                return string.Format("{0} {1}", this.address1, this.address2);
            }
        }

        public System.Device.Location.GeoCoordinate Location
        {
            get
            {
                return new System.Device.Location.GeoCoordinate(this.latitude, this.longitude);
            }
        }

    }

    public class Relationship
    {
        public bool is_past { get; set; }
        public string title { get; set; }
        public Person person { get; set; }
        //For person object:
        public Firm firm { get; set; }
        public Provider provider { get; set; }
        public string FullName
        {
            get
            {
                if (this.person != null)
                    return string.Format("{0} {1}", this.person.first_name, this.person.last_name);
                else
                    return string.Empty;
            }
        }
    }

    public class Person
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string permalink { get; set; }
    }

    public class Provider
    {
        public string name { get; set; }
        public string permalink { get; set; }
    }

    public class Firm
    {
        public string name { get; set; }
        public string permalink { get; set; }
        public string type_of_entity { get; set; }
        public Image image { get; set; }
    }

    public class Milestone
    {
        public string description { get; set; }
        public int stoned_year { get; set; }
        public int stoned_month { get; set; }
        public int stoned_day { get; set; }
        public string source_url { get; set; }
        public string source_text { get; set; }
        public string source_description { get; set; }
        public string stoneable_type { get; set; }
        public object stoned_value { get; set; }
        public object stoned_value_type { get; set; }
        public object stoned_acquirer { get; set; }
        public Stoneable stoneable { get; set; }

        public string MilestoneDate
        {
            get
            {

                try
                {
                    return new DateTime(this.stoned_year, this.stoned_month, this.stoned_day).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.stoned_month, this.stoned_year);
                }

            }
        }
    }

    public class Stoneable
    {
        public string name { get; set; }
        public string permalink { get; set; }
        //For person object:
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class Fund
    {
        public string name { get; set; }
        public string source_url { get; set; }
        public string source_description { get; set; }
        public double? raised_amount { get; set; }
        public string raised_currency_code { get; set; }
        public int funded_year { get; set; }
        public int funded_month { get; set; }
        public int funded_day { get; set; }

        public string FundDate
        {
            get
            {

                try
                {
                    return new DateTime(this.funded_year, this.funded_month, this.funded_day).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.funded_month, this.funded_year);
                }

            }
        }
    }

    public class Investment2
    {
        public FundingRound2 funding_round { get; set; }

        public int Year
        {
            get
            {
                return this.funding_round.funded_year;
            }
        }

        public int Month
        {
            get
            {
                return this.funding_round.funded_month;
            }
        }

        public int Day
        {
            get
            {
                return this.funding_round.funded_day;
            }
        }
    }

    public class FundingRound2
    {
        public string round_code { get; set; }
        public string source_url { get; set; }
        public string source_description { get; set; }
        public double? raised_amount { get; set; }
        public string raised_currency_code { get; set; }
        public int funded_year { get; set; }
        public int funded_month { get; set; }
        public int funded_day { get; set; }
        public Company2 company { get; set; }
        public string InvestmentDate
        {
            get
            {

                try
                {
                    return new DateTime(this.funded_year, this.funded_month, this.funded_day).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.funded_month, this.funded_year);
                }

            }
        }
    }

    public class Company2
    {
        public string name { get; set; }
        public string permalink { get; set; }
    }

    public class PFirm
    {
        public bool is_past { get; set; }
        public string title { get; set; }

        public Firm firm { get; set; }
        public Provider provider { get; set; }
    }

    public class PDegree
    {
        public string degree_type { get; set; }
        public string subject { get; set; }
        public string institution { get; set; }
        public int graduated_year { get; set; }
        public int graduated_month { get; set; }
        public int graduated_day { get; set; }

        public string InstitutionFull
        {
            get
            {
                return string.Format("{0}, {1}", this.institution, this.degree_type);
            }
        }

        public string GraduatedDate
        {
            get
            {
                try
                {
                    return new DateTime(this.graduated_year, this.graduated_month, this.graduated_day).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}