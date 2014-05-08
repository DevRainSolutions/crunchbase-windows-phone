using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Net;
using System.Text;

namespace DevRainSolutions.TechCompanies.Core.Models
{
    [Table]
    public class CrunchBaseCompany
    {
        //this is the class definition to enum all of the data items to be retrieved from the
        //deserialized JSON object

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public Guid Id { get; set; }

        [Column(CanBeNull = false)]
        public string name { get; set; }

        [Column(CanBeNull = false)]
        public string permalink { get; set; }

        public string Namespace { get; set; }

        public string first_name { get; set; }
        public string last_name { get; set; }
        public string birthplace { get; set; }
        public int born_year { get; set; }
        public int born_month { get; set; }
        public int born_day { get; set; }
        public string affiliation_name { get; set; }

        public string Name
        {
            get
            {
                return Namespace == "person" ? string.Format("{0} {1}", first_name, last_name) : name;
            }
        }

        public string homepage_url { get; set; }
        public string crunchbase_url { get; set; }
        public string category_code { get; set; }
        public string description { get; set; } // = "";
        public int? number_of_employees { get; set; } // = 0;

        public string twitter_username { get; set; }

        public int? founded_year { get; set; }

        public int? founded_month { get; set; }

        public int? founded_day { get; set; }

        public string deadpooled_year;

        public string deadpooled_month;

        public string deadpooled_day;

        public string deadpooled_url;

        public string alias_list;

        public string email_address { get; set; }

        public string phone_number { get; set; }

        public string Description
        {
            get
            {
                if (this.overview != null)
                    return HttpUtility.HtmlDecode(this.overview.RemoveHtmlWithParagraphs());
                else
                    return string.Empty;
            }
        }

        public string blog_url { get; set; }

        public IPO ipo;

        public string IPO
        {
            get
            {
                return this.ipo != null
                           ? string.Format(
                               "{0} {1} {2}",
                               this.ipo.stock_symbol,
                               new DateTime(int.Parse(this.ipo.pub_year), int.Parse(this.ipo.pub_month), int.Parse(this.ipo.pub_day)).ToShortDateString(),
                               this.ipo.valuation_amount)
                           : string.Empty;
            }
        }

        public string blog_feed_url;

        public string overview { get; set; }
        public bool deadpool;
        public int? deadpool_year; //= "";
        public Image image;

        public string Founded
        {
            get
            {
                string date = (founded_year != null ? founded_year.ToString() : "") + (founded_month != null ? "." + founded_month.ToString() : "") + (founded_day != null ? "." + founded_day.ToString() : "");
                return date; //(founded_year!=0||founded_month!=0||founded_day!=0)? new DateTime(this.founded_year, this.founded_month, this.founded_day).ToShortDateString() : string.Empty;
            }
        }

        public string ImageUrl
        {
            get
            {
                return image != null && image.available_sizes.Count > 0
                           ? string.Format("{0}{1}", Constants.CrunchbaseSite, (image.available_sizes.Last()[1]))
                           : string.Empty;
            }
        }

        public string SecondLine
        {
            get
            {
                var sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(IPO))
                {
                    sb.AppendFormat("{0}", IPO);
                }

                if (!string.IsNullOrWhiteSpace(category_code))
                {
                    sb.AppendFormat(", {0}", category_code);
                }

                if (!string.IsNullOrWhiteSpace(Founded))
                {
                    sb.AppendFormat(", {0}", Founded);
                }

                return sb.ToString();
            }
        }

        public ObservableCollection<OfficeLocation> offices { get; set; }
        public System.Device.Location.GeoCoordinate MapCenter { get; set; }

        public string tag_list;

        public ObservableCollection<nlStructure> products { get; set; }
        public List<Relationship> relationships { get; set; }
        public List<Milestone> milestones { get; set; }
        public List<Fund> funds { get; set; }
        public List<Investment2> investments { get; set; }
        public List<PFirm> providerships { get; set; }
        public List<PDegree> degrees { get; set; }


        public List<Funding> funding_rounds;
        public List<Link> external_links;
        public Dictionary<string, Funding> aggFundStructure = new Dictionary<string, Funding>();
        public List<string> keyword_tags;

        //helper function to generate the URL for the JSON object on a per company basis
        public void FixCrunchBaseURL()
        {
            string urlBase = "http://www.crunchbase.com/company/";


            this.crunchbase_url = urlBase + this.permalink;
        }

        //helper function to generate the URL for the image to be pulled from the crunchbase API
        public string GetImageURL()
        {
            var baseURL = "http://www.crunchbase.com/";
            if (this.image != null)
            {
                return baseURL + this.image.available_sizes[1][1];
            }

            return "none";
        }

        //simple function which pulls all the funding data for each company and aggregates the funding across
        //all rounds - the data is dumped into a dictionary that has all funding for each round
        public void AggregateFunding()
        {
            foreach (Funding fundingRound in this.funding_rounds)
            {
                if (this.aggFundStructure.ContainsKey(fundingRound.round_code))
                {
                    if (fundingRound.raised_amount == null)
                    {
                        fundingRound.raised_amount = 0;
                    }

                    this.aggFundStructure[fundingRound.round_code].raised_amount += fundingRound.raised_amount;


                    if (this.aggFundStructure[fundingRound.round_code].funded_year < fundingRound.funded_year)
                    {
                        this.aggFundStructure[fundingRound.round_code].funded_year = fundingRound.funded_year;
                    }
                }
                else
                {
                    if (fundingRound.raised_amount == null)
                    {
                        fundingRound.raised_amount = 0;
                    }

                    if (fundingRound.funded_year == null)
                    {
                        //use -1 to denote that the year returned was null
                        fundingRound.funded_year = -1;
                    }

                    this.aggFundStructure.Add(fundingRound.round_code, fundingRound);
                }
            }
        }

        //takes the dictionary and iterates through it to get the total funding.  Using the dictionary allows
        //for a pivot on funding round later should I choose
        public double GetAggregateFunding()
        {
            return this.funding_rounds.Sum(fundingRound => (double)fundingRound.raised_amount);
        }

        //helper function to parse the keyword-tag string
        public void SplitTagString()
        {
            if (this.tag_list != null)
            {
                string[] keywordTags = this.tag_list.Split(',');

                foreach (string tag in keywordTags)
                {
                    //make sure those pesky trailing and leading spaces are gone
                    tag.Trim();
                }

                this.keyword_tags = keywordTags.ToList();
            }
        }

        //the number of keywords can be [0,n), which makes for challenges with the Pivot data creation.  To simplify
        //this, I simply take 5 keywords if there are 5, and if not, return \t in place of a word
        //the special case is a null, and to avoid a bug in the Pivot CXML, return "none" for the first word, and 4x\t
        public string GetKeywordList()
        {
            string tmpString = "";
            if (this.keyword_tags != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i < this.keyword_tags.Count())
                    {
                        tmpString += this.keyword_tags[i].ToString() + "\t";
                    }
                    else
                    {
                        tmpString += " " + "\t";
                    }
                }
            }
            else
            {
                tmpString = "none\t\t\t\t\t";
            }

            return tmpString;
        }
    }


}

