using Project.Model;
using Project.Model.Enums;
using Projekat.Controller;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.AdministratorPages
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Page
    {
        private UserController userController = new UserController();

        public CreateUser()
        {
            InitializeComponent();
            ComboBox.Items.Add("Host");
            ComboBox.Items.Add("Guest");
        }

        private void Create_User_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string surname = Surname.Text;
            string jmbg = JMBG.Text;
            string pNumber = PhoneNumber.Text;
            string email = Email.Text;
            string password = Password.Password;
            string combo = ComboBox.Text;
            UserType ut;

            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(surname) &&
                !String.IsNullOrEmpty(jmbg) && !String.IsNullOrEmpty(pNumber) &&
                !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) &&
                !String.IsNullOrEmpty(combo))
            {
                if (ComboBox.SelectedIndex == 0)
                {
                    ut = UserType.Host;
                }
                else
                {
                    ut = UserType.Guest;
                }

                User userNew = new User
                {
                    Name = name,
                    Surname = surname,
                    JMBG = jmbg,
                    MobilePhone = pNumber,
                    Email = email,
                    Password = password,
                    UserType = ut,
                    Blocked = false,

                };

                if (userController.FindByEmail(email) != null)
                {
                    MessageBox.Show("User with same email already exists");
                    return;
                }
                else if(userController.FindByJmbg(jmbg) != null)
                {
                    MessageBox.Show("User with same jmbg already exists");
                    return;
                }

                userController.Create(userNew);
            }
            else
            {
                MessageBox.Show("Error: you must fill in the fields");
            }


        }
    }
}
