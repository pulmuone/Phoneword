using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Phoneword
{
    public partial class ViewController : UIViewController
    {
        string translatedNumber = "";
        public List<string> PhoneNumbers { get; set; }

        public ViewController (IntPtr handle) : base (handle)
        {
            PhoneNumbers = new List<string>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            string translatedNumber = "";

            TranslateButton.TouchUpInside += (object sender, EventArgs e) =>
            {

                // Convert the phone number with text to a number
                // using PhoneTranslator.cs
                translatedNumber = PhoneTranslator.ToNumber(PhoneNumberText.Text);

                // 키보드 해제
                PhoneNumberText.ResignFirstResponder();

                if (translatedNumber == "")
                {
                    CallButton.SetTitle("Call", UIControlState.Normal);
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.SetTitle("Call " + translatedNumber, UIControlState.Normal);
                    CallButton.Enabled = true;
                }
            };


            CallButton.TouchUpInside += (object sender, EventArgs e) =>
            {

                PhoneNumbers.Add(translatedNumber);
                var url = new NSUrl("tel:" + translatedNumber);

                // Use URL handler with tel: prefix to invoke Apple's Phone app,
                // otherwise show an alert dialog

                if (!UIApplication.SharedApplication.OpenUrl(url))
                {
                    var alert = UIAlertController.Create("Not supported", "Scheme 'tel:' is not supported on this device", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);
                }
            };

            //Segue 없이 탐색
            //CallHistoryButton.TouchUpInside += (object sender, EventArgs e) => 
            //{
            //    CallHistoryController callHistory = this.Storyboard.InstantiateViewController("CallHistoryController") as CallHistoryController;
            //    if (callHistory != null)
            //    {
            //        callHistory.PhoneNumbers = PhoneNumbers;
            //        this.NavigationController.PushViewController(callHistory, true);
            //    }
            //};

            //CAGradientLayer gradient = new CAGradientLayer();
            //gradient.Frame = this.CallButton.Frame;
            //gradient.Colors = new CGColor[] {UIColor.Yellow.CGColor , UIColor.Blue.CGColor};
            //gradient.StartPoint = new CGPoint(0.0, 0.5);
            //gradient.EndPoint = new CGPoint(0.5, 0.5);
            //gradient.Locations = new NSNumber[] {Convert.NS(this.CallButton.Frame.Location.X), this.CallButton.Frame.Location.X };

            //CallButton.Layer.InsertSublayer(gradient, 0);
            //CallButton.Layer.CornerRadius = CallButton.Frame.Size.Height / 2;
            //CallButton.Layer.BorderWidth = 5.0f;


        }

        //Segue로 할 경우
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            // set the view controller that’s powering the screen we’re
            // transitioning to

            var callHistoryController = segue.DestinationViewController as CallHistoryController;

            //set the table view controller’s list of phone numbers to the
            // list of dialed phone numbers

            if (callHistoryController != null)
            {
                callHistoryController.PhoneNumbers = PhoneNumbers;
            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}