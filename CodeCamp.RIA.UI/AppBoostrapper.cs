using System.Windows.Controls;

namespace CodeCamp.RIA.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using Caliburn.Micro;
    using CodeCamp.RIA.UI.Controls;
    using CodeCamp.RIA.UI.ViewModels;
    using CodeCamp.RIA.UI.Infrastructure.Services;

    public class AppBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer _container;

        protected override void Configure()
        {
            InitializeContainer();
            InitializeConventions();
        }

        private void InitializeContainer()
        {

            var catalog = new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());

            _container = CompositionHost.Initialize(catalog);

            var batch = new CompositionBatch();

            IEventAggregator eventAggregator = new EventAggregator();
            IWindowManager windowManager = new WindowManager();
            ILoggingService loggingService = new LoggingService();
            eventAggregator.Subscribe(windowManager);

            batch.AddExportedValue<IEventAggregator>(eventAggregator);
            batch.AddExportedValue<IWindowManager>(windowManager);
            batch.AddExportedValue<ILoggingService>(loggingService);
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        private static void InitializeConventions()
        {
            ConventionManager.AddElementConvention<BusyIndicatorEx>(BusyIndicator.IsBusyProperty, "IsBusy", "Loaded");
            ConventionManager.AddElementConvention<ExtendedTextBox>(TextBox.TextProperty, "Text", "EnterKeyDown");
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Any())
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
    }

}
