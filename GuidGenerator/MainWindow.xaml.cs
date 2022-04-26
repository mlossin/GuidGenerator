using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuidGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            Configuration = GenerationConfigurationType.UseQuotationMarks;
            
            var names = Enum.GetNames(typeof(GenerationConfigurationType)).ToList();

            PossibleConfigurations = new ObservableCollection<GenerationConfigurationType>();
            names.ForEach(x =>
            {
                GenerationConfigurationType item;
                if (GenerationConfigurationType.TryParse<GenerationConfigurationType>(x, true, out item))
                {
                    PossibleConfigurations.Add(item);
                }
            });
            InitializeComponent();
        }

        public GenerationConfigurationType Configuration { get; set; }

        public ObservableCollection<GenerationConfigurationType> PossibleConfigurations { get; set; }

        private void GenerateGuidHandler(object sender, RoutedEventArgs e)
        {
            var newGuid = Guid.NewGuid();
            var newGuidText = $"\"{Guid.NewGuid()}\"";
            
                switch (Configuration)
                {
                    case GenerationConfigurationType.UseQuotationMarks:
                        newGuidText = $"\"{newGuid}\"";
                        break;
                    case GenerationConfigurationType.Plain:
                        newGuidText = $"{newGuid}";
                        break;
                    case GenerationConfigurationType.GuidParse:
                        newGuidText = $"Guid.Parse({newGuidText})";
                        break;
                    case GenerationConfigurationType.GuidParseLine:
                        newGuidText = $"var newGuid = Guid.Parse({newGuidText});";
                        break;
                    default:
                        newGuidText = $"{newGuid}";
                        break;
                }

                this.tbGeneratedGuid.Text = newGuidText;
            Clipboard.Clear();
            Clipboard.SetText(newGuidText);
        }
    }
}
