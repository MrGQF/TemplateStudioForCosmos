using Cosmos.App.Sdk.v1;
using Cosmos.App.Sdk.v1.Controls.WebView;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Avalonia.Controls;

namespace ts.ProjectName.Shared
{
    public class ContentGui : UserControl, ICosmosAppWidget
    {
        public IWebView webView;
        public ICommonWebRuntime commonRuntime;
        public ICosmosAppAccessProvider AccessProvider { get; set; }
        public ContentGui()
        {
            
        }

        private ICosmosAppContextInjection _contextInjection = null;
        public virtual ICosmosAppContextInjection ContextInjection
        {
            get
            {
                return _contextInjection;
            }
            set
            {
                _contextInjection = value;
                OnContextInjectionSet(_contextInjection);
            }
        }

        void OnContextInjectionSet(ICosmosAppContextInjection contextInjection)
        {
            try
            {
                webView = contextInjection.ThisAppContext.GlobalContexts.EngineContext.WebViewFactory.CreateWebView();
                commonRuntime = new CommonWebRuntime(webView, contextInjection.ThisAppContext.GlobalContexts.AccountContext);
                var config = commonRuntime.ReadFromResource("Config.json");
                try
                {
                    commonRuntime.SetCookie(config.Url, config.Domain);
                }
                catch (Exception ex)
                {
                    contextInjection.ThisAppContext?.AppLogger?.Log(CosmosLogLevel.Error, $"ContentGui SetCookie Failed:{ex}");
                }

                commonRuntime.Navigate(config.Url);
                Content = commonRuntime.GetWebContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ContentGui OnContextInjectionSet Failed:{ex}");
                contextInjection?.ThisAppContext?.AppLogger?.Log(CosmosLogLevel.Error, $"ContentGui OnContextInjectionSet Failed:{ex}");
            }
        }

        public Task ContinueAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task PauseAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            commonRuntime?.Close();
            return Task.CompletedTask;
        }

        public async Task OnAppInjectionCompleteAsync()
        {
            return;
        }
    }
}
