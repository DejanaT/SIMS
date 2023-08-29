using Project.Model;
using Projekat.Controller;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.AdministratorPages
{
    /// <summary>
    /// Interaction logic for DisplayAllUsers.xaml
    /// </summary>
    public partial class DisplayAllUsers : Page
    {

        private UserController userController = new UserController();
        private List<User> allUsers = new List<User>();

        public DisplayAllUsers()
        {
            InitializeComponent();
            allUsers = userController.GetUsers();
            dataGrid.ItemsSource = allUsers;
        }

        private void FilterAndSort()
        {
            ComboBoxItem filterSelectedItem = (ComboBoxItem)filterComboBox.SelectedItem;
            string userFilter = filterSelectedItem.Tag.ToString();

            IEnumerable<User> filteredUsers;

            if (userFilter == "Show all")
            {
                filteredUsers = allUsers;
            }
            else
            {
                filteredUsers = allUsers.Where(u => u.UserType.ToString() == userFilter);
            }

            ApplySorting(filteredUsers);
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                return;
            }

            ComboBoxItem sortSelectedItem = (ComboBoxItem)sortComboBox.SelectedItem;
            string sortOption = sortSelectedItem.Tag.ToString();

            IEnumerable<User> sortedUsers = dataGrid.ItemsSource as IEnumerable<User>;

            ApplySorting(sortedUsers, sortOption);
        }

        private void ApplySorting(IEnumerable<User> users, string sortOption = null)
        {
            if (sortOption == null)
            {
                dataGrid.ItemsSource = users;
                return;
            }

            if (sortOption == "Name Asc")
            {
                dataGrid.ItemsSource = users.OrderBy(u => u.Name);
            }
            else if (sortOption == "Name Desc")
            {
                dataGrid.ItemsSource = users.OrderByDescending(u => u.Name);
            }
            else if (sortOption == "Surname Asc")
            {
                dataGrid.ItemsSource = users.OrderBy(u => u.Surname);
            }
            else if (sortOption == "Surname Desc")
            {
                dataGrid.ItemsSource = users.OrderByDescending(u => u.Surname);
            }
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterAndSort();
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            User selectedUser = (User)selectedRow.Item;
            //selectedUser.Blocked = true;
            if (selectedUser.UserType.ToString() != "Administrator")
            {
                userController.BlockUser(selectedUser);
                dataGrid.Items.Refresh();
                MessageBox.Show($"You have successfully blocked user :  { selectedUser.Name} {selectedUser.Surname}.");
            }
            else
            {
                MessageBox.Show($"Administrators hold a special status and cannot be blocked!");
            }

        }

        private void UnblockButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            User selectedUser = (User)selectedRow.Item;
            //selectedUser.Blocked = false;
            userController.UnblockUser(selectedUser);
            dataGrid.Items.Refresh();
            MessageBox.Show($"You have successfully unblocked user :  {selectedUser.Name} {selectedUser.Surname}.");
        }
    }

}
