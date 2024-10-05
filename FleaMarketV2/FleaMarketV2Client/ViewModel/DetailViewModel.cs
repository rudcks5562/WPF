using CommonLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class DetailViewModel :BaseViewModel
    {

        private SItemModel model;
        public ICommand MinimizeCommand { get; }
        public ICommand CloseCommand { get; }

        public DetailViewModel(SItemModel sel)
        {
            MinimizeCommand = new DelegateCommand(OnMinimize);
            CloseCommand = new DelegateCommand(OnClose);
            Model = sel;
            if(sel.ImageData is null)
            {
                sel.ImageData = ImageToByteArray("/Resources/Images/cat2.jpg");
                OnPropertyChanged(nameof(Model));

            }
        }
        public byte[] ImageToByteArray(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentException("이미지 경로가 유효하지 않습니다.", nameof(imagePath));

            if (!File.Exists(imagePath))
                throw new FileNotFoundException("이미지 파일을 찾을 수 없습니다.", imagePath);

            byte[] imageBytes;
            try
            {
                imageBytes = File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                throw new IOException("이미지 파일을 읽는 중 오류가 발생했습니다.", ex);
            }

            return imageBytes;
        }
        public SItemModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private void OnMinimize(object parameter)
        {
            var window = parameter as Window;
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }

        private void OnClose(object parameter)
        {
            var window = parameter as Window;
            if (window != null)
                window.Close();
        }



    }
}
