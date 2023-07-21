using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.CommunityToolkit.Core;
using Xamarin.Forms;

namespace PM2E2GRUPOLENINS.Config
{
    public partial class Base64toVideo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MediaSource videoSource = null;

            if (value != null)
            {
                string base64Video = (string)value;
                byte[] videoBytes = System.Convert.FromBase64String(base64Video);
                var filePath = Path.GetTempFileName();
                File.WriteAllBytes(filePath, videoBytes);
                videoSource = MediaSource.FromFile(filePath);
            }

            return videoSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
