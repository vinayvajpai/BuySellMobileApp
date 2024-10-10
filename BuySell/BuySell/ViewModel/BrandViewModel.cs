using BuySell.Helper;
using BuySell.Model;
using BuySell.Views;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class BrandViewModel : BaseViewModel
    {
        private List<string> _brandList;
        #region Constructor
        public BrandViewModel(INavigation nav, List<string> BrandList)
        {
            navigation = nav;
            _brandList = BrandList.ToList();
            Task.Run(() =>
                {
                    GetBrandList();
                });
        }
        public BrandViewModel(INavigation nav)
        {
            navigation = nav;
            Task.Run(() =>
            {
                GetBrandList();
            });
        }
        #endregion

        #region Properties

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set {
               _searchText = value;
                OnPropertyChanged("SearchText");
                if (!string.IsNullOrEmpty(SearchText))
                {
                    var filterBrand = MasterBrandList.Where(b => b.BrandName.ToLower().Contains(_searchText.ToLower())).ToList();
                    BrandList = new ObservableCollection<BrandModel>(filterBrand);
                }
                else
                {
                    BrandList.Clear();
                }
            }
        }

        private ObservableCollection<BrandModel> _MasterBrandList = new ObservableCollection<BrandModel>();
        public ObservableCollection<BrandModel> MasterBrandList
        {
            get { return _MasterBrandList; }
            set { _MasterBrandList = value; OnPropertyChanged("MasterBrandList"); }
        }

        private ObservableCollection<BrandModel> _BrandList = new ObservableCollection<BrandModel>();
        public ObservableCollection<BrandModel> BrandList
        {
            get { return _BrandList; }
            set { _BrandList = value; OnPropertyChanged("BrandList"); }
        }
        #endregion

        #region Methods
        async public void GetBrandList()
        {
            try
            {
                var brandJson = await Global.GetBrandJson();
                var productListResponses = JsonConvert.DeserializeObject<List<BrandModel>>(brandJson);
                MasterBrandList = new ObservableCollection<BrandModel>(productListResponses);
                foreach (var Brand in _brandList.ToList())
                {
                    var masterBrand = MasterBrandList.Where(b => b.BrandName.Trim().ToLower() == Brand.Trim().ToLower()).FirstOrDefault();
                    if (masterBrand == null)
                    {
                        var brandModel = new BrandModel
                        {
                            BrandName = Brand.ToString()
                        };
                        MasterBrandList.Add(brandModel);
                    }
                }
                BrandList = new ObservableCollection<BrandModel>(MasterBrandList);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Command
        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        IsTap = false;
                        await navigation.PopAsync(true);
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }

        private Command _SearchDoneCommand;
        public Command SearchDoneCommand
        {
            get
            {
                return _SearchDoneCommand ?? (_SearchDoneCommand = new Command(async () =>
                {
                    try
                    {
                        IsTap = false;
                        MessagingCenter.Send<object, string>("SelectPropertyValue", "SelectPropertyValue", SearchText.ToString());
                        MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
                        navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }
        #endregion
    }

    #region Model
    public class BrandModel : BaseViewModel
    {
        public string BrandName { get; set; }
    }
    #endregion
}