using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

// 한곳에 몰아넣기 나중에
namespace FleaMarketV2Client.Service
{
    public class ImageToByteConvertService : IValueConverter// 바이트 데이터를 image로 변환하는 함수입니다!!!!! 이름을 잘못지었습니다!!!
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cat2.jpg");
            if (value is byte[] imgBytes)
            {
                if(value is null)
                {
                    if (File.Exists(defaultImagePath))
                    {
                        return new BitmapImage(new Uri(defaultImagePath, UriKind.Absolute));
                    }
                }
                try
                {
                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        // MemoryStream에서 이미지 객체 생성
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = ms;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze(); // 이미지를 사용 중인 스레드 외부에서도 사용할 수 있도록 Freeze
                        return bitmapImage;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("이미지 변환 중 오류 발생: " + ex.Message);
                }
            }
           // return new BitmapImage(new Uri("pack://application:,,,/cat2.jpg"));
//return new BitmapImage(new Uri("./cat2.jpg", UriKind.Absolute));
            if (File.Exists(defaultImagePath))
            {
                return new BitmapImage(new Uri(defaultImagePath, UriKind.Absolute));
            }


            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}