using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace BuySell.Popup
{
    public partial class ColorPickerPopup : PopupPage
    {
        public event EventHandler<Color> eventDone;

        public ColorPickerPopup()
        {
            InitializeComponent();
            BindingContext = new BaseViewModel();
        }

        public void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;

            var scale = 21F;
            SKPath path = new SKPath();
            path.MoveTo(-1 * scale, -1 * scale);
            path.LineTo(0 * scale, -1 * scale);
            path.LineTo(0 * scale, 0 * scale);
            path.LineTo(1 * scale, 0 * scale);
            path.LineTo(1 * scale, 1 * scale);
            path.LineTo(0 * scale, 1 * scale);
            path.LineTo(0 * scale, 0 * scale);
            path.LineTo(-1 * scale, 0 * scale);
            path.LineTo(-1 * scale, -1 * scale);

            SKMatrix matrix = SKMatrix.MakeScale(2 * scale, 2 * scale);
            SKPaint paint = new SKPaint
            {
                PathEffect = SKPathEffect.Create2DPath(matrix, path),
                Color = Color.LightGray.ToSKColor(),
                IsAntialias = true
            };
            var patternRect = new SKRect(0, 0, ((SKCanvasView)sender).CanvasSize.Width, ((SKCanvasView)sender).CanvasSize.Height);
            canvas.Save();
            canvas.DrawRect(patternRect, paint);
            canvas.Restore();
            
        }

        public async void Cancel_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

      public void Done_Tapped(System.Object sender, System.EventArgs e)
        {
            if(eventDone != null)
            {
                eventDone.Invoke(sender, ColorWheel.SelectedColor);
            }
        }

    }
}
