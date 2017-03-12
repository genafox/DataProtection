using System.Collections;
using System.Windows;
using Autofac;
using Client.Models;
using Client.ViewModels;
using Contracts.Interfaces;
using DES;
using DES.Domain.Interfaces;
using IoC;
using Microsoft.AspNet.SignalR.Client;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContainer resolver = ContainerInitializer.GetContainer();
        private IDataEncryptor encryptor;
        private BitArray key;

        public MainWindow()
        {
            InitializeComponent();
            SignalR();
            encryptor = resolver.Resolve<IDataEncryptor>();
            GenerateKeyButton_Click(null, null);
        }

        private void GenerateKeyButton_Click(object sender, RoutedEventArgs e)
        {
            key = KeyService.GenerateKey();
            byte[] keyBytes = new byte[8];
            key.CopyTo(keyBytes, 0);
            KeyTextBox.Text = keyBytes.ToHexString();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string message = encryptor.Encrypt(MessageTextBox.Text, KeyTextBox.Text);
            Broadcast(new ChatMessage { Message = message });
        }

        public ChatMessageViewModel ChatVm { get; set; } = new ChatMessageViewModel();
        public HubConnection Conn { get; set; }
        public IHubProxy Proxy { get; set; }

        public void SignalR()
        {
            Conn = new HubConnection("http://tonystark-001-site1.dtempurl.com/");
            Proxy = Conn.CreateHubProxy("ChatHub");
            Conn.Start();

            Proxy.On<ChatMessage>("broadcastMessage", OnMessage);
        }

        public void Broadcast(ChatMessage msg)
        {
            Proxy.Invoke("Send", msg);
        }

        private async void OnMessage(ChatMessage msg)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                string message = encryptor.Decrypt(msg.Message, KeyTextBox.Text);
                LogTextBox.Text += $"{message}\n\r";
            });
        }
    }
}
