using Foundation;
using System;
using UIKit;
using CoreAnimation;
using CoreGraphics;namespace Phoneword
{
    public partial class CallButtonGradient : UIButton
    {
        public CallButtonGradient (IntPtr handle) : base (handle)
        {

            CAGradientLayer gradient = new CAGradientLayer();
            gradient.Frame = this.Bounds;

            gradient.Colors = new CGColor[] { UIColor.Yellow.CGColor, UIColor.Blue.CGColor };
            gradient.StartPoint = new CGPoint(0.0, 0.5);
            gradient.EndPoint = new CGPoint(0.5, 1.0);
            gradient.Locations = new NSNumber[] { 0.5, 1.0 };

            this.Layer.InsertSublayer(gradient, 0);
        }
    }
}