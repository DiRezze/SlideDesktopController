using System.Net;
using Microsoft.Maui.Controls;
using QRCoder;

namespace SlideDesktopController
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckNetworkConnection();
        }

        private void CheckNetworkConnection()
        {
            try
            {
                string ipAddress = GetLocalIPAddress();

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    GenerateQRCode(ipAddress);
                }
                else
                {
                    DisplayAlert("Sem conexão", "É necessário ter conexão com a internet para usar esse serviço", "Ok");
                    throw new Exception("Endereço IP não foi encontrado.");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("Nenhuma conexão de rede disponível.");
        }

        private void GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            RenderQRCode(qrCodeBytes, QRCodeImage);
        }

        private void RenderQRCode( byte[] imgBytes, Image targetImg)
        {
            targetImg.Source = ImageSource.FromStream(() => new MemoryStream(imgBytes));
        }

        private void ClickedTestConnection(object sender, EventArgs e)
        {
            CheckNetworkConnection();
        }
    }
}
