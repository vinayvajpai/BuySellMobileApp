using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.RestResponse;
using BuySell.Model;
using BuySell.Views;
using BuySell.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Globalization;

namespace BuySell.ViewModel
{
    public class MyEarningViewModel : BaseViewModel
    {
        #region Constructor
        public MyEarningViewModel(INavigation _nav , MyEarningsResponseModel myEarningsResponseModel)
        {
            navigation= _nav;

            if(myEarningsResponseModel != null )
            {

                WithDrawAmount = "$" + Convert.ToDouble(myEarningsResponseModel.Balance).ToString("F2", CultureInfo.InvariantCulture);
                InWithdrawalStatusPayments = "$" + Convert.ToDouble(myEarningsResponseModel.InWithdrawalStatusPayments).ToString("F2", CultureInfo.InvariantCulture);
                if (myEarningsResponseModel.CompletedPayments != null)
                {
                    WithDrawalHistory = new List<LastWithdrawals> { new LastWithdrawals()
                            {
                                CompletedPayments = "$" + Convert.ToDouble(myEarningsResponseModel.CompletedPayments).ToString("F2", CultureInfo.InvariantCulture),
                                 LastCompletedPayment = myEarningsResponseModel.LastCompletedPayment
                            }};
                }
            }
        }
        #endregion

        #region Properties

        private string _withDrawAmount = "$0";
        public string WithDrawAmount
        {
            get { return _withDrawAmount; }
            set { _withDrawAmount = value; OnPropertyChanged("WithDrawAmount"); }
        }
        
        private string _InWithdrawalStatusPayments = "$0";
        public string InWithdrawalStatusPayments
        {
            get { return _InWithdrawalStatusPayments; }
            set { _InWithdrawalStatusPayments = value; OnPropertyChanged("InWithdrawalStatusPayments"); }
        }

        private List<LastWithdrawals> _WithDrawalHistory;
        public List<LastWithdrawals> WithDrawalHistory
        {
            get
            {
                return _WithDrawalHistory;
            }
            set
            {
                _WithDrawalHistory = value;
                OnPropertyChanged(nameof(WithDrawalHistory));
            }
        }

        private string _Price = "$0";
        public string Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        //private string _Price = "$0";
        //public string Price
        //{
        //    get
        //    {
        //        if (_Price != null)
        //        {
        //            return _Price.Contains("$") ? _Price : "$" + _Price;
        //        }
        //        else
        //            return _Price;
        //    }
        //    set
        //    {
        //        try
        //        {
        //            //Condition added to restrict the user to do not enter values in decimals
        //            if (value.Contains("."))
        //            {
        //                value = value.Replace(".", "");
        //                Global.AlertWithAction("Only integers are allowed",null);
        //                //var alertConfig = new AlertConfig
        //                //{
        //                //    Message = "Only integers are allowed",
        //                //    OkText = "OK",

            //                //};
            //                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
            //                _Price = value;
            //            }
            //            else
            //            {
            //                _Price = value;
            //                if (!string.IsNullOrWhiteSpace(_Price))
            //                {
            //                    var a = _Price.ToCharArray();
            //                    if (a.Length > 1)
            //                    {
            //                        if (a[1] == '.')
            //                        {
            //                            _Price = _Price.Replace(".", "");
            //                        }
            //                    }
            //                }


            //            }
            //            OnPropertyChanged("Price");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //    }

            //}
            #endregion

            #region Commands
        private Command _WithdrawlCommand;
        public Command WithdrawlCommand
        {
            get
            {
                return _WithdrawlCommand ?? (_WithdrawlCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        var amount = Convert.ToDouble(WithDrawAmount.Replace("$", ""));
                        if (amount > 0)
                        {
                            await navigation.PushAsync(new WithDrawlSummaryView(amount));
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("There is not enough money in your account");
                            IsTap = false;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }
        #endregion

        #region Method

        #endregion

    }
}