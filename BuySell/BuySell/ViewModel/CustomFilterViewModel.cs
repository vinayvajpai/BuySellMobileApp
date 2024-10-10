using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.Utility;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public partial class CustomFilterViewModel : BaseViewModel
    {
        public string RootSelectedCategory = null;
        public string selGender = null;

        #region Constructor
        public CustomFilterViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<FilterModel> _FilterList = new ObservableCollection<FilterModel>()
        {
            
        };
        public ObservableCollection<FilterModel> FilterList
        {
            get { return _FilterList; }
            set { _FilterList = value; OnPropertyChanged("FilterList"); }
        }

        private bool _IsShowCatAll;
        public bool IsShowCatAll
        {
            get { return _IsShowCatAll; }
            set { _IsShowCatAll = value; OnPropertyChanged("IsShowCatAll"); }
        }

        #endregion

        #region ShowAllCat Methods
        public void ShowAllCat()
        {
            FilterList.Clear();
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.SortStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            //selGender = IsShowCatAll ? Constant.AllStr : Global.GenderParam;
            //FilterList.Add(new FilterModel()
            //{
            //    KEY = Constant.GenderStr,
            //    Value = selGender,
            //    PreviousValue = selGender,
            //    CrossVisible = false,
            //    DownVisible = true,
            //    IsKeyVisible = false
            //});
            if ((Global.Subcategory != null && Global.StoreIndex != 2) || (Global.Subcategory == null))
            {
                //RootSelectedCategory = IsShowCatAll ? Constant.AllStr : Global.Subcategory != null ? (Global.GenderParam + (string.IsNullOrEmpty(Global.Subcategory) ? "" : " | " + Global.Subcategory)):Constant.AllStr;
                RootSelectedCategory = IsShowCatAll ? Constant.AllStr : (Global.GenderParam + (string.IsNullOrEmpty(Global.Subcategory) ? "" : " | " + Global.Subcategory));
                FilterList.Add(new FilterModel()
                {
                    KEY = Constant.CategoryStr,
                    Value = RootSelectedCategory,//RootSelectedCategory,//IsShowCatAll ? "All" : (Global.GenderParam + (string.IsNullOrEmpty(Global.Subcategory) ? "" : " | " + Global.Subcategory)),
                    PreviousValue = RootSelectedCategory,
                    CrossVisible = false,
                    DownVisible = true,
                    IsKeyVisible = true
                });
            }
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.SizeStr,
                Value = Constant.AllStr,
                PreviousValue= Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.BrandStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.ColorStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.ConditionStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.AvailabilityStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.PriceStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
            FilterList.Add(new FilterModel()
            {
                KEY = Constant.ShippingPriceStr,
                Value = Constant.AllStr,
                PreviousValue = Constant.AllStr,
                CrossVisible = false,
                DownVisible = true,
                IsKeyVisible = true
            });
        }
        #endregion
    }

    #region FilterModel
    public class FilterModel : BaseViewModel
    {
        public string KEY { get; set; }

        public bool IsKeyVisible { get; set; }

        private bool _DownVisible;
        public bool DownVisible
        {
            get
            {
                return _DownVisible;
            }
            set
            {
                _DownVisible = value;
                OnPropertyChanged("DownVisible");
            }
        }

        private string _previousValue;
        public string PreviousValue
        {
            get
            {
                return _previousValue;
            }
            set
            {
                _previousValue = value;
                OnPropertyChanged("PreviousValue");
            }
        }

        private bool _CrossVisible;
        public bool CrossVisible
        {
            get
            {
                return _CrossVisible;
            }
            set
            {
                _CrossVisible = value;
                OnPropertyChanged("CrossVisible");
            }
        }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

    }
    #endregion
}
