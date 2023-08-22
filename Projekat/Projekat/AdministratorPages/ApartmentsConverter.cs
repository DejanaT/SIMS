﻿using Project.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Projekat.AdministratorPages
{

    public class ApartmentsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IDictionary<string, Apartment> apartments)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var apartmentEntry in apartments)
                {
                    sb.Append(apartmentEntry.Value.Name);
                    sb.Append(", ");
                }
                if (sb.Length > 0)
                    sb.Length -= 2;
                return sb.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
