using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using Phone7.Fx.Extensions;

namespace DevRainSolutions.TechCompanies.Core
{
    public class DataService
    {
        private IsolatedStorageFile isoStore;

        string isoFilePath;

        IsolatedStorageFileStream historyFile;
        
        public DataService()
        {
            isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            using (var context = new CrunchBaseDataContext())
            {
                if (!context.DatabaseExists())
                {
                    // create database if it does not exist
                    context.CreateDatabase();
                }
            }
        }

        public void Save(string name, string content)
        {
            var fileName = name + ".js";
            var sw = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Create, isoStore));
            sw.WriteLine(content);
            sw.Close();
        }

        public bool FileExists(string name)
        {
            return isoStore.FileExists(name + ".js");
        }

        public T GetDetails<T>(string name)
        {
            var fileName = name + ".js";
            var file = isoStore.OpenFile(fileName, FileMode.Open);
            var s = new DataContractJsonSerializer(typeof(T));
            var details = (T)s.ReadObject(file);
            file.Close();
            return details;
        }

        public ObservableCollection<CrunchBaseSearchItem> GetHistory()
        {
            using (var context = new CrunchBaseDataContext())
            {
                return (from i in context.Companies
                        orderby i.DateAdded descending 
                        select i).ToObservableCollection();
            }
        }

        public void DropDatabase()
        {
            //isoStore.Remove();
            using (var context = new CrunchBaseDataContext())
            {
                context.Companies.Context.DeleteDatabase();
                context.Companies.Context.CreateDatabase();
            }
        }

        public void AddToHistory(CrunchBaseSearchItem item)
        {
            using (var context = new CrunchBaseDataContext())
            {
                var company = context.Companies.FirstOrDefault(i => (i.permalink == item.permalink && i.@namespace == item.@namespace));

                if (company != null)
                {
                    company.DateAdded = DateTime.Now;
                }
                else
                {
                    item.DateAdded = DateTime.Now;
                    context.Companies.InsertOnSubmit(item);
                }

                context.SubmitChanges();
            }
        }
    }
}
