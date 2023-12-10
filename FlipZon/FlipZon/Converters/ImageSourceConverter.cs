using System;
using System.Globalization;
using System.Net;

namespace FlipZon.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        static WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object response = null;
            try
            {
                if (value == null)
                    response = null;

                var byteArray = Client.DownloadData(value.ToString());
                response= ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

