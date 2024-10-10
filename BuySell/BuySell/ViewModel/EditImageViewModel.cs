using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using BuySell.Views;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class EditImageViewModel : BaseViewModel
    {
        #region Constractor
        public EditImageViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Properties
        double mX = 0f;
        double mY = 0f;
        double mRatioPan = -0.0015f;
        double mRatioZoom = 0.8f;

        public string ImageUrl { get; set; } = "http://loremflickr.com/600/600/nature?filename=crop_transformation.jpg";

        private List<ITransformation> transformations;
        public List<ITransformation> Transformations
        {
            get
            {
                return transformations;
            }
            set
            {
                transformations = value;
                OnPropertyChanged("Transformations");
            }
        }

        public double CurrentZoomFactor { get; set; }

        public double CurrentXOffset { get; set; }

        public double CurrentYOffset { get; set; }
        #endregion

        #region Commands
        private Command _CancleCommand;
        public Command CancleCommand
        {
            get
            {
                return _CancleCommand ?? (_CancleCommand = new Command(async () =>
            {
                try
                {
                    if (IsTap)
                        return;
                    IsTap = true;
                    await navigation.PopAsync();

                }
                catch (Exception ex)
                {
                    IsTap = false;
                    Debug.WriteLine(ex.Message);
                }
                }));
            }
        }

        private Command viewImageCommand;
        public ICommand ViewImageCommand
        {
            get
            {
                if (viewImageCommand == null)
                {
                    viewImageCommand = new Command(ViewImage);
                }

                return viewImageCommand;
            }
        }

        private ICommand changeImageCommand;
        public ICommand ChangeImageCommand
        {
            get
            {
                if (changeImageCommand == null)
                {
                    changeImageCommand = new Command(ChangeImage);
                }

                return changeImageCommand;
            }
        }
        #endregion

        #region Methods
        private void ViewImage()
        {

        }

        public void Reload()
        {
            CurrentZoomFactor = 1d;
            CurrentXOffset = 0d;
            CurrentYOffset = 0d;
        }

        public void ReloadImage()
        {
            try
            {
                Transformations = new List<ITransformation>() {
                new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, 1f, 1f)
            };

                MessagingCenter.Send<object, object>(this, "Refresh", "");
            }
            catch (Exception ex)
            {

            }
        }

        public void OnPanUpdated(PanUpdatedEventArgs e)
        {
            try
            {
                if (e.StatusType == GestureStatus.Completed)
                {
                    mX = CurrentXOffset;
                    mY = CurrentYOffset;
                }
                else if (e.StatusType == GestureStatus.Running)
                {
                    CurrentXOffset = (e.TotalX * mRatioPan) + mX;
                    CurrentYOffset = (e.TotalY * mRatioPan) + mY;
                    ReloadImage();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OnPinchUpdated(PinchGestureUpdatedEventArgs e)
        {
            try
            {
                if (e.Status == GestureStatus.Completed)
                {
                    mX = CurrentXOffset;
                    mY = CurrentYOffset;
                }
                else if (e.Status == GestureStatus.Running)
                {
                    CurrentZoomFactor += (e.Scale - 1) * CurrentZoomFactor * mRatioZoom;
                    CurrentZoomFactor = Math.Max(1, CurrentZoomFactor);

                    CurrentXOffset = (e.ScaleOrigin.X * mRatioPan) + mX;
                    CurrentYOffset = (e.ScaleOrigin.Y * mRatioPan) + mY;
                    ReloadImage();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void ChangeImage()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                navigation.PopAsync();
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}

