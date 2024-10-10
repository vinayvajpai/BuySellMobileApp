using System;
using System.Diagnostics;
using System.Windows.Input;
using BuySell.Model;
using Xamarin.Forms;
using BuySell.Helper;
using BuySell.Views;
using BuySell.Model.LoginResponse;
using static BuySell.ViewModel.CountryListViewModel;
using Acr.UserDialogs;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuySell.View;

namespace BuySell.ViewModel
{
    public class AddShippingAddressViewModel : BaseViewModel
    {
        #region Constructor
        Action<AddAddressModel> callBackResponse = null;
        public AddShippingAddressViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        public AddShippingAddressViewModel(INavigation _nav, Action<AddAddressModel> action)
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
        public AddShippingAddressViewModel(INavigation _nav, Action<AddAddressModel> action, bool IsEdit)
        {
            navigation = _nav;
            callBackResponse = action;
            IsEditAdd = IsEdit;
            if(IsEdit)
            {
                PageTitle = "Edit Shipping Address";
            }
            else
            {
                PageTitle = "Add Shipping Address";
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

        public string PageTitle { get; set; } = "Add Shipping Address";

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
                    string methodUrl = string.Empty;
                    if (!IsEditAdd)
                    {
                        methodUrl = $"/api/Account/AddShippingAddress";
                        AddAddModel.IsBilling = false;
                        AddAddModel.IsBillingShippingSame = false;
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Adding shipping address, please wait..");
                        await Task.Delay(50);
                    }
                    else
                    {
                        methodUrl = $"/api/Account/UpdateShippingAddress";
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Updating shipping address, please wait..");
                        await Task.Delay(50);
                    }

                    if (Constant.LoginUserData != null)
                    {
                        AddAddModel.UserId = Constant.LoginUserData.Id;
                        AddAddModel.IsDefault = AddAddModel.IsDefault;
                        AddAddModel.State =Global.GetTwoLetterforState(AddAddModel.State);
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
                                //Global.GlobalAddressList = new ObservableCollection<AddAddressModel>();
                                //if (Global.GlobalAddressList != null)
                                //{
                                //    if (Global.GlobalAddressList.Count > 0)
                                //        Constant.globalSelectedAddress = Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault();
                                //    else
                                //        Constant.globalSelectedAddress = new AddAddressModel();
                                //}
                                Constant.globalSelectedAddress = AddressList;
                                Global.AlertWithAction(IsEditAdd ? "Address Updated successfully!!" : "Address added successfully!!", async () =>
                                {
                                    try
                                    {
                                        bool doesPageExists = navigation.NavigationStack.Any(p => p is OrderDetailPage);
                                        if (doesPageExists)
                                        {
                                            await Global.PopToPage<OrderDetailPage>(navigation);
                                        }
                                        else
                                        {
                                            await navigation.PopAsync();
                                        }
                                        //bool doesPageExists = navigation.NavigationStack.Any(p => p is OrderDetailPage);
                                        //if (doesPageExists)
                                        //{
                                        //    for (var counter = 1; counter < 2; counter++)
                                        //    {
                                        //        navigation.RemovePage(navigation.NavigationStack[navigation.NavigationStack.Count - 2]);
                                        //    }
                                        //    await navigation.PopAsync();
                                        //}
                                        //else
                                        //{
                                        //    await navigation.PopAsync();
                                        //}
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine(ex);
                                        await navigation.PopAsync();
                                    }
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = IsEditAdd? "Address Updated successfully" : "Address added successfully",
                                //    OkText = "OK",
                                //    OnAction = async() =>
                                //    {
                                //        try
                                //        {
                                //            bool doesPageExists = navigation.NavigationStack.Any(p => p is OrderDetailPage);
                                //            if (doesPageExists)
                                //            {
                                //                for (var counter = 1; counter < 2; counter++)
                                //                {
                                //                    navigation.RemovePage(navigation.NavigationStack[navigation.NavigationStack.Count - 2]);
                                //                }
                                //                await navigation.PopAsync();
                                //            }
                                //            else
                                //            {
                                //                await navigation.PopAsync();
                                //            }
                                //        }
                                //        catch (Exception ex)
                                //        {
                                //            Debug.WriteLine(ex);
                                //            await navigation.PopAsync();
                                //        }
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

