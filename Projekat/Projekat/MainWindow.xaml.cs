using Project.Model;
using Project.Model.Enums;
using Projekat.AdministratorPages;
using Projekat.Controller;
using Projekat.DTOs;
using Projekat.GuestPages;
using Projekat.HostPages;
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
        private int attemptsNumber = 0;
        private int maxAttempts = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            loginDTO.Email = Email.Text;
            loginDTO.Password = Password.Password;

            if (userController.IsUserExist(loginDTO))
            {
                User user = userController.FindByEmail(loginDTO.Email);
                CheckUserType(user);
            }
            else
            {
                int remainingAttempts = CheckRemainingAttempts();
                if(remainingAttempts > 0)
                {
                    MessageBox.Show("Invalid credentials!\nRemaining attempts: " + remainingAttempts);
                }
              
            }
        }

        public int CheckRemainingAttempts()
        {
            attemptsNumber++;
            int remainingAttempts = maxAttempts - attemptsNumber;

            if (remainingAttempts < 1)
            {
                MessageBox.Show("You have used all attempts.\n" +
                                "You are forbidden from logging in again.");
                this.Close();
            }

            return remainingAttempts;
        }

        public void CheckMaxAttempts()
        {
            if (attemptsNumber >= maxAttempts)
            {
                MessageBox.Show("You have used " + maxAttempts + " attempts.\n" +
                                "You are forbidden from logging in again.");
                this.Close();
            }
        }
        public void CheckUserType(User user)
        {
            if (user.UserType == UserType.Administrator)
            {
                ShowWindow(new AdminWindow(user));
            }
            else if (user.UserType == UserType.Host)
            {
                ShowWindow(new HostWindow(user));
            }
            else
            {
                ShowWindow(new GuestWindow(user));
            }
        }

        private void ShowWindow(Window window)
        {
            window.Show();
            this.Close();
        }
    }
}

