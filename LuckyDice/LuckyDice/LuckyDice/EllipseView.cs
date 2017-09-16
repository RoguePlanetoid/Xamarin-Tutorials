// Based on EllipseView by Jeff Prosise 
// With contributions by Julia Boichentsova

#if __ANDROID__
using Android.Graphics;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EllipseView), typeof(AndroidEllipseViewRenderer))]
public class AndroidEllipseViewRenderer : VisualElementRenderer<EllipseView>
{
    public AndroidEllipseViewRenderer()
    {
        SetWillNotDraw(false);
    }

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnElementPropertyChanged(sender, e);
        if (e.PropertyName == EllipseView.StrokeWidthProperty.PropertyName ||
            e.PropertyName == EllipseView.ColorProperty.PropertyName)
        {
            Invalidate();
        }
    }

    protected override void OnDraw(Canvas canvas)
    {
        EllipseView element = Element;
        Rect rect = new Rect();
        GetDrawingRect(rect);
        rect.Left += ((int)element.StrokeWidth);
        rect.Right -= ((int)element.StrokeWidth);
        rect.Top += ((int)element.StrokeWidth);
        rect.Bottom -= ((int)element.StrokeWidth);
        Paint paint = new Paint()
        {
            StrokeWidth = element.StrokeWidth,
            Color = element.Color.ToAndroid(),
            AntiAlias = true
        };
        paint.SetStyle(element.IsFilled ? Paint.Style.FillAndStroke : Paint.Style.Stroke);
        canvas.DrawOval(new RectF(rect), paint);
    }
}
#endif

#if __IOS__
using CoreGraphics;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EllipseView), typeof(AppleEllipseViewRenderer))]
public class AppleEllipseViewRenderer : VisualElementRenderer<EllipseView>
{
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnElementPropertyChanged(sender, e);
        if (e.PropertyName == EllipseView.StrokeWidthProperty.PropertyName ||
            e.PropertyName == EllipseView.ColorProperty.PropertyName)
        {
            SetNeedsDisplay();
        }
    }

    public override void Draw(CGRect rect)
    {
        using (CGContext context = UIGraphics.GetCurrentContext())
        {
            double delta = Element.StrokeWidth / 2.0;
            CGRect item = new CGRect(
            rect.Left + delta,
            rect.Top + delta,
            rect.Size.Width - Element.StrokeWidth,
            rect.Size.Height - Element.StrokeWidth);
            CGPath path = CGPath.EllipseFromRect(item);
            context.AddPath(path);
            context.SetLineWidth(Element.StrokeWidth);
            if (Element.IsFilled)
            {
                context.SetFillColor(Element.Color.ToCGColor());
            }
            context.SetStrokeColor(Element.Color.ToCGColor());
            context.DrawPath(Element.IsFilled ? CGPathDrawingMode.FillStroke : CGPathDrawingMode.Stroke);
        }
    }
}
#endif

#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(EllipseView), typeof(EllipseViewRenderer))]
public class EllipseViewRenderer : ViewRenderer<EllipseView, Ellipse>
{
    protected override void OnElementChanged(ElementChangedEventArgs<EllipseView> e)
    {
        base.OnElementChanged(e);
        if (Element != null)
        {
            Ellipse ellipse = new Ellipse()
            {
                DataContext = Element
            };
            ellipse.SetBinding(Ellipse.StrokeProperty,
                new Windows.UI.Xaml.Data.Binding()
                {
                    Path = new PropertyPath("Color"),
                    Converter = new ColorConverter()
                });
            if(Element.IsFilled)
            {
                ellipse.SetBinding(Ellipse.FillProperty,
                    new Windows.UI.Xaml.Data.Binding()
                    {
                        Path = new PropertyPath("Color"),
                        Converter = new ColorConverter()
                    });
            }
            ellipse.SetBinding(Ellipse.StrokeThicknessProperty,
               new Windows.UI.Xaml.Data.Binding()
               {
                   Path = new PropertyPath("StrokeWidth")
               });
            SetNativeControl(ellipse);
        }
    }
}
#endif

namespace Xamarin.Forms
{
    public class EllipseView : View
    {
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create<EllipseView, Color>(p => p.Color, Color.Accent);

        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create<EllipseView, float>(p => p.StrokeWidth, 5f);

        public static readonly BindableProperty IsFilledProperty =
            BindableProperty.Create<EllipseView, bool>(p => p.IsFilled, false);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public bool IsFilled
        {
            get { return (bool)GetValue(IsFilledProperty); }
            set { SetValue(IsFilledProperty, value); }
        }
    }
}