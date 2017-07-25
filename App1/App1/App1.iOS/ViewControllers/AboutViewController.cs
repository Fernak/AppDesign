using System;
using UIKit;

using App1.ViewModel;

namespace App1.iOS
{
    public partial class AboutViewController : UIViewController
    {
		public AboutViewModel ViewModel { get; set; }
		public AboutViewController(IntPtr handle) : base(handle) 
		{
			ViewModel = new AboutViewModel();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = ViewModel.Title;

			// TODO: Localize these.
			AppNameLabel.Text = "App1.iOS";
			VersionLabel.Text = "1.0";
			AboutTextView.Text = "This app is written in C# and native APIs using the Xamarin Platform. It shares code with its iOS, Android, & Windows versions.";
		}

		partial void ReadMoreButton_TouchUpInside(UIButton sender) => ViewModel.OpenWebCommand.Execute(null);
    }
}