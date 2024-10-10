using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class AlertsViewModel : BaseViewModel
    {
        #region constructor

        public AlertsViewModel()
        {
            GetAlerts();
        }

        #endregion

        #region properties

        private ObservableCollection<AlertsModel> _AlertsModelList = new ObservableCollection<AlertsModel>();
        public ObservableCollection<AlertsModel> AlertsModelList
        {
            get { return _AlertsModelList; }
            set { _AlertsModelList = value; OnPropertyChanged("AlertsModelList"); }
        }

        private AlertsModel _SelectedAlert;
        public AlertsModel SelectedAlert
        {
            get
            {
                return _SelectedAlert;
            }
            set
            {
                _SelectedAlert = value;
                OnPropertyChanged("SelectedAlert");
            }
        }


        #endregion

        #region commands

        private ICommand _SelectedAlertCmd;
        public ICommand SelectedAlertCmd
        {
            get
            {
                if (_SelectedAlertCmd == null)
                {
                     _SelectedAlertCmd = new Command(SelectedAlertCommand);
                }

                return _SelectedAlertCmd;
            }

        }

        #endregion

        #region get alerts method
        public async void GetAlerts()
        {
            try
            {
                AlertsModelList.Add(new AlertsModel()
                {
                    ActivityImage = "FillHeart.png",
                    AlertText = "i wanna buy this rainbow",
                    NotificationTime = DateTime.Now.ToString(),
                    ProductImage = "GreenDress.png",
                    UserImage = "Profile.png",
                    UserName = "Sonu240",
                    ThemeColor = Global.GetThemeColor(ThemesColor.BlueColor)

                }); 
                AlertsModelList.Add(new AlertsModel()
                {
                    ActivityImage = "FillHeart.png",
                    AlertText = "i wanna buy this rainbow",
                    NotificationTime = DateTime.Now.ToString(),
                    ProductImage = "GreenDress.png",
                    UserImage = "Profile.png",
                    UserName = "Manoj250",
                    ThemeColor = Global.GetThemeColor(ThemesColor.GreenColor)
                });
                AlertsModelList.Add(new AlertsModel()
                {
                    ActivityImage = "FillHeart.png",
                    AlertText = "i wanna buy this rainbow",
                    NotificationTime = DateTime.Now.ToString(),
                    ProductImage = "GreenDress.png",
                    UserImage = "Profile.png",
                    UserName = "Dhiraj300",
                    ThemeColor = Global.GetThemeColor(ThemesColor.RedColor)
                });
                AlertsModelList.Add(new AlertsModel()
                {
                    ActivityImage = "FillHeart.png",
                    AlertText = "i wanna buy this rainbow",
                    NotificationTime = DateTime.Now.ToString(),
                    ProductImage = "GreenDress.png",
                    UserImage = "Profile.png",
                    UserName = "Raj500",
                    ThemeColor = Global.GetThemeColor(ThemesColor.OrangeColor)
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        #endregion

        #region selected alert method
        private void SelectedAlertCommand(object obj)
        {
           // to do 
        }
        #endregion

    }
}
