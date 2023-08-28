using Project.Model;
using Projekat.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for AddApartments.xaml
    /// </summary>
    // ...
    public partial class AddApartments : Window
    {

        private ApartmentController apc = new ApartmentController();
        private Hotel selectedHotel = new Hotel();
        private ObservableCollection<Apartment> addedApartments;

        public AddApartments(List<Apartment> existingApartments, Hotel hotel)
        {
            InitializeComponent();
            selectedHotel = hotel;
            addedApartments = new ObservableCollection<Apartment>(existingApartments);
            DataContext = this;

        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (addedApartments.Count == 0)
            {
                MessageBox.Show("You haven't added any apartments.");
                return;
            }

            foreach (var apartment in addedApartments)
            {
                selectedHotel.Apartments.Add(apartment.Name, apartment);
            }
            DialogResult = true;

        }

        private void ClearInputs()
        {
            Name.Text = string.Empty;
            Description.Text = string.Empty;
            Number.Text = string.Empty;
            MaxGuests.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsValid())
            {
                string name = Name.Text;
                string description = Description.Text;
                int number = int.Parse(Number.Text);
                int maxGuests = int.Parse(MaxGuests.Text);

                Apartment newApartment = new Apartment
                {
                    Name = name,
                    Description = description,
                    RoomsQuantity = number,
                    MaxNumberOfGuests = maxGuests
                };

                addedApartments.Add(newApartment);
                apc.Create(newApartment);
                dataGrid.Items.Add(newApartment);
                ClearInputs();
            }
        }

        public ObservableCollection<Apartment> AddedApartments
        {
            get { return addedApartments; }
        }


        private bool AreFieldsValid()
        {
            if (String.IsNullOrEmpty(Name.Text) || String.IsNullOrEmpty(Description.Text) ||
                String.IsNullOrEmpty(Number.Text) || String.IsNullOrEmpty(MaxGuests.Text) )
            {
                MessageBox.Show("Error: you must fill in all fields");
                return false;
            }

            if (apc.FindByName(Name.Text) != null)
            {
                MessageBox.Show("Error: Apartment with the same Name already exists");
                return false;
            }

            if (!int.TryParse(Number.Text, out int number) || number <= 0)
            {
                MessageBox.Show("Error: Number of rooms must be positive integer.");
                return false;
            }

            if (!int.TryParse(MaxGuests.Text, out int maxG) || maxG <= 0)
            {
                MessageBox.Show("Error: Max Guests must be a positive integer.");
                return false;
            }

            return true;
        }
    }
}

