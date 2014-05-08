using System.Device.Location;
using DevRainSolutions.TechCompanies.Core;
using DevRainSolutions.TechCompanies.Core.Models;
using DevRainSolutions.TechCompanies.ViewModel;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace DevRainSolutions.TechCompanies.Pages
{
    public partial class CompanyDetails
    {
        private MainViewModel context;

        // Constructor
        public CompanyDetails()
        {
            this.InitializeComponent();

            context = this.DataContext as MainViewModel;
            
            ArrangePivots();
            jlProducts.GroupDescriptors.Add(new GenericGroupDescriptor<nlStructure, string>(i => i.name.Substring(0, 1).ToUpper()));
        }

        public void ArrangePivots()
        {
            mainPivot.Items.Clear();
            mainPivot.Items.Add(piInfo);            
            switch (context.CurrentSearchItem.@namespace)
            {
                case "company":
                    ShowPivotItemsForCompany();
                    break;
                case "product":
                    ShowPivotItemsForProduct();
                    break;
                case "person":
                    ShowPivotItemsForPerson();
                    break;
                case "service-provider":
                    ShowPivotItemsForServiceProvider();
                    break;
                case "financial-organization":
                    ShowPivotItemsForFinansicalOrganisation();
                    break;
            }
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                MainMap.Center = context.SelectedObject.MapCenter;
            });
        }

        void ShowPivotItemsForCompany()
        {
            AddPI(piProducts);
            AddPI(piOffices);
            AddPI(piPeople);
            AddPI(piMilestones);
        }

        void ShowPivotItemsForProduct()
        {
            AddPI(piMilestones);
        }

        void ShowPivotItemsForPerson()
        {
            AddPI(piCompanies);
            AddPI(piDegrees);
            AddPI(piMilestones);
        }

        void ShowPivotItemsForServiceProvider()
        {
            AddPI(piOffices);
            AddPI(piServiceProviderships);
            AddPI(piMilestones);
        }

        void ShowPivotItemsForFinansicalOrganisation()
        {
            AddPI(piOffices);
            AddPI(piPeople);
            AddPI(piInvestments);
            AddPI(piFunds);
            AddPI(piFinOrgProviderships);
            AddPI(piMilestones);
        }

        void AddPI(PivotItem item)
        {
            mainPivot.Items.Add(item);
        }


        private void lbOffices_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var coord = new GeoCoordinate();
            var office = e.AddedItems[0] as OfficeLocation;
            if (office != null)
            {
                coord.Latitude = office.latitude;
                coord.Longitude = office.longitude;

                MainMap.Center = coord;
                MainMap.ZoomLevel = 13;
            }
            lbOffices.SelectedItem = null;
        }

        private void lbMilestones_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RadDataBoundListBox listBox = sender as RadDataBoundListBox;
            Milestone milestone = listBox.SelectedItem as Milestone;
            if (milestone != null && !string.IsNullOrWhiteSpace(milestone.source_url))
            {
                this.OpenSite(milestone.source_url);
            }
            lbMilestones.SelectedItem = null;
        }

        private void OpenSite(string url)
        {
            Tasks.ShowWebBrowserTask(url);
        }

        private void jlProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = new CrunchBaseSearchItem();

            item.@namespace = "product";
            item.name = (e.AddedItems[0] as nlStructure).name;
            item.permalink = (e.AddedItems[0] as nlStructure).permalink;
            item.crunchbase_url = string.Format("http://www.crunchbase.com/{0}/{1}", item.@namespace, item.permalink);
            jlProducts.SelectedItem = null;
            context.CurrentSearchItem = item;
            context.companiesStack.Push(context.SelectedObject);
            context.LoadData();
            ArrangePivots();
        }

        private void jlPeople_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = new CrunchBaseSearchItem();

            item.@namespace = "person";
            Relationship person=e.AddedItems[0] as Relationship;
            item.first_name = person.person.first_name;
            item.last_name = person.person.last_name;
            item.permalink = person.person.permalink;
            item.crunchbase_url = string.Format("http://www.crunchbase.com/{0}/{1}", item.@namespace, item.permalink);
            jlPeople.SelectedItem = null;
            context.CurrentSearchItem = item;
            context.companiesStack.Push(context.SelectedObject);
            context.LoadData();
            ArrangePivots();
        }

        private void lbCompanies_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = new CrunchBaseSearchItem();

            item.@namespace = "company";
            Relationship comp = e.AddedItems[0] as Relationship;
            item.name = comp.firm.name;
            item.permalink = comp.firm.permalink;
            item.crunchbase_url = string.Format("http://www.crunchbase.com/{0}/{1}", item.@namespace, item.permalink);
            lbCompanies.SelectedItem = null;
            context.CurrentSearchItem = item;
            context.companiesStack.Push(context.SelectedObject); 
            context.LoadData();
            ArrangePivots();
        }

        private void jlServiceProviders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = new CrunchBaseSearchItem();

            if ((e.AddedItems[0] as PFirm).firm.type_of_entity == "financial_org")
                item.@namespace = "financial-organization";
            else
                item.@namespace = (e.AddedItems[0] as PFirm).firm.type_of_entity;
            item.permalink = (e.AddedItems[0] as PFirm).firm.permalink;
            item.name = (e.AddedItems[0] as PFirm).firm.name;
            item.crunchbase_url = string.Format("http://www.crunchbase.com/{0}/{1}", item.@namespace, item.permalink);
            jlServiceProviders.SelectedItem = null;
            context.CurrentSearchItem = item;
            context.companiesStack.Push(context.SelectedObject);
            context.LoadData();
            ArrangePivots();
        }

        private void jlFinOrgProviders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = new CrunchBaseSearchItem();

            item.@namespace = "service-provider";
            item.permalink = (e.AddedItems[0] as PFirm).provider.permalink;
            item.name = (e.AddedItems[0] as PFirm).provider.name;
            item.crunchbase_url = string.Format("http://www.crunchbase.com/{0}/{1}", item.@namespace, item.permalink);
            jlFinOrgProviders.SelectedItem = null;
            context.CurrentSearchItem = item;
            context.companiesStack.Push(context.SelectedObject);
            context.LoadData();
            ArrangePivots();
        }

        private void BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (context.companiesStack.Count == 0)
                e.Cancel = false;
            else
            {
                context.SelectedObject = context.companiesStack.Pop();
                context.CurrentSearchItem.@namespace = context.SelectedObject.Namespace;
                ArrangePivots();
                e.Cancel = true;
            }
        }
    }
}