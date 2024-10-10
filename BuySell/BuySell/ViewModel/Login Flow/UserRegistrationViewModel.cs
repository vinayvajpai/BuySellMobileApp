using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.Views;
using BuySell.Views.Login_Flow;
using BuySell.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BuySell.ViewModel.Login_Flow
{
    public class UserRegistrationViewModel : BaseViewModel
    {
        public UserRegistrationViewModel(INavigation _nav)
        {
            navigation = _nav;
        }

        public string PageTitle { get; set; } = "Seller Registration";

        private AddAddressModel _AddAddModel = new AddAddressModel()
        {
            State = Global.SelectedState
        };
        public AddAddressModel AddAddModel
        {
            get { return _AddAddModel; }
            set { _AddAddModel = value; OnPropertyChanged("AddAddModel"); }
        }

        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;

                            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
                            navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _BackCommand;
            }
        }

        private ICommand _SelectStateCommand;
        public ICommand SelectStateCommand
        {
            get
            {
                if (_SelectStateCommand == null)
                {
                    _SelectStateCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            navigation.PushAsync(new CountryListView());
                            MessagingCenter.Subscribe<object, string>("SelectStateValue", "SelectStateValue", (sender, arg) =>
                            {
                                if (arg != null)
                                {
                                    AddAddModel.State = arg.ToString();
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _SelectStateCommand;
            }
        }


        private ICommand _ContinueCommand;
        public ICommand ContinueCommand
        {
            get
            {
                if (_ContinueCommand == null)
                {
                    _ContinueCommand = new Command(async () =>
                    {
                        try
                        {
                            await ContinueMethod();
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _ContinueCommand;
            }
        }



        public async Task ContinueMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }


                if (IsTap)
                    return;

                IsTap = true;

                if (string.IsNullOrEmpty(AddAddModel.FullName))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter full name");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(AddAddModel.AddressLine1))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter addressline1");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(AddAddModel.City))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter city");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(AddAddModel.State))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter state");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(AddAddModel.ZipCode))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter zip code");
                    IsTap = false;
                    return;
                }
                try
                {

                    await navigation.PushAsync(new OTPVerificationPage(AddAddModel));
                }
                catch (Exception ex)
                {
                    IsTap = false;
                    UserDialogs.Instance.HideLoading();
                    Debug.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
