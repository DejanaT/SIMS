﻿using Project.Model;
using Projekat.Controller;
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
using System.Windows.Shapes;

namespace Projekat.AdministratorPages
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        public AdminWindow(User user)
        {
            InitializeComponent();
            Name.Text = user.Name;
            Surname.Text = user.Surname;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = Menu.SelectedIndex;
            if (index == -1) return;
            switch (index)
            {
                
                case 0:
                    mainFrame.Navigate(new CreateUser());
                    break;
                case 1:
                    mainFrame.Navigate(new DisplayAllUsers());
                    break;
                case 2:
                    mainFrame.Navigate(new CreateHotel());
                    break;
                case 3:
                    mainFrame.Navigate(new DisplayAllHotels());
                    break;
            }

            Menu.SelectedIndex = -1;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
    }
}
