using BuySell.Model;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class ItemDetailSizeViewModel : BaseViewModel
    {
        #region Constructor
        public ItemDetailSizeViewModel(INavigation _nav)
        {
            GetItemSizeList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<ItemDetailSizeModel> _ItemSizeList = new ObservableCollection<ItemDetailSizeModel>();
        public ObservableCollection<ItemDetailSizeModel> ItemSizeList
        {
            get { return _ItemSizeList; }
            set { _ItemSizeList = value; OnPropertyChanged("ItemSizeList"); }
        }

        private ItemDetailSizeModel _SelectedItemSize;
        public ItemDetailSizeModel SelectedItemSize
        {
            get
            {
                return _SelectedItemSize;
            }
            set
            {
                _SelectedItemSize = value;
                OnPropertyChanged("SelectedItemSize");
                if (SelectedItemSize != null)
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;

                        SelectedItemSize.SelectionColor = SelectedItemSize != null ? Color.FromHex(ThemeColor) : Color.WhiteSmoke;
                        ObservableCollection<ItemDetailSizeModel> list = new ObservableCollection<ItemDetailSizeModel>();
                        foreach (var item in ItemSizeList)
                        {
                            if (item.Size == SelectedItemSize.Size)
                            {
                                list.Add(new ItemDetailSizeModel
                                {
                                    Size = item.Size,
                                    SelectionColor = Color.FromHex(ThemeColor)

                                });
                            }
                            else
                            {
                                list.Add(new ItemDetailSizeModel
                                {
                                    Size = item.Size,
                                    SelectionColor = Color.WhiteSmoke

                                });

                            }

                        }
                        if (ItemSizeList.Count > 0)
                        {
                            ItemSizeList.Clear();
                            ItemSizeList = list;
                        }
                        if (SelectedItemSize != null)
                        {
                            navigation.PushAsync(new Page());
                        }
                        else
                        {
                            IsTap = false;
                        }
                        SelectedItemSize = null;
                    }
                    catch (Exception ex)
                    {
                        IsTap = true;
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region Methods
        public void GetItemSizeList()
        {
            try
            {
                ItemSizeList.Clear();
                ItemSizeList = new ObservableCollection<ItemDetailSizeModel>()
                {
                    new ItemDetailSizeModel()
                    {
                         Size="S"
                    },
                     new ItemDetailSizeModel()
                    {
                       Size="M"
                     },

                      new ItemDetailSizeModel()
                    {
                         Size="L"
                     },
            };
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}