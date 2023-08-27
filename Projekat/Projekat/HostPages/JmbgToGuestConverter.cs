using Project.Model;
using Projekat.Controller;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Projekat.HostPages
{
    public class JmbgToGuestConverter : IValueConverter
    {
        private UserController userController = new UserController();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string guestJmbg = value as string;

            User guest = userController.FindByJmbg(guestJmbg);
            string guestFullName = $"{guest.Name} {guest.Surname}";

            return guestFullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

