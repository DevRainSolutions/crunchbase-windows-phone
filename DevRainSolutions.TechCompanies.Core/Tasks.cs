using System;
using Microsoft.Phone.Tasks;

namespace DevRainSolutions.TechCompanies.Core
{
    public static class Tasks
    {
        public static bool IsLoaded { get; set; }

        public static void ShowMarketplaceReviewTask()
        {
            var task = new MarketplaceReviewTask();
            ExecuteTask(task.Show);

        }

        public static void ShowEmailComposeTask(string to, string subject, string body)
        {
            var task = new EmailComposeTask
            {
                To = to,
                Subject = subject,
                Body = body
            };
            ExecuteTask(task.Show);
        }

        public static void ShowEmailComposeTask(string to, string subject)
        {
            var task = new EmailComposeTask
            {
                To = to,
                Subject = subject
            };
            ExecuteTask(task.Show);
        }

        public static void ShowSmsComposeTask(string to, string body)
        {
            var task = new SmsComposeTask
            {
                To = to,
                Body = body
            };
            ExecuteTask(task.Show);
        }

        public static void ShowWebBrowserTask(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (url.StartsWith("mailto:"))
                {
                    Tasks.ShowEmailComposeTask(url.Replace("mailto:", null), string.Empty);
                    return;
                }

                var task = new WebBrowserTask { Uri = new Uri(url) };
                ExecuteTask(task.Show);
            }
        }

        public static void ShowShareStatusTask(string status)
        {
            var task = new ShareStatusTask { Status = status };
            ExecuteTask(task.Show);
        }

        public static void ShowShareLinkTask(string title, string link, string message)
        {
            var task = new ShareLinkTask
            {
                Title = title,
                LinkUri = new Uri(link, UriKind.Absolute),
                Message = message
            };
            ExecuteTask(task.Show);
        }

        public static void ShowMarketplaceSearchTask(string term)
        {
            var task = new MarketplaceSearchTask { ContentType = MarketplaceContentType.Applications, SearchTerms = term };
            ExecuteTask(task.Show);

        }

        private static void ExecuteTask(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (InvalidOperationException e)
            {


            }

        }

        public static void ShowShareLinkStatusTask(string url, string title, string message = "Check out this link!")
        {
            var task = new ShareLinkTask
                { 
                    LinkUri = new Uri(url), 
                    Message = message, 
                    Title = title
                };
            ExecuteTask(task.Show);
        }
    }
}
