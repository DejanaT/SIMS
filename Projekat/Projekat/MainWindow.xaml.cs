using Projekat.AdministratorPages;
using Projekat.Controller;
using Projekat.DTOs;
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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private UserController userController = new UserController();
        private LoginDTO loginDTO = new LoginDTO();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            loginDTO.Email = Email.Text;
            loginDTO.Password = Password.Text;

            if (userController.IsUserExist(loginDTO))
            {
                Frame newFrame = new Frame();
                newFrame.Navigate(new CreateUser());
                this.Content = newFrame;

            }
            else
            {
                MessageBox.Show("Invalid credentials or you are blocked!");
            }
        }

     }
}

