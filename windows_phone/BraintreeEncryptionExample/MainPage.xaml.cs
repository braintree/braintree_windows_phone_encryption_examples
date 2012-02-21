using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using BraintreeEncryption.Library;

namespace BraintreeEncryptionExample
{
    public partial class MainPage
    {
        private const string PublicKey = "your_client_side_encryption_public_key";
        private IsolatedStorageSettings _isolatedStorageSettings;
        private const string _merchantServerUrlKey = "MerchantServerUrl";
        private string _merchantServerUrl;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DisableForm();
            PostToMerchantServer();
        }

        private void PostToMerchantServer()
        {
            var braintree = new Braintree(PublicKey);
            var parameters = new Dictionary<string, object>
                                 {
                                     {"cc_number", braintree.Encrypt(CreditCardNumber.Text)},
                                     {"cc_exp_date", braintree.Encrypt(ExpirationDate.Text)},
                                     {"cc_cvv", braintree.Encrypt(Cvv.Text)}
                                 };

            var client = new BraintreeHttpClient(_merchantServerUrl, UploadStringCompleted);
            client.Post(parameters);
        }

        private void UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            MessageBox.Show(e.Error == null ? e.Result : "Could not connect to the merchant server.");
            EnableForm();
        }

        private void DisableForm()
        {
            CreditCardNumber.IsEnabled = false;
            ExpirationDate.IsEnabled = false;
            Cvv.IsEnabled = false;
            Submit.IsEnabled = false;
        }

        private void EnableForm()
        {
            CreditCardNumber.IsEnabled = true;
            ExpirationDate.IsEnabled = true;
            Cvv.IsEnabled = true;
            Submit.IsEnabled = true;
        }

        private void LoadSettings()
        {
            _isolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;
            if (_isolatedStorageSettings.Contains(_merchantServerUrlKey))
            {
                _merchantServerUrl = (string)_isolatedStorageSettings[_merchantServerUrlKey];
            }
        }
    }
}