using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.RestResponse;
using BuySell.Model;
using BuySell.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BuySell.Views;

namespace BuySell.ViewModel
{
    public class AddShippingFromAddressViewModel : BaseViewModel
    {
        #region Constructor
        Action<AddAddressModel> callBackResponse = null;
        public AddShippingFromAddressViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        public AddShippingFromAddressViewModel(INavigation _nav, Action<AddAddressModel> action)
        {
            navigation = _nav;
            callBackResponse = action;
            MessagingCenter.Subscribe<object, string>("SelectPropertyValue", "SelectPropertyValue", (sender, arg) =>
            {
                if (arg != null)
                {
                    AddAddModel.State = arg.ToString();
                }
            });
        }
        public AddShippingFromAddressViewModel(INavigation _nav, Action<AddAddressModel> action, bool IsEdit)
        {
            navigation = _nav;
            callBackResponse = action;
            IsEditAdd = IsEdit;
            if(IsEdit)
            {
                PageTitle = "Edit Address";
            }
            else
            {
                PageTitle = "Add Address";
            }
            MessagingCenter.Subscribe<object, string>("SelectPropertyValue", "SelectPropertyValue", (sender, arg) =>
            {
                if (arg != null)
                {
                    AddAddModel.State = arg.ToString();
                }
            });
        }

        #endregion

        #region Properties

        public string PageTitle { get; set; } = "Add Address";

        private AddAddressModel _AddAddModel = new AddAddressModel()
        {
            State = Global.SelectedState
        };
        public AddAddressModel AddAddModel
        {
            get { return _AddAddModel; }
            set { _AddAddModel = value; OnPropertyChanged("AddAddModel"); }
        }

        private bool _IsEditAdd = false;
        public bool IsEditAdd
        {
            get { return _IsEditAdd; }
            set { _IsEditAdd = value; OnPropertyChanged("IsEditAdd"); }
        }

        #endregion

        #region Command
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
                            //if (IsTap)
                            //    return;
                            //IsTap = true;
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

        //private ICommand _SaveCommand;
        //public ICommand SaveCommand
        //{
        //    get
        //    {
        //        if (_SaveCommand == null)
        //        {
        //            _SaveCommand = new Command(async () => await SaveMethod());

        //        }

        //        return _SaveCommand;
        //    }
        //}
        #endregion

        public async Task SaveMethod()
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
                //if (string.IsNullOrEmpty(AddAddModel.AddressLine2))
                //{
                //    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter addressline2");
                //    IsTap = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(AddAddModel.City))
                //{
                //    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter city");
                //    IsTap = false;
                //    return;
                //}
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
                    string methodUrl = string.Empty;
                    if (!IsEditAdd)
                    {
                        methodUrl = $"/api/Account/AddFromShippingAddress";
                        AddAddModel.IsBilling = false;
                        AddAddModel.IsBillingShippingSame = false;
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Adding address, please wait..");
                        await Task.Delay(50);
                    }
                    else
                    {
                        methodUrl = $"/api/Account/UpdateFromShippingAddress";
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Updating address, please wait..");
                        await Task.Delay(50);
                    }

                    if (Constant.LoginUserData != null)
                    {
                        AddAddModel.UserId = Constant.LoginUserData.Id;
                        AddAddModel.IsDefault = true;
                        AddAddModel.State = Global.GetTwoLetterforState(AddAddModel.State);
                    }

                    if (!string.IsNullOrWhiteSpace(methodUrl))
                    {
                        Debug.WriteLine(JsonConvert.SerializeObject(AddAddModel));

                        RestResponseModel responseModel = await WebService.PostData(AddAddModel, methodUrl, true);
                        if (responseModel != null)
                        {
                            if (responseModel.status_code == 200)
                            {
                                var AddressList = JsonConvert.DeserializeObject<AddAddressModel>(responseModel.response_body);
                                Constant.globalSelectedFromAddress = AddressList;
                                Global.AlertWithAction(IsEditAdd ? "Address Updated successfully!!" : "Address added successfully!!", async () =>
                                {
                                    await navigation.PopAsync();
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = IsEditAdd ? "Address Updated successfully" : "Address added successfully",
                                //    OkText = "OK",
                                //    OnAction = async () =>
                                //    {
                                //        await navigation.PopAsync();
                                //    }
                                //};
                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                UserDialogs.Instance.HideLoading();
                            }
                            else if (responseModel.status_code == 500)
                            {
                                ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
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
