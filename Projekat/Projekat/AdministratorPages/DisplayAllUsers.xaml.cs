using Project.Model;
using Projekat.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private void NameSurname_Sorting(object sender, DataGridSortingEventArgs e)
        {

            var column = e.Column;
            e.Handled = true;

            if (column.SortDirection == ListSortDirection.Descending)
            {
                column.SortDirection = ListSortDirection.Ascending;
                dataGrid.Items.SortDescriptions.Clear();
                dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, ListSortDirection.Ascending));
            }
            else
            {
                column.SortDirection = ListSortDirection.Descending;
                dataGrid.Items.SortDescriptions.Clear();
                dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, ListSortDirection.Descending));
            }

        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)filterComboBox.SelectedItem;
            string userFilter = selectedItem.Tag.ToString();

            if (userFilter == "Show all")
            {
                dataGrid.ItemsSource = allUsers;
            }
            else
            {
                dataGrid.ItemsSource = allUsers.Where(u => u.UserType.ToString() == userFilter);
            }

        }
    }
}
