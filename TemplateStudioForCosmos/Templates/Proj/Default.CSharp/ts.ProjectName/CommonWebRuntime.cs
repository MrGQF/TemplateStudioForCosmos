using Cosmos.App.Sdk.v1;
using Cosmos.App.Sdk.v1.Contexts;
using Cosmos.App.Sdk.v1.Controls.WebView;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace ts.ProjectName.Shared
{
    internal class CommonWebRuntime : ICommonWebRuntime
    {
        private readonly IWebView webView;
        private readonly ICosmosAccountContext accountContext;

        public CommonWebRuntime(
            IWebView webView,
            ICosmosAccountContext accountContext)
        {
            this.webView = webView;
            this.accountContext = accountContext;
        }

        public Task Close()
        {
            webView?.Close();
            return Task.CompletedTask;
        }

        public object GetWebContent()
        {
            return webView;
        }

        public Task Navigate(string url)
        {
            webView.Navigate(url);
            return Task.CompletedTask;
        }

        public void OpenDevelopTools()
        {
            webView.OpenDevToolsWindow();
        }

        public Config ReadFromResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var configNames = assembly.GetManifestResourceNames();
            var configName = configNames.First(t => t.Contains(fileName));
            var value = assembly.GetManifestResourceStream(configName);
            var config = JsonSerializer.Deserialize<Config>(value);
            return config;
        }

        public Task SetCookie(string url, string domain = null)
        {
            if (accountContext.Cookies == null
                || !accountContext.Cookies.Any())
            {
                return Task.CompletedTask;
            }

            foreach (var cookie in accountContext.Cookies)
            {
                var webViewCookie = webView.Environment.CookieManager.CreateCookieWithSystemNetCookie(cookie as Cookie);
                if(!string.IsNullOrEmpty(domain))
                {
                    webViewCookie.Domain = domain;
                }
                webView.Environment.CookieManager.AddOrUpdateCookie(url, webViewCookie);
            }

            return Task.CompletedTask;
        }
    }
}
