using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuySell.Utility
{
    public class ZoomGestureContainer : ContentView
    {
        //double currentScale = 1;
        //double startScale = 1;
        //double xOffset = 0;
        //double yOffset = 0;
        private double _currentScale = 1;

        private double _startScale = 1;

        private double _xOffset;

        private double _yOffset;

        private bool _secondDoubleTapp;
        public ZoomGestureContainer()
        {
            //var pinchGesture = new PinchGestureRecognizer();
            //pinchGesture.PinchUpdated += OnPinchUpdated;
            //GestureRecognizers.Add(pinchGesture);
            PinchGestureRecognizer pinchGestureRecognizer = new PinchGestureRecognizer();
            pinchGestureRecognizer.PinchUpdated += PinchUpdated;
            base.GestureRecognizers.Add(pinchGestureRecognizer);
            PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += OnPanUpdated;
            base.GestureRecognizers.Add(panGestureRecognizer);
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            tapGestureRecognizer.Tapped += DoubleTapped;
            base.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Started:
                    _startScale = Content.Scale;
                    Content.AnchorX = 0;
                    Content.AnchorY = 0;
                    break;
                case GestureStatus.Running:
                    {
                        if (_currentScale <= 5)
                        {
                            _currentScale += (e.Scale - 1) * _startScale;
                            _currentScale = Math.Max(1, _currentScale);
                            double num = (Content.X + _xOffset) / Width;
                            double num2 = Width / (Content.Width * _startScale);
                            double num3 = (e.ScaleOrigin.X - num) * num2;
                            double num4 = (Content.Y + _yOffset) / Height;
                            double num5 = Height / (Content.Height * _startScale);
                            double num6 = (e.ScaleOrigin.Y - num4) * num5;
                            double val = _xOffset - num3 * Content.Width * (_currentScale - _startScale);
                            double val2 = _yOffset - num6 * Content.Height * (_currentScale - _startScale);
                            Content.TranslationX = Math.Min(0, Math.Max(val, (0 - Content.Width) * (_currentScale - 1)));
                            Content.TranslationY = Math.Min(0, Math.Max(val2, (0 - Content.Height) * (_currentScale - 1)));
                            Content.Scale = _currentScale;
                        }
                        else
                        {
                            _currentScale = 5;
                        }
                            break;
                        
                    }
                case GestureStatus.Completed:
                    _xOffset = Content.TranslationX;
                    _yOffset = Content.TranslationY;
                    break;
            }
        }

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (Content.Scale == 1)
            {
                return;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    {
                        double num = e.TotalX * Scale + _xOffset;
                        double num2 = e.TotalY * Scale + _yOffset;
                        double num3 = Content.Width * Content.Scale;
                        double num4 = Content.Height * Content.Scale;
                        bool num5 = num3 > Application.Current.MainPage.Width;
                        bool flag = num4 > Application.Current.MainPage.Height;
                        if (num5)
                        {
                            double num6 = (num3 - Application.Current.MainPage.Width / 2) * -1;
                            double num7 = Math.Min(Application.Current.MainPage.Width / 2, num3 / 2);
                            if (num < num6)
                            {
                                num = num6;
                            }

                            if (num > num7)
                            {
                                num = num7;
                            }
                        }
                        else
                        {
                            num = 0;
                        }

                        if (flag)
                        {
                            double num8 = (num4 - Application.Current.MainPage.Height / 2) * -1;
                            double num9 = Math.Min(Application.Current.MainPage.Width / 2, num4 / 2);
                            if (num2 < num8)
                            {
                                num2 = num8;
                            }

                            if (num2 > num9)
                            {
                                num2 = num9;
                            }
                        }
                        else
                        {
                            num2 = 0;
                        }

                        Content.TranslationX = num;
                        Content.TranslationY = num2;
                        break;
                    }
                case GestureStatus.Completed:
                    _xOffset = Content.TranslationX;
                    _yOffset = Content.TranslationY;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                case GestureStatus.Started:
                case GestureStatus.Canceled:
                    break;
            }
        }

        public async void DoubleTapped(object sender, EventArgs e)
        {
            double multiplicator = Math.Pow(2, 0.1);
            _startScale = Content.Scale;
            Content.AnchorX = 0;
            Content.AnchorY = 0;
            for (int i = 0; i < 10; i++)
            {
                if (!_secondDoubleTapp)
                {
                    _currentScale *= multiplicator;
                }
                else
                {
                    _currentScale /= multiplicator;
                }

                double num = (Content.X + _xOffset) / Width;
                double num2 = Width / (Content.Width * _startScale);
                double num3 = (0.5 - num) * num2;
                double num4 = (Content.Y + _yOffset) / Height;
                double num5 = Height / (Content.Height * _startScale);
                double num6 = (0.5 - num4) * num5;
                double val = _xOffset - num3 * Content.Width * (_currentScale - _startScale);
                double val2 = _yOffset - num6 * Content.Height * (_currentScale - _startScale);
                base.Content.TranslationX = Math.Min(0, Math.Max(val, (0 - Content.Width) * (_currentScale - 1)));
                base.Content.TranslationY = Math.Min(0, Math.Max(val2, (0 - Content.Height) * (_currentScale - 1)));
                base.Content.Scale = _currentScale;
                await Task.Delay(10);
            }

            _secondDoubleTapp = !_secondDoubleTapp;
            _xOffset = Content.TranslationX;
            _yOffset = Content.TranslationY;
        }

        //void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        //{
        //    if (e.Status == GestureStatus.Started)
        //    {
        //        startScale = Content.Scale; Content.AnchorX = 0; Content.AnchorY = 0;
                
        //    }
        //    if (e.Status == GestureStatus.Running)
        //    {
        //        //currentScale = currentScale+((e.Scale – 1) * startScale);
        //        currentScale = currentScale + (e.Scale - 1) * startScale;
        //        currentScale = Math.Max(1, currentScale);
        //        double renderedX = Content.X + xOffset;
        //        double deltaX = renderedX / Width;
        //        double deltaWidth = Width / (Content.Width * startScale);
        //        //double originX = (e.ScaleOrigin.X – deltaX) * deltaWidth;
        //        double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;
        //        double renderedY = Content.Y + yOffset; double deltaY = renderedY / Height;
        //        double deltaHeight = Height / (Content.Height * startScale);
        //        //double originY = (e.ScaleOrigin.Y – deltaY) *deltaHeight;
        //        double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;
        //        double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
        //        double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);
        //        Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
        //        Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);
        //        Content.Scale = currentScale;
        //    }
        //    if (e.Status == GestureStatus.Completed)
        //    {
        //        xOffset = Content.TranslationX; yOffset = Content.TranslationY;
        //    }
        //}
    }
}

