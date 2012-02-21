using System;
using System.IO.IsolatedStorage;

namespace BraintreeEncryptionExample
{
    public partial class Settings
    {
        private IsolatedStorageSettings _isolatedStorageSettings;
        private const string _merchantServerUrlKey = "MerchantServerUrl";

        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadSettings();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            WriteSettings();
            _isolatedStorageSettings.Save();
            NavigateToMainPage();
        }

        private void Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigateToMainPage();
        }

        private void LoadSettings()
        {
            _isolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;

            if (_isolatedStorageSettings.Contains(_merchantServerUrlKey))
            {
                MerchantServerUrl.Text = (string)_isolatedStorageSettings[_merchantServerUrlKey];
            }
            else _isolatedStorageSettings.Add(_merchantServerUrlKey, "");
        }

        private void WriteSettings()
        {
            _isolatedStorageSettings[_merchantServerUrlKey] = MerchantServerUrl.Text;
        }

        private void NavigateToMainPage()
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}