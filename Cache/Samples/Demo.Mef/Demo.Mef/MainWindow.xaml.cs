using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.ComponentModel.Composition;

namespace Demo.Mef
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPartImportsSatisfiedNotification
    {
        // Mark the Logger to be imported by MEF
        [Import]
        public ILogger Logger { get; set; }

        [ImportMany("Demo.Mef.MenuItems")]
        public IEnumerable<Button> MenuItems { get; set; }

        #region rest

        public MainWindow()
        {
            InitializeComponent();
            Compose();
            listboxMessages.ItemsSource = Logger.Logs;
        }

        private void Compose()
        {
            // AssemblyCatalog takes an assembly and  looks for all Imports and Exports within it
            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            // DirectoryCatalog takes all assemblies with in a given dir and looks for Imports/Exports
            var directoryCatalog = new DirectoryCatalog("PlugIns");

            // AggregateCatalog holds multiple  ComposablePartCatalogs
            var aggregator = new AggregateCatalog();
            aggregator.Catalogs.Add(assemblyCatalog);
            aggregator.Catalogs.Add(directoryCatalog);

            var container = new CompositionContainer(aggregator);
            container.ComposeParts(this);
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log(txtMessage.Text);
            txtMessage.Text = string.Empty;
        }

        #endregion

        public void OnImportsSatisfied()
        {
            foreach (var menuItem in MenuItems)
            {
                stackPanelMenu.Children.Add(menuItem);
            }
        }
    }
}
