using Project.Model;
using Project.Model.Enums;
using Projekat.Controller;
using System;
using System.Text.RegularExpressions;
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
                if (!IsEmailValid(email))
                {
                    MessageBox.Show("Error: Invalid e-mail format!");
                    return;
                }
                if (!IsPhoneNumberValid(pNumber)) 
                {
                    MessageBox.Show("Error: Invalid phone number format\n Expected: +381........!");
                    return;
                }
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
                    string errorMessage = "Error: User with the same email: " + email + " already exists!";
                    MessageBox.Show(errorMessage);
                    return;
                }
                else if(userController.FindByJmbg(jmbg) != null)
                {
                    string errorMessage = "Error: User with the same jmbg: " + jmbg + " already exists!";
                    MessageBox.Show(errorMessage);
                    return;
                }
                else if(jmbg.Length != 13)
                {
                    string errorMessage = "JMBG must be exactly 13 digits!";
                    MessageBox.Show(errorMessage);
                    return;
                }

                userController.Create(userNew);
            }
            else
            {
                MessageBox.Show("Error: you must fill in the fields!");
            }


        }

        private bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            string pattern = @"^\+[0-9]{11,12}$"; // +(11 ili 12 cifara)
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
