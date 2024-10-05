using CommonLib.Model;
using FleaMarketV2Client.Model;
using FleaMarketV2Client.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class RegistViewModel : BaseViewModel
    {
        private string itemName;
        private int itemCost;
        private string itemDesc;
        private string itemRegLog;
        private string imagePath;
        public string BasicPath = "/Resources/Images/cat2.jpg";

        public string ItemName
        {
            get => itemName;
            set
            {
                itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        public int ItemCost
        {
            get => itemCost;
            set
            {
                itemCost = value;
                OnPropertyChanged(nameof(ItemCost));
            }
        }
        public string ItemDesc
        {
            get => itemDesc;
            set
            {
                itemDesc = value;
                OnPropertyChanged(nameof(ItemDesc));
            }
        }
        public string ItemRegLog
        {
            get => itemRegLog;
            set
            {
                itemRegLog = value;
                OnPropertyChanged(nameof(itemRegLog));
            }
        }
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public ICommand ImagePathBtnCommand { get; }
        public ICommand ExecuteRegistItemCommand { get; }



        public RegistViewModel()
        {
            ImagePathBtnCommand = new DelegateCommand(GetImagePath);
            ExecuteRegistItemCommand = new DelegateCommand(ExecuteRegistItem);
            ItemRegLog = DateTime.Now.ToString();
            OnPropertyChanged(nameof(ItemRegLog));

        }

        public void GetImagePath(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // 선택한 파일 경로
                ImagePath= openFileDialog.FileName;
            }
        }
        public void ExecuteRegistItem(object obj)
        {
            SItemModel regItem = new SItemModel();
            
            regItem.ItemName = ItemName;
            regItem.Cost = ItemCost;
            regItem.RegLog = ItemRegLog;
            regItem.Description = ItemDesc;
            regItem.ItemCode = "-1";
            regItem.IsOnMarket = 1;
            regItem.Seller = IntegratedCommon.instance.sockClient.UserId;
            if (File.Exists(ImagePath))
            {
                regItem.ImageData = ImageToByteArray(ImagePath);
            }
            else
            {
                regItem.ImageData = ImageToByteArray("Resources/Images/cat2.jpg");
            }
            SUserModel tmpUserModel = new SUserModel();
            tmpUserModel.UserID = IntegratedCommon.instance.sockClient.UserId;
            tmpUserModel.Cmd = "Regist";
            tmpUserModel.ItemModels = new System.Collections.ObjectModel.ObservableCollection<SItemModel>();
            tmpUserModel.ItemModels.Add(regItem);
            

            IntegratedCommon.instance.sockClient.Send(IntegratedCommon.instance.sockClient.ClientSocket,tmpUserModel);
            //NavigationService.Instance.CloseSubWindow

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




    }
}
