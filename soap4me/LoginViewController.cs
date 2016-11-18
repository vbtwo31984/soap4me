using System;

using UIKit;

namespace soap4me
{
	public partial class LoginViewController : UIViewController
	{
		public LoginViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		async partial void login(UIButton sender)
		{
			var api = SoapApi.getInstance();
			var result = (await api.login(usernameTextField.Text, passwordTextField.Text));
			Console.WriteLine(result);
			resultLabel.Text = result.ToString();
		}
	}
}

