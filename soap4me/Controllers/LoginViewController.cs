using System;
using Foundation;
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

			var defaults = NSUserDefaults.StandardUserDefaults;
			var username = defaults.StringForKey("username");
			var password = defaults.StringForKey("password");

			if (username != null && password != null)
			{
				usernameTextField.Text = username;
				passwordTextField.Text = password;
				login(null);
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		async partial void login(UIButton sender)
		{
			UpdateUIForLogin(true);

			var api = SoapApi.getInstance();
			var result = (await api.login(usernameTextField.Text, passwordTextField.Text));

			if (result.OK)
			{
				var defaults = NSUserDefaults.StandardUserDefaults;
				defaults.SetString("username", usernameTextField.Text);
				defaults.SetString("password", passwordTextField.Text);
				PerformSegue("showMainInterface", this);
			}
			else
			{
				UpdateUIForLogin(false);

			}
		}

		private void UpdateUIForLogin(bool loggingIn)
		{
			usernameTextField.Enabled = !loggingIn;
			passwordTextField.Enabled = !loggingIn;
			loginButton.Enabled = !loggingIn;
			errorLabel.Hidden = loggingIn;
			if (loggingIn)
			{
				loginActivityIndicator.StartAnimating();
			}
			else
				loginActivityIndicator.StopAnimating();
		}

		[Action("Logout:")]
		public void Logout(UIStoryboardSegue segue)
		{
			var defaults = NSUserDefaults.StandardUserDefaults;
			defaults.RemoveObject("username");
			defaults.RemoveObject("password");

			UpdateUIForLogin(false);
			errorLabel.Hidden = true;
			usernameTextField.Text = "";
			passwordTextField.Text = "";
		}
	}
}

