using System;
namespace soap4me
{
	public class LoginResultInternal
	{
		public long ok;
		public string token;
		public string till;
		public string sid;
	}

	public class LoginResult
	{
		private LoginResultInternal result;

		public LoginResult(LoginResultInternal result)
		{
			this.result = result;
		}

		public DateTime Till
		{
			get
			{
				if (result.till != null)
				{
					return Util.UnixTimestampToDateTime(long.Parse(result.till));
				}
				else
				{
					throw new Exception("We do not have a till date");
				}
			}
		}

		public bool OK
		{
			get
			{
				return result.ok == 1;
			}
		}

		public string Token
		{
			get
			{
				return result.token;
			}
		}

		public string Sid
		{
			get
			{
				return result.sid;
			}
		}
	}
}
