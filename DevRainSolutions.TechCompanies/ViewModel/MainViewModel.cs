using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevRainSolutions.TechCompanies.Core;
using DevRainSolutions.TechCompanies.Core.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Hammock;
using Microsoft.Phone.Controls;
using Phone7.Fx.Extensions;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace DevRainSolutions.TechCompanies.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        DataService dataService;
        bool dataUpdating;
        public CrunchBaseSearchResult SearchResult { get; private set; }
        public CrunchBaseSearchItem CurrentSearchItem { get; set; }

        public bool isBusy;
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                this.RaisePropertyChanged(() => IsBusy);
            }
        }

        public Stack<CrunchBaseCompany> companiesStack;

        private void NavigateTo(string page)
        {
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri("/Pages/" + page + ".xaml", UriKind.Relative));
        }

        private CrunchBaseCompany selectedObject;
        public CrunchBaseCompany SelectedObject
        {
            get { return selectedObject; }
            set
            {
                selectedObject = value;
                RaisePropertyChanged(() => SelectedObject);
            }
        }

        public string SearchText { get; set; }

        public ICommand CompanySelectionChanged
        {
            get
            {
                return new RelayCommand<SelectionChangedEventArgs>(

                    args =>
                    {
                        if (args.AddedItems.Count > 0)
                        {
                            CurrentSearchItem = args.AddedItems[0] as CrunchBaseSearchItem;
                            this.LoadData();
                            //args.AddedItems.Clear();
                        }
                    },

                    args => args.AddedItems.Count > 0);
            }
        }




        public ICommand SearchButtonCommand
        {
            get
            {
                return new RelayCommand<KeyEventArgs>((args) =>
                                                          {
                                                              if (args.Key == Key.Enter)
                                                              {
                                                                  SearchCompanies(SearchText);
                                                              }
                                                          });
            }
        }

        public ICommand DropDBCommand
        {
            get
            {
                return new RelayCommand(() =>
                                            {

                                                if (
                                                    MessageBox.Show(
                                                        "Are you sure you want to delete all offline content?",
                                                        "Tech Companies",
                                                        MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                                                {

                                                    dataService.DropDatabase();
                                                    RaisePropertyChanged(() => History);
                                                }
                                            });

            }
        }

        public ICommand UpdateInformationCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var item = new CrunchBaseSearchItem
                    {
                        @namespace = SelectedObject.Namespace,
                        permalink = SelectedObject.permalink
                    };

                    if (item.@namespace == "person")
                    {
                        item.first_name = SelectedObject.first_name;
                        item.last_name = SelectedObject.last_name;
                    }
                    else
                    {
                        item.name = SelectedObject.name;
                    }

                    item.crunchbase_url = string.Format("http://www.crunchbase.com/{0}/{1}", item.@namespace, item.permalink);
                    CurrentSearchItem = item;
                    dataUpdating = true;
                    this.LoadData();
                });
            }
        }

        public ICommand GoToAboutPageCommand { get { return new RelayCommand(() => NavigateTo("about")); } }
        public ICommand GoToSearchPageCommand { get { return new RelayCommand(() => NavigateTo("search")); } }

        public ICommand SendAFeedbackCommand
        {
            get
            {
                return
                    new RelayCommand(
                        () => Tasks.ShowEmailComposeTask("support@devrain.com", "Tech Companies for Windows Phone"));
            }
        }

        public ICommand GoWebCommand { get { return new RelayCommand(() => Tasks.ShowWebBrowserTask("http://devrain.com")); } }

        public ICommand ReviewCommand { get { return new RelayCommand(Tasks.ShowMarketplaceReviewTask); } }

        public ICommand AnotherAppsCommand { get { return new RelayCommand(() => Tasks.ShowMarketplaceSearchTask("DevRain Solutions")); } }

        public ObservableCollection<CrunchBaseSearchItem> SearchResults
        {
            get { return this.SearchResult != null ? this.SearchResult.results : null; }
        }

        public ObservableCollection<CrunchBaseSearchItem> SearchResultsCompanies
        {
            get
            {
                return this.SearchResult != null ? SearchResult.results.Where(i => i.Type == CrunchbaseTypes.Company).ToObservableCollection()
                    : new ObservableCollection<CrunchBaseSearchItem>();
            }
        }

        public ObservableCollection<CrunchBaseSearchItem> SearchResultsPersons
        {
            get
            {
                return this.SearchResult != null ? SearchResult.results.Where(i => i.Type == CrunchbaseTypes.Person).ToObservableCollection()
                    : new ObservableCollection<CrunchBaseSearchItem>();
            }
        }

        public ObservableCollection<CrunchBaseSearchItem> SearchResultsProducts
        {
            get
            {
                return this.SearchResult != null ? SearchResult.results.Where(i => i.Type == CrunchbaseTypes.Product).ToObservableCollection()
                    : new ObservableCollection<CrunchBaseSearchItem>();
            }
        }

        public ObservableCollection<CrunchBaseSearchItem> History
        {
            get { return dataService.GetHistory(); }
        }

        public MainViewModel()
        {
            dataService = new DataService();
            companiesStack = new Stack<CrunchBaseCompany>();
            dataUpdating = false;

            //this.SearchText = "Microsoft";



        }

        public void SearchCompanies(string searchText)
        {
            SetBusy(true);
            var url = string.Format(Constants.SearchQuery, searchText, 1);
            Html(url, ProcessSearchCompaniesResponse);
        }

        public void Html(string url, Action<RestResponse<string>, object> callback)
        {
            Html(url, callback, null);
        }

        public void Html(string url, Action<RestResponse<string>, object> callback, object objectState)
        {
            var client = new RestClient { Timeout = TimeSpan.FromMilliseconds(Constants.DefaultRequestTimeout) };
            var request = new RestRequest { Path = url };
            client.BeginRequest(request, new RestCallback<string>((req, response, state) => callback.Invoke(response, objectState)));
        }

        public void SetBusy(bool value)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = value;
            });
        }

        void ProcessSearchCompaniesResponse(RestResponse<string> results, object state)
        {
            try
            {
                var s = new DataContractJsonSerializer(typeof(CrunchBaseSearchResult));
                var searchResult = s.ReadObject(results.ContentStream) as CrunchBaseSearchResult;

                if (searchResult != null)
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                                              {
                                                                  SearchResult = searchResult;
                                                                  RaisePropertyChanged(() => SearchResults);
                                                                  RaisePropertyChanged(() => SearchResultsCompanies);
                                                                  RaisePropertyChanged(() => SearchResultsPersons);
                                                                  RaisePropertyChanged(() => SearchResultsProducts);
                                                              });
                }
            }
            catch (Exception ex)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => MessageBox.Show(ex.Message, "Error occured", MessageBoxButton.OK));
            }
            finally
            {
                SetBusy(false);
            }
        }

        void ProcessDataResponse(RestResponse<string> results, object state)
        {
            CrunchBaseSearchItem item = (state as CrunchBaseSearchItem);
            var name = item.@namespace + "." + item.permalink;
            var s = new DataContractJsonSerializer(typeof(CrunchBaseCompany));
            var root = (CrunchBaseCompany)s.ReadObject(results.ContentStream);
            dataService.Save(name, results.Content);

            DispatcherHelper.CheckBeginInvokeOnUI(() => NavigateToCompanyDetails(root, item));
        }

        public void LoadData()
        {
            SetBusy(true);

            // network exists
            if (DeviceInfoHelper.NetworkExists)
            {
                string url = string.Format(Constants.CompanyQuery, CurrentSearchItem.@namespace, CurrentSearchItem.permalink);
                Html(url, ProcessDataResponse, CurrentSearchItem);
            }
            else
            {
                //var name = CurrentSearchItem.@namespace + "." + CurrentSearchItem.permalink;
                //if (dataService.FileExists(name) && !dataUpdating)
                //{
                //    NavigateToCompanyDetails(dataService.GetDetails<CrunchBaseCompany>(name), CurrentSearchItem);
                //}
                //else
                //{
                dataUpdating = false;
                MessageBox.Show("No connection. Please try later", "Error occured", MessageBoxButton.OK);
                SetBusy(false);
                //}
            }
        }

        private void NavigateToCompanyDetails(CrunchBaseCompany company, CrunchBaseSearchItem item)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (company.offices != null)
                {
                    foreach (OfficeLocation office in company.offices)
                    {
                        office.Id = company.offices.IndexOf(office) + 1;
                    }
                    if (company.offices.Count != 0)
                        company.MapCenter = company.offices[0].Location;
                }

                company.Namespace = item.@namespace;
                SelectedObject = company;

                this.IsBusy = false;
                // save in history
                NavigateTo("CompanyDetails"); // TBD
                dataService.AddToHistory(CurrentSearchItem);
                RaisePropertyChanged(() => this.History);
            });
        }
    }
}