using System;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    public class ReorderImgListModel : BaseViewModel
    {
        public ImageSource imageSource { get; set; }

        private bool _isBeingDragged;
        public bool IsBeingDragged
        {
            get { return _isBeingDragged; }
            set { SetProperty(ref _isBeingDragged, value); }
        }

        private bool _isBeingDraggedOver;
        public bool IsBeingDraggedOver
        {
            get { return _isBeingDraggedOver; }
            set { SetProperty(ref _isBeingDraggedOver, value); }
        }
    }
}
