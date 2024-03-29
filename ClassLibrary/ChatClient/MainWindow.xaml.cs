﻿using ChatClient.ServiceChat;
using System;
using System.Collections.Generic;
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
using ChatClient.ServiceChat;

namespace ChatClient
{
    public partial class MainWindow : Window , ServiceChat.IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChat.ServiceChatClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }
        void ConnectUser() 
        {
            if (!isConnected)
            {
                client = new ServiceChat.ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                isConnected = true;
                bConnDicon.Content = "Disconnect";
            }
        }
        void DisconnectUser() 
        {
            client.Disconnect(ID);
            client = null;
            tbUserName.IsEnabled = true;
            isConnected = false;
            bConnDicon.Content = "Connect";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessege_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(tbMessege.Text, ID);
                    tbMessege.Text = String.Empty;
                }
            }
        }
    }
}
