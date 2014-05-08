using System.Windows.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace DevRainSolutions.StartupsCentral.Core.Models
{
    //using Microsoft.Phone.Controls.Maps;

    [Table]
    public class CrunchBaseCompany
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public string Id { get; set; }

        //Person only fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthplace { get; set; }
        public int BornYear { get; set; }
        public int BornMonth { get; set; }
        public int BornDay { get; set; }
        public string AffiliationName { get; set; }
        
        public List<PersonFirm> Firms { get; set; }
        public List<PersonDegree> Degrees { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string BornDate
        {
            get
            {
                try
                {
                    return new DateTime(this.BornYear, this.BornMonth, this.BornDay).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        //************************************

        public string Name { get; set; }
        public string Permalink { get; set; }
        public string Overview { get; set; }
        public string Website { get; set; }
        public string Blog { get; set; }
        public string BlogFeedUrl { get; set; }
        public string TwitterUserName { get; set; }
        public string Category { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int EmployeesCount { get; set; }
        public int FoundedYear { get; set; }
        public int FoundedMonth { get; set; }
        public int FoundedDay { get; set; }
        public string Description { get; set; }  
        public string CrunchBaseUrl { get; set; }
        public string Namespace { get; set; }
        public string ImageUrl { get; set; }
        public List<CompanyProduct> Products { get; set; }
        
        public List<CompanyOffice> Offices { get; set; }

        public ObservableCollection<LocationInfo> MapLocations { get; set; }

        public List<CompanyPeople> People { get; set; }


        public List<PersonMilestone> Milestones { get; set; }

        public List<ServiceRelationshipFirm> Providerships { get; set; }

        public List<CompanyInvestment> Investments { get; set; }

        public List<FinOrgFund> Funds { get; set; }
        
        public CompanyIpo Ipo { get; set; }
        
        public string FoundedDate
        {
            get {

                try
                {
                    return new DateTime(this.FoundedYear, this.FoundedMonth, this.FoundedDay).ToShortDateString();
                }
                catch (Exception)
                {
                    return string.Format("{0}/{1}", this.FoundedMonth, this.FoundedYear);
                }                 
            }
        }        
    }
}
